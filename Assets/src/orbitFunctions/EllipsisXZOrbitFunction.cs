using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.src.orbitFunctions
{
    internal class EllipsisXZOrbitFunction : IOrbitFunction
    {
        // TODO: Start time offset

        public string Id { get; }
        public Vector3 Offset { get; }
        public float HorizontalAxisX { get; }
        public float VerticalAxisZ { get; }
        public float Duration { get; }

        public string Type => "ELLIPSIS_XZ";

        public EllipsisXZOrbitFunction(string id, Vector3 offset, float horizontalAxisX, float horizontalAxisZ, float duration)
        {
            Id = id;
            Offset = offset;
            HorizontalAxisX = horizontalAxisX;
            VerticalAxisZ = horizontalAxisZ;
            Duration = duration;
        }

    }
}
