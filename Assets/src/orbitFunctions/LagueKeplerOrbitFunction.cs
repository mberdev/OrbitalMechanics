using Newtonsoft.Json;
using System;
using UnityEngine;

namespace Assets.src.orbitFunctions
{
    // Will be used by compute shaders too
    internal interface ILagueKeplerOrbitFunction
    {
        public float? OffsetX { get; }
        public float? OffsetY { get; }
        public float? OffsetZ { get; }

        public float Periapsis { get; }
        public float Apoapsis { get; }
        public int DurationSeconds { get; }
    }

    public class LagueKeplerOrbitFunction : IOrbitFunction, ILagueKeplerOrbitFunction
    {
        public string Id { get; }

        public float? OffsetX { get; }
        public float? OffsetY { get; }
        public float? OffsetZ { get; }
        public float Periapsis { get; }
        public float Apoapsis { get; }
        public int DurationSeconds { get; }

        public OrbitTypes TypeEnum => OrbitTypes.LAGUE_KEPLER;

        public const string StaticTypeStr = nameof(OrbitTypes.LAGUE_KEPLER);
        public string Type => StaticTypeStr;

        [JsonConstructor]
        public LagueKeplerOrbitFunction(
            string id,
            float periapsis, 
            float apoapsis,
            int durationSeconds,
            float? offsetX = null,
            float? offsetY = null,
            float? offsetZ = null
        )
        {
            Id = id;
            OffsetX = offsetX;
            OffsetY = offsetY;
            OffsetZ = offsetZ;

            if (durationSeconds <= 0)
            {
                throw new Exception($"{nameof(LagueKeplerOrbitFunction)} ({id}) : missing positive {nameof(durationSeconds)}");
            }

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
            DurationSeconds = durationSeconds;

            CalculateSemiConstants();
        }



        #region semi constants
        public double SemiMajorLength { get; private set; }
        public double LinearEccentricity { get; private set; }
        public double Eccentricity { get; private set; }
        public double SemiMinorLength { get; private set; }
        public double RadiantsPerSecond { get; private set; }

        private void CalculateSemiConstants()
        {
            SemiMajorLength = (Periapsis + Apoapsis) / 2.0;
            LinearEccentricity = SemiMajorLength - Periapsis; // distance between center of ellipse and focus
            Eccentricity = LinearEccentricity / SemiMajorLength; // from circle to increasingly elliptical

            // 0 is a perfect circle, 1 turns the ellipse into a parabola
            if (Eccentricity < 0 || Eccentricity >= 1.0)
            {
                throw new Exception($"Eccentricity value not supported: {Eccentricity}");
            }
            SemiMinorLength = Math.Sqrt(Math.Pow(SemiMajorLength, 2) - Math.Pow(LinearEccentricity, 2));

            RadiantsPerSecond = 2.0 * Math.PI / (double)DurationSeconds;
        }
        #endregion

        public float X_FromAngleRadiants(Vector2 centreOfMass, double angleRadiants)
        {
            var ellipseCenterX = centreOfMass.x + LinearEccentricity;
            var x = SemiMajorLength * Math.Cos(angleRadiants) + ellipseCenterX;
            //if (x > float.MaxValue)
            //{
            //    throw new Exception($"Kepler orbit: X value too large: {x}");
            //}
            return (float)x;
        }

        public float Y_FromAngleRadiants(Vector2 centreOfMass, double angleRadiants)
        {
            var ellipseCenterY = centreOfMass.y;
            var y = SemiMajorLength * Math.Sin(angleRadiants) + ellipseCenterY;
            //if (y > float.MaxValue)
            //{
            //    throw new Exception($"Kepler orbit: Y value too large: {y}");
            //}
            return (float)y;
        }

    }
}
