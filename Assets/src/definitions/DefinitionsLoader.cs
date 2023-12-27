
using Assets.src.IO;

namespace Assets.src.definitions
{
    public sealed class DefinitionsLoader
    {
        private readonly string _definitionsPath;

        public DefinitionsLoader(string definitionsPath)
        {
            _definitionsPath = definitionsPath;
        }

        public DefinitionRoot[] LoadAllDefinitions()
        {
            var json = FilesLoader.LoadFiles(_definitionsPath);
            return DefinitionsParser.Parse(json);
        }
    }

}
