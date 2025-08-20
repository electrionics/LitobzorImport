using LitobzorImport.Contracts;
using LitobzorImport.DataAccess;
using LitobzorImport.DataAccess.Contracts;
using LitobzorImport.Domain;
using LitobzorImport.Domain.Aggregates;
using LitobzorImport.Domain.References;
using LitobzorImport.Domain.Relations;
using LitobzorImport.Logic;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;

using System.Reflection;
using LitobzorImport.DataAccess.DomainConfig.References;
using LitobzorImport.DataAccess.DomainConfig.Aggregate;
using LitobzorImport.DataAccess.DomainConfig.Relations;
using System.Diagnostics;


Console.WriteLine("MedDRA Data Importer v1.0");
Console.WriteLine("========================");
Console.WriteLine();

var serviceCollection = new ServiceCollection();
ConfigureServices(serviceCollection);

var serviceProvider = serviceCollection.BuildServiceProvider();

//var context = serviceProvider.GetRequiredService<LitobzorDataContext>();
//context.Database.EnsureCreated();

//return;

var importService = serviceProvider.GetRequiredService<IImportService>();
var progressNotifier = serviceProvider.GetRequiredService<IProgressNotifier>();

var sw = Stopwatch.StartNew();

importService.CleanDatabase();
Console.WriteLine();

importService.ImportReferences();
Console.WriteLine();

importService.ImportRelations();
Console.WriteLine();

importService.ImportFullHierarchy();
Console.WriteLine();

importService.ShowStatistics();

sw.Stop();
Console.WriteLine(sw.Elapsed.ToString("hh\\:mm\\:ss"));

Console.WriteLine("Errors:");
progressNotifier.WriteDelayed();

Console.ReadLine();

static void ConfigureServices(IServiceCollection services)
{
    // main services
    services.AddSingleton<IProgressNotifier, ConsoleCompositeProgressNotifier>();
    services.AddSingleton<IImportService, ImportService>();
    services.AddSingleton<IReadService, ReadService>();

    // read repositories (file)
    services.AddTransient<IReadSourceRepository<SystemOrganClass>, ParseFileRepository<SystemOrganClass>>();
    services.AddTransient<IReadSourceRepository<HighLevelGroupTerm>, ParseFileRepository<HighLevelGroupTerm>>();
    services.AddTransient<IReadSourceRepository<HighLevelTerm>, ParseFileRepository<HighLevelTerm>>();
    services.AddTransient<IReadSourceRepository<PreferredTerm>, ParseFileRepository<PreferredTerm>>();
    services.AddTransient<IReadSourceRepository<LowLevelTerm>, ParseFileRepository<LowLevelTerm>>();

    services.AddTransient<IReadSourceRepository<SocHlgtRelation>, ParseFileRepository<SocHlgtRelation>>();
    services.AddTransient<IReadSourceRepository<HlgtHltRelation>, ParseFileRepository<HlgtHltRelation>>();
    services.AddTransient<IReadSourceRepository<HltPtRelation>, ParseFileRepository<HltPtRelation>>();

    services.AddTransient<IReadSourceRepository<MedDraHierarchy>, ParseFileRepository<MedDraHierarchy>>();

    // read repositories (db)
    services.AddTransient<IReadHierarchyRepository<MedDraHierarchy>, ReadDbRepository<MedDraHierarchy>>();
    services.AddTransient<IReadHierarchyRepository<HighLevelGroupTerm>, ReadDbRepository<HighLevelGroupTerm>>();
    services.AddTransient<IReadHierarchyRepository<HighLevelTerm>, ReadDbRepository<HighLevelTerm>>();
    services.AddTransient<IReadHierarchyRepository<PreferredTerm>, ReadDbRepository<PreferredTerm>>();
    services.AddTransient<IReadHierarchyRepository<LowLevelTerm>, ReadDbRepository<LowLevelTerm>>();

    services.AddTransient<IReadHierarchyRepository<SocHlgtRelation>, ReadDbRepository<SocHlgtRelation>>();
    services.AddTransient<IReadHierarchyRepository<HlgtHltRelation>, ReadDbRepository<HlgtHltRelation>>();
    services.AddTransient<IReadHierarchyRepository<HltPtRelation>, ReadDbRepository<HltPtRelation>>();

    services.AddTransient<IReadHierarchyRepository<SystemOrganClass>, ReadDbHierarchyRepository>();

    //write repositories (db)
    services.AddTransient<IWriteRepository<SystemOrganClass>, ImportDbRepository<SystemOrganClass>>();
    services.AddTransient<IWriteRepository<HighLevelGroupTerm>, ImportDbRepository<HighLevelGroupTerm>>();
    services.AddTransient<IWriteRepository<HighLevelTerm>, ImportDbRepository<HighLevelTerm>>();
    services.AddTransient<IWriteRepository<PreferredTerm>, ImportDbRepository<PreferredTerm>>();
    services.AddTransient<IWriteRepository<LowLevelTerm>, ImportDbRepository<LowLevelTerm>>();

    services.AddTransient<IWriteRepository<SocHlgtRelation>, ImportDbRepository<SocHlgtRelation>>();
    services.AddTransient<IWriteRepository<HlgtHltRelation>, ImportDbRepository<HlgtHltRelation>>();
    services.AddTransient<IWriteRepository<HltPtRelation>, ImportDbRepository<HltPtRelation>>();

    services.AddTransient<IWriteRepository<MedDraHierarchy>, ImportDbRepository<MedDraHierarchy>>();

    //configs
    services.AddSingleton<IDomainConfig<SystemOrganClass>, SystemOrganClassConfig>();
    services.AddSingleton<IDomainConfig<HighLevelGroupTerm>, HighLevelGroupTermConfig>();
    services.AddSingleton<IDomainConfig<HighLevelTerm>, HighLevelTermConfig>();
    services.AddSingleton<IDomainConfig<PreferredTerm>, PreferredTermConfig>();
    services.AddSingleton<IDomainConfig<LowLevelTerm>, LowLevelTermConfig>();

    services.AddSingleton<IDomainConfig<SocHlgtRelation>, SocHlgtRelationConfig>();
    services.AddSingleton<IDomainConfig<HlgtHltRelation>, HlgtHltRelationConfig>();
    services.AddSingleton<IDomainConfig<HltPtRelation>, HltPtRelationConfig>();

    services.AddSingleton<IDomainConfig<MedDraHierarchy>, MedDraHierarchyConfig>();

    //app config
    var configuration = GetConfiguration("LitobzorImport.App.appconfig.json");
    var config = configuration.GetSection("Data").Get<DataConfiguration>();
    services.AddSingleton<IDataConfiguration>(config!);

    //data context
    services.AddDbContextFactory<LitobzorDataContext>((options) =>
    {
        options.UseSqlite(config!.ConnectionString);
    }, ServiceLifetime.Transient);
}

static IConfiguration GetConfiguration(string name)
{
    var assembly = Assembly.GetExecutingAssembly();
    using (var stream = assembly.GetManifestResourceStream(name))
    {
        var config = new ConfigurationBuilder()
            .AddJsonStream(stream!);
        return config.Build();
    }
}