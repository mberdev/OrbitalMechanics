#nullable enable 

using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
namespace Assets.src.IO
{

    public sealed class FilesLoader
    {
        public static string PathToSubFolder(string path1, string? path2 = null, string? path3 = null)
        {
            // Get current execution folder
            var currentFolder = System.IO.Directory.GetCurrentDirectory();
            var path = System.IO.Path.Combine(currentFolder, path1);
            if (path2 != null)
            {
                path = System.IO.Path.Combine(path, path2);
            }
            if (path3 != null)
            {
                path = System.IO.Path.Combine(path, path3);
            }

            return path;

        }
        public static async Task<string[]> LoadAllJsonFilesAsync(string path1, string? path2 = null, string? path3 = null)
        {
            var directoryPath = PathToSubFolder(path1, path2, path3);

            var jsonFiles = Directory
                .GetFiles(directoryPath)
                .Where(f => f.EndsWith(".json"))
                .ToArray();

            return await LoadFilesAsync(jsonFiles);
        }

        public static async Task<string[]> LoadFilesAsync(string[] filesPaths)
        {
            // TODO : Parallelize
            var tasks =  filesPaths.Select(filePath => File.ReadAllTextAsync(filePath));

            // TODO: Find out why WhenAll hangs Unity.
            //return await Task.WhenAll(tasks);
            return tasks.Select(t => t.GetAwaiter().GetResult()).ToArray();
        }
    }
}
