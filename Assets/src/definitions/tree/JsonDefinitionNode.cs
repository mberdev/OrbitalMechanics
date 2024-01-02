#nullable enable

using System.Collections.Generic;

namespace Assets.src.definitions
{
    public sealed class JsonDefinitionNode
    {
        public string Id { get; set; } = string.Empty;
        public string? DisplayName { get; set; }
        public string? Description { get; set; }
        public JsonMesh? Mesh { get; set; }
        public List<JsonFixedOrbitFunction>? FixedOrbitFunctions { get; set; }
        public List<JsonDefinitionNode>? Children { get; set; }
    }

}

