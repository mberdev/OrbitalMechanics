#nullable enable

using System.Collections.Generic;

namespace Assets.src.definitions
{
    public class FixedOrbitFunction
    {
        public string Type { get; set; } = string.Empty;
        public int OffsetX { get; set; }
        public int OffsetY { get; set; }
        public int OffsetZ { get; set; }

        public Dictionary<string, string>? Params { get; set; }
    }
}
