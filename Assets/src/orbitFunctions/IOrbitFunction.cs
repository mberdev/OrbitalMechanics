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
        public Vector3 Offset { get; }
    }
}
