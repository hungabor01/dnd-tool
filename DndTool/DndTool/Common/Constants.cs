namespace DndTool.Common
{
    public class Constants
    {
        public class Persistence
        {
            public const string CampaignFileExtension = ".campaign";
            public const string SaveFilesDirectory = "SaveFiles";

            public static readonly string SaveFilesPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, SaveFilesDirectory);
        }

        public class Sessions
        {
            public const int DefaultSessionIndex = -1;
        }
    }
}
