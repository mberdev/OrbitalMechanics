#nullable enable

using System.Collections.Generic;

namespace Assets.src.definitions
{
    public class JsonFixedOrbitFunction
    {
        public string? Id { get; set; }
        public string? Type { get; set; }
        public int? OffsetX { get; set; }
        public int? OffsetY { get; set; }
        public int? OffsetZ { get; set; }

        public Dictionary<string, string>? Params { get; set; }
    }
}
