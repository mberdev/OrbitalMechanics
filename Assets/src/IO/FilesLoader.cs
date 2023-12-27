using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
namespace Assets.src.IO
{

    public sealed class FilesLoader
    {
        public static string[] LoadFiles(string directoryPath)
        {

            var jsonFiles = Directory
                .GetFiles(directoryPath)
                .Where(f => f.EndsWith(".json"))
                .ToArray();

            return LoadFiles(jsonFiles);
        }

        public static string[] LoadFiles(string[] filesPaths)
        {
            // TODO : Parallelize
            return filesPaths.Select(filePath => File.ReadAllText(filePath)).ToArray();
        }
    }
}
