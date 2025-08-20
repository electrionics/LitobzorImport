namespace LitobzorImport.DataAccess.Contracts;

public interface IDataConfiguration
{
    public string FolderPath { get; set; }

    public string ConnectionString { get; set; }
}
