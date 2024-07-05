using System.Configuration;
using System.Text.Json;

namespace MarketProgram.Library.Helpers.FileWork
{
    public static class FileSaveClass
    {
        public static void FileSave<T>(T some, string fileWritePath)
        {
            string path;

            path = ConfigurationManager.AppSettings[fileWritePath]!;

            string json = JsonSerializer.Serialize(some);

            File.WriteAllText(path, json);
        }
    }
}
