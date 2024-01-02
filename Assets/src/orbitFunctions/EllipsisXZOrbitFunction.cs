using Newtonsoft.Json;
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
        public float? OffsetX { get; }
        public float? OffsetY { get; }
        public float? OffsetZ { get; }
        public float HorizontalAxisX { get; }
        public float VerticalAxisZ { get; }
        public float DurationMs { get; }

        public const string StaticType = "ELLIPSIS_XZ";
        public string Type => StaticType;

        [JsonConstructor]
        public EllipsisXZOrbitFunction(
            string id,
            float? offsetX, 
            float? offsetY, 
            float? offsetZ,
            float horizontalAxisX, 
            float verticalAxisZ, 
            float durationMs)
        {
            Id = id;
            OffsetX = offsetX;
            OffsetY = offsetY;
            OffsetZ = offsetZ;
            
            HorizontalAxisX = horizontalAxisX;
            VerticalAxisZ = verticalAxisZ;
            DurationMs = durationMs;
        }

        public float LerpEllipseX(long timeMs)
        {
            return (HorizontalAxisX * Mathf.Cos((timeMs / DurationMs) * Mathf.PI * 2));
        }

        public float LerpEllipseZ(long timeMs)
        {
            return (VerticalAxisZ * Mathf.Sin((timeMs / DurationMs) * Mathf.PI * 2));
        }

    }
}
