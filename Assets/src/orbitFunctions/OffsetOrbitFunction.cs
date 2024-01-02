using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using UnityEngine;

namespace Assets.src.orbitFunctions
{
    internal class OffsetOrbitFunction : IOrbitFunction
    {
        public string Id { get; }
        public float? OffsetX { get; }
        public float? OffsetY { get; }
        public float? OffsetZ { get; }

        public const string StaticType = "OFFSET";
        public string Type => StaticType;

        [JsonConstructor]
        public OffsetOrbitFunction(string id, float? offsetX = null, float? offsetY = null, float? offsetZ = null)
        {
            Id = id;
            OffsetX = offsetX;
            OffsetY = offsetY;
            OffsetZ = offsetZ;
        }

    }
}
