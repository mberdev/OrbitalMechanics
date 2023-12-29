using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.src.orbitFunctions
{
    internal class OffsetOrbitFunction : IOrbitFunction
    {
        public string Id { get; }
        public Vector3 Offset { get; }

        public string Type => "OFFSET";

        public OffsetOrbitFunction(string id, Vector3 offset)
        {
            Id = id;
            Offset = offset;
        }

    }
}
