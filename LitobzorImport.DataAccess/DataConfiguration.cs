using LitobzorImport.DataAccess.Contracts;

namespace LitobzorImport.DataAccess
{
    public class DataConfiguration : IDataConfiguration
    {
        public string FolderPath { get; set; }
        public string ConnectionString { get; set; }
    }
}
