using LitobzorImport.Contracts;
using LitobzorImport.DataAccess.Contracts;
using LitobzorImport.Domain.Aggregates;
using LitobzorImport.Domain.References;
using LitobzorImport.Domain.Relations;

namespace LitobzorImport.Logic
{
    public class ReadService(
        IReadSourceRepository<HighLevelGroupTerm> highLevelGroupTermsRepository,
        IReadSourceRepository<HighLevelTerm> highLevelTermsRepository,
        IReadSourceRepository<LowLevelTerm> lowLevelTermRepository,
        IReadSourceRepository<PreferredTerm> preferredTermRepository,
        IReadSourceRepository<SystemOrganClass> systemOrganClassRepository,

        IReadSourceRepository<HltPtRelation> hltPtRelationRepository,
        IReadSourceRepository<HlgtHltRelation> hlgtHltRelationRepository,
        IReadSourceRepository<SocHlgtRelation> socHlgtRelationRepository,

        IReadSourceRepository<MedDraHierarchy> medDraHierarchyRepository,

        IReadHierarchyRepository<SystemOrganClass> readSocHierarchyRepository,

        IReadHierarchyRepository<HighLevelGroupTerm> readHlgtRepository,
        IReadHierarchyRepository<HighLevelTerm> readHltRepository,
        IReadHierarchyRepository<PreferredTerm> readPtRepository,
        IReadHierarchyRepository<LowLevelTerm> readLltRepository,

        IReadHierarchyRepository<SocHlgtRelation> readSocHlgtRelationRepository,
        IReadHierarchyRepository<HlgtHltRelation> readHlgtHltRelationRepository,
        IReadHierarchyRepository<HltPtRelation> readHltPtRelationtRepository,

        IReadHierarchyRepository<MedDraHierarchy> readMedDraRepository) : IReadService
    {
        private readonly IReadSourceRepository<HighLevelGroupTerm> highLevelGroupTermsRepository = highLevelGroupTermsRepository;
        private readonly IReadSourceRepository<HighLevelTerm> highLevelTermsRepository = highLevelTermsRepository;
        private readonly IReadSourceRepository<LowLevelTerm> lowLevelTermRepository = lowLevelTermRepository;
        private readonly IReadSourceRepository<PreferredTerm> preferredTermRepository = preferredTermRepository;
        private readonly IReadSourceRepository<SystemOrganClass> systemOrganClassRepository = systemOrganClassRepository;

        private readonly IReadSourceRepository<HltPtRelation> hltPtRelationRepository = hltPtRelationRepository;
        private readonly IReadSourceRepository<HlgtHltRelation> hlgtHltRelationRepository = hlgtHltRelationRepository;
        private readonly IReadSourceRepository<SocHlgtRelation> socHlgtRelationRepository = socHlgtRelationRepository;

        private readonly IReadHierarchyRepository<SystemOrganClass> readSocHierarchyRepository = readSocHierarchyRepository;
        private readonly IReadHierarchyRepository<HighLevelGroupTerm> readHlgtRepository = readHlgtRepository;
        private readonly IReadHierarchyRepository<HighLevelTerm> readHltRepository = readHltRepository;
        private readonly IReadHierarchyRepository<PreferredTerm> readPtRepository = readPtRepository;
        private readonly IReadHierarchyRepository<LowLevelTerm> readLltRepository = readLltRepository;

        private readonly IReadHierarchyRepository<SocHlgtRelation> readSocHlgtRelationRepository = readSocHlgtRelationRepository;
        private readonly IReadHierarchyRepository<HlgtHltRelation> readHlgtHltRelationRepository = readHlgtHltRelationRepository;
        private readonly IReadHierarchyRepository<HltPtRelation> readHltPtRelationtRepository = readHltPtRelationtRepository;

        private readonly IReadHierarchyRepository<MedDraHierarchy> readMedDraRepository = readMedDraRepository;

        private readonly IReadSourceRepository<MedDraHierarchy> medDraHierarchyRepository = medDraHierarchyRepository;

        #region IReadService Implementation

        public List<SourceReference<HighLevelGroupTerm>> ReadHighLevelGroupTerms()
        {
            return highLevelGroupTermsRepository.GetAll().ToList();
        }

        public List<SourceReference<HighLevelTerm>> ReadHighLevelTerms()
        {
            return highLevelTermsRepository.GetAll().ToList();
        }

        public List<SourceReference<LowLevelTerm>> ReadLowLevelTerms()
        {
            return lowLevelTermRepository.GetAll().ToList();
        }

        public List<SourceReference<PreferredTerm>> ReadPreferredTerms()
        {
            return preferredTermRepository.GetAll().ToList();
        }

        public List<SourceReference<SystemOrganClass>> ReadSystemOrganClasses()
        {
            return systemOrganClassRepository.GetAll().ToList();
        }

        public List<SourceReference<HltPtRelation>> ReadHltPtRelations()
        {
            return hltPtRelationRepository.GetAll().ToList();
        }

        public List<SourceReference<HlgtHltRelation>> ReadHlgtHltRelations()
        {
            return hlgtHltRelationRepository.GetAll().ToList();
        }

        public List<SourceReference<SocHlgtRelation>> ReadHlgtSocRelations()
        {
            return socHlgtRelationRepository.GetAll().ToList();
        }

        public List<SourceReference<MedDraHierarchy>> ReadMedDraHierarchies()
        {
            return medDraHierarchyRepository.GetAll().ToList();
        }

        public List<SystemOrganClass> ReadHierarchyFull()
        {
            return readSocHierarchyRepository.GetAllFull();
        }

        public List<(string, int)> ReadStatistics()
        {
            var toReturn = new List<(string, int)>();

            toReturn.Add((readSocHierarchyRepository.Config.LongDisplayName, readSocHierarchyRepository.GetCount()));
            toReturn.Add((readHlgtRepository.Config.LongDisplayName, readHlgtRepository.GetCount()));
            toReturn.Add((readHltRepository.Config.LongDisplayName, readHltRepository.GetCount()));
            toReturn.Add((readPtRepository.Config.LongDisplayName, readPtRepository.GetCount()));
            toReturn.Add((readLltRepository.Config.LongDisplayName, readLltRepository.GetCount()));

            toReturn.Add((readSocHlgtRelationRepository.Config.LongDisplayName, readMedDraRepository.GetCount()));
            toReturn.Add((readHlgtHltRelationRepository.Config.LongDisplayName, readMedDraRepository.GetCount()));
            toReturn.Add((readHltPtRelationtRepository.Config.LongDisplayName, readMedDraRepository.GetCount()));

            toReturn.Add((readMedDraRepository.Config.LongDisplayName, readMedDraRepository.GetCount()));


            return toReturn;
        }

        #endregion
    }
}
