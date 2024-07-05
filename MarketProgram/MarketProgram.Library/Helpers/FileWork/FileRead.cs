using System.Configuration;
using System.Text.Json;

namespace MarketProgram.Library.Helpers.FileWork
{
    public static class FileReadClass
    {
        public static T? FileRead<T>(string fileWritePath)
        {

            string path;

            path = ConfigurationManager.AppSettings[fileWritePath]!;


            if (!File.Exists(path))
                return default(T?);

            string json = File.ReadAllText(path);

            T? some = JsonSerializer.Deserialize<T>(json);

            return some;
        }
    }
}
