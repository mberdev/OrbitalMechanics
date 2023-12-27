#nullable enable

using System.Collections.Generic;

namespace Assets.src.definitions
{
    public sealed class DefinitionNode
    {
        public string Id { get; set; } = string.Empty;
        public string? DisplayName { get; set; }
        public string? Description { get; set; }
        public List<FixedOrbitFunction>? FixedOrbitFunctions { get; set; }
        public List<DefinitionNode>? Children { get; set; }
    }

}

