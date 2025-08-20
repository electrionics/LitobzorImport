namespace LitobzorImport.Contracts
{
    public interface IProgressNotifier
    {
        void NotifyProgress(string message, string type);

        void WriteDelayed();
    }
}