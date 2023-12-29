
using Assets.src.IO;

namespace Assets.src.definitions
{
    public sealed class DefinitionsLoader
    {
        public JsonDefinitionRoot[] LoadAllDefinitions()
        {

            var json = FilesLoader.LoadAllJsonFiles("Assets", "Universes");
            return DefinitionsParser.Parse(json);
        }
    }

}
