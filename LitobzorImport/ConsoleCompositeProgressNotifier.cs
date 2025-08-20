using LitobzorImport.Contracts;

namespace LitobzorImport.Logic
{
    public class ConsoleCompositeProgressNotifier : IProgressNotifier
    {
        private readonly List<string> delayedMessages = [];

        public void NotifyProgress(string message, string type)
        {
            switch (type)
            {
                case "batchSuccess":
                case "fileSuccess":
                case "readFileError":
                case "message":
                case "imported":
                case "statistics":
                    Console.WriteLine(message);
                    break;
                case "lineError":
                case "createError":
                    delayedMessages.Add(message);
                    break;
                default:
                    goto case "message";
            }
        }

        public void WriteDelayed()
        {
            foreach (var message in delayedMessages)
            {
                Console.WriteLine(message);
            }
        }
    }
}
