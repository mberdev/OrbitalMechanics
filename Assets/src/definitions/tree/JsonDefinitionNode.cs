#nullable enable

using Assets.src.definitions.tree.jsonConverters;
using Assets.src.orbitFunctions;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace Assets.src.definitions
{
    public sealed class JsonDefinitionNode
    {
        public string Id { get; set; } = string.Empty;
        public string? DisplayName { get; set; }
        public string? Description { get; set; }
        public JsonMesh? Mesh { get; set; }

        [JsonConverter(typeof(FixedOrbitFunctionsArrayConverter))]
        public IOrbitFunction[]? FixedOrbitFunctions { get; set; }
        public JsonDefinitionNode[]? Children { get; set; }
    }

}

