#nullable enable

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.src.orbitFunctions
{
    public interface IOrbitFunction
    {
        public string Id { get; }
        public string Type { get; }
        public OrbitTypes TypeEnum { get; }

        public float? OffsetX { get; }
        public float? OffsetY { get; }
        public float? OffsetZ { get; }
    }
}
