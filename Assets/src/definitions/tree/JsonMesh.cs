#nullable enable

using System.Collections.Generic;

namespace Assets.src.definitions
{
    public class JsonMesh
    {
        public string? Type { get; set; }

        public Dictionary<string, string>? Params { get; set; }
    }
}
