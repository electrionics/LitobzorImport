
using LitobzorImport.Contracts;
using LitobzorImport.DataAccess.Contracts;
using LitobzorImport.Domain.Aggregates;
using LitobzorImport.Domain.Base;
using LitobzorImport.Domain.References;
using LitobzorImport.Domain.Relations;

namespace LitobzorImport.Logic
{
    public class ImportService(
        IWriteRepository<HighLevelGroupTerm> highLevelGroupTermsRepository,
        IWriteRepository<HighLevelTerm> highLevelTermsRepository,
        IWriteRepository<LowLevelTerm> lowLevelTermRepository,
        IWriteRepository<PreferredTerm> preferredTermRepository,
        IWriteRepository<SystemOrganClass> systemOrganClassRepository,

        IWriteRepository<HltPtRelation> hltPtRelationRepository,
        IWriteRepository<HlgtHltRelation> hlgtHltRelationRepository,
        IWriteRepository<SocHlgtRelation> socHlgtRelationRepository,

        IWriteRepository<MedDraHierarchy> medDraHierarchyRepository,
        
        IReadService readService,
        IProgressNotifier progressNotifier) : IImportService
    {
        private readonly IWriteRepository<HighLevelGroupTerm> highLevelGroupTermsRepository = highLevelGroupTermsRepository;
        private readonly IWriteRepository<HighLevelTerm> highLevelTermsRepository = highLevelTermsRepository;
        private readonly IWriteRepository<LowLevelTerm> lowLevelTermRepository = lowLevelTermRepository;
        private readonly IWriteRepository<PreferredTerm> preferredTermRepository = preferredTermRepository;
        private readonly IWriteRepository<SystemOrganClass> systemOrganClassRepository = systemOrganClassRepository;
        private readonly IWriteRepository<HltPtRelation> hltPtRelationRepository = hltPtRelationRepository;
        private readonly IWriteRepository<HlgtHltRelation> hlgtHltRelationRepository = hlgtHltRelationRepository;
        private readonly IWriteRepository<SocHlgtRelation> socHlgtRelationRepository = socHlgtRelationRepository;
        private readonly IWriteRepository<MedDraHierarchy> medDraHierarchyRepository = medDraHierarchyRepository;
        private readonly IReadService readService = readService;
        private readonly IProgressNotifier progressNotifier = progressNotifier;

        public void CleanDatabase()
        {
            var anyReposiory = highLevelGroupTermsRepository;
            anyReposiory.CleanDatabase();

            progressNotifier.NotifyProgress("Database initialized successfully.", "message");
        }

        public void ImportFullHierarchy()
        {
            var writeRepository = medDraHierarchyRepository;
            var data = readService.ReadMedDraHierarchies();
            var dataToMatch = readService.ReadHierarchyFull();

            var imported = 0;
            foreach (var datum in data)
            {
                try
                {
                    // matching with database hierarchy (else - exception)

                    var socToMatch = dataToMatch.Single(x => x.SocCode == datum.Value.SocCode);
                    var hlgtToMatch = socToMatch.SocHlgtRelations.Single(x => x.HlgtCode == datum.Value.HlgtCode).HighLevelGroupTerm;
                    var hltToMatch = hlgtToMatch.HlgtHltRelations.Single(x => x.HltCode == datum.Value.HltCode).HighLevelTerm;
                    var ptToMatch = hltToMatch.HltPtRelations.Single(x => x.PtCode == datum.Value.PtCode).PreferredTerm;
                    var defaultPtToMatch = socToMatch.PreferredTerms.Single(x => x.PtSocCode == datum.Value.PtSocCode);

                    writeRepository.Create(datum.Value);
                    imported++;
                }
                catch (Exception ex)
                {
                    progressNotifier.NotifyProgress($"File {writeRepository.Config.FileName}: Can't create {writeRepository.Config.DisplayName} hierarchy from line {datum.LineNumber}", "createError");
                }
            }

            progressNotifier.NotifyProgress($"Building hierarchy... №{imported} records", "imported");

            data.Clear();
            data.TrimExcess();
        }

        public void ImportReferences()
        {
            var socs = readService.ReadSystemOrganClasses();
            SaveReferencesInternal(systemOrganClassRepository, socs);

            var hlgts = readService.ReadHighLevelGroupTerms();
            SaveReferencesInternal(highLevelGroupTermsRepository, hlgts);

            var hlts = readService.ReadHighLevelTerms();
            SaveReferencesInternal(highLevelTermsRepository, hlts);

            var pts = readService.ReadPreferredTerms();
            SaveReferencesInternal(preferredTermRepository, pts);

            var llts = readService.ReadLowLevelTerms();
            SaveReferencesInternal(lowLevelTermRepository, llts);
        }

        private void SaveReferencesInternal<T>(IWriteRepository<T> writeRepository, List<SourceReference<T>> data)
            where T : IReferenceMarker
        {
            var imported = 0;
            foreach (var reference in data)
            {
                try
                {
                    writeRepository.Create(reference.Value);
                    imported++;
                }
                catch (Exception ex)
                {
                    progressNotifier.NotifyProgress($"File {writeRepository.Config.FileName}: Can't create {writeRepository.Config.DisplayName} record from line {reference.LineNumber}", "createError");
                }
            }

            progressNotifier.NotifyProgress($"Importing {writeRepository.Config.DisplayName}... №{imported} records imported", "imported");

            data.Clear();
            data.TrimExcess();
        }

        public void ImportRelations()
        {
            progressNotifier.NotifyProgress("Importing relationships...", "message");

            var sochlgts = readService.ReadHlgtSocRelations();
            SaveRelationshipsInternal(socHlgtRelationRepository, sochlgts);

            var hlgthlts = readService.ReadHlgtHltRelations();
            SaveRelationshipsInternal(hlgtHltRelationRepository, hlgthlts);

            var hltpts = readService.ReadHltPtRelations();
            SaveRelationshipsInternal(hltPtRelationRepository, hltpts);
        }

        private void SaveRelationshipsInternal<T>(IWriteRepository<T> writeRepository, List<SourceReference<T>> data)
            where T : IRelationMarker
        {
            var imported = 0;
            foreach (var relation in data)
            {
                try
                {
                    writeRepository.Create(relation.Value);
                    imported++;
                }
                catch (Exception ex)
                {
                    progressNotifier.NotifyProgress($"File  {writeRepository.Config.FileName}: Can't create {writeRepository.Config.DisplayName} relation from line {relation.LineNumber}", "createError");
                }
            }

            progressNotifier.NotifyProgress($"Importing {writeRepository.Config.DisplayName}... №{imported} relations imported", "imported");

            data.Clear();
            data.TrimExcess();
        }

        public void ShowStatistics()
        {
            var statistics = readService.ReadStatistics();

            progressNotifier.NotifyProgress("Statistics:", "message");

            foreach(var kvp in statistics)
            {
                progressNotifier.NotifyProgress($"- {kvp.Item1}: №{kvp.Item2}", "statistics");
            }
        }
    }
}
