using Assets.src.definitions;
using Assets.src.state2;


namespace Assets.src
{
    public sealed class Engine
    {
        public static Engine Instance { get; } = new Engine();

        public Time Time { get; } = new Time();

        public DefinitionRoot Definition { get; private set;  }

        public GlobalSnapshot InitialSnapshop { get; private set;  } 


        private void LoadDefinitions()
        {
            // Get current execution folder
            var currentFolder = System.IO.Directory.GetCurrentDirectory();
            var definitionsFolder = System.IO.Path.Combine(currentFolder, "Assets", "Universes");

            var definitions = new DefinitionsLoader(definitionsFolder).LoadAllDefinitions();
            Definition = definitions[0]; // TODO : ability to choose which.
        }

        private Engine()
        {
            LoadDefinitions();
            InitialSnapshop = GlobalSnapshot.CreateInitial(Definition);
        }
    }
}
