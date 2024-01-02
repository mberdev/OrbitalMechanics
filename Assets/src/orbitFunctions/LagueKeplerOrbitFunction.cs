using Newtonsoft.Json;
using System;
using UnityEngine;

namespace Assets.src.orbitFunctions
{
    public class LagueKeplerOrbitFunction : IOrbitFunction
    {
        public string Id { get; }

        public float? OffsetX { get; }
        public float? OffsetY { get; }
        public float? OffsetZ { get; }
        public double Periapsis { get; }
        public double Apoapsis { get; }

        public const string StaticType = "LAGUE_KEPLER";

        public string Type => StaticType;

        [JsonConstructor]
        public LagueKeplerOrbitFunction(
            string id,
            double periapsis, 
            double apoapsis,
            float? offsetX = null,
            float? offsetY = null,
            float? offsetZ = null
        )
        {
            Id = id;
            OffsetX = offsetX;
            OffsetY = offsetY;
            OffsetZ = offsetZ;

            if (periapsis <= 0 || apoapsis <= 0 )
            {
                throw new Exception($"{nameof(LagueKeplerOrbitFunction)} ({id}) : missing positive {nameof(periapsis)} or {nameof(apoapsis)}");
            }

            if (apoapsis < periapsis)
            {
                throw new Exception($"{nameof(LagueKeplerOrbitFunction)} ({id}) :  {nameof(periapsis)} must be smaller than {nameof(apoapsis)}");
            }

            Periapsis = periapsis;
            Apoapsis = apoapsis;

            CalculateSemiConstants();
        }



        #region semi constants
        public double SemiMajorLength { get; private set; }
        public double LinearEccentricity { get; private set; }
        public double Eccentricity { get; private set; }
        public double SemiMinorLength { get; private set; }

        private void CalculateSemiConstants()
        {
            SemiMajorLength = (Periapsis + Apoapsis) / 2;
            LinearEccentricity = SemiMajorLength - Periapsis; // distance between center of ellipse and focus
            Eccentricity = LinearEccentricity / SemiMajorLength; // from circle to increasingly elliptical

            if (Eccentricity <= 0 || Eccentricity >= 1.0)
            {
                throw new Exception($"Eccentricity value not supported: {Eccentricity}");
            }
            SemiMinorLength = Math.Sqrt(Math.Pow(SemiMajorLength, 2) - Math.Pow(LinearEccentricity, 2));

        }
        #endregion

        public long X_FromAngleRadiants(Vector2 centreOfMass, double angleRadiants)
        {
            var ellipseCenterX = centreOfMass.x + LinearEccentricity;
            //EllipseCenterY = centreOfMass.y;
            return (long)(SemiMajorLength * Math.Cos(angleRadiants) + ellipseCenterX);
        }

        public long Y_FromAngleRadiants(Vector2 centreOfMass, double angleRadiants)
        {
            var ellipseCenterY = centreOfMass.y;
            return (long)(SemiMajorLength * Math.Sin(angleRadiants) + ellipseCenterY);
        }

    }
}
