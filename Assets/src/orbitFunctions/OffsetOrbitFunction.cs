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
    // Will be used by compute shaders too
    internal interface IOffsetOrbitFunction
    {
        public float? OffsetX { get; }
        public float? OffsetY { get; }
        public float? OffsetZ { get; }
    }

    internal class OffsetOrbitFunction : IOrbitFunction, IOffsetOrbitFunction
    {
        public string Id { get; }
        public float? OffsetX { get; }
        public float? OffsetY { get; }
        public float? OffsetZ { get; }

        public OrbitTypes TypeEnum => OrbitTypes.OFFSET;

        public const string StaticTypeStr = nameof(OrbitTypes.OFFSET);
        public string Type => StaticTypeStr;


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
