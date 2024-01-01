using Assets.src.math;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.src.orbitFunctions
{
    public class KeplerOrbitFunction: IOrbitFunction
    {
        public string Id { get; }

        public Vector3 Offset { get; }
        public Kepler Kepler { get; }

        public string Type => "KEPLER";

        public KeplerOrbitFunction(string id, Vector3 offset, Kepler kepler)
        {
            Id = id;
            Offset = offset;
            Kepler = kepler;
        }
        
    }
}
