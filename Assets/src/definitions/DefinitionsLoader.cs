
using Assets.src.IO;
using System.Threading.Tasks;

namespace Assets.src.definitions
{
    public sealed class DefinitionsLoader
    {
        public async Task<JsonDefinitionRoot[]> LoadAllDefinitionsAsync()
        {

            var jsonDatas = await FilesLoader.LoadAllJsonFilesAsync("Assets", "Universes");
            return await DefinitionsParser.ParseAsync(jsonDatas);
        }
    }

}
