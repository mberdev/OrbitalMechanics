using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.src.math
{
    /// <summary>
    /// A lot of this knowledge comes from https://www.youtube.com/watch?v=Ie5L8Nz1Ns0
    /// Credits : "Street´lights behind the trees"
    /// </summary>
    public class Kepler
    {
        public static float GravitationConstant = 6.674f;

        public float OrbiterMass { get; }
        public float SemiMajorAxis { get; }
        public float Excentricity { get; }
        public float Inclination { get; }
        public float LongitudeOfAscendingNode { get; }
        public float ArgumentOfPeriapsis { get; }

        // represents a time shift
        public long MeanLongitude { get; }



        // Semi-constants
        public float MeanAngularMotion { get; private set; }
        public float TrueAnomalyConstant { get; private set; }
        public float Mu { get; private set; }


        public Kepler(float orbiterMass, float semiMajorAxis, float excentricity, float inclination, float longitudeOfAscendingNode, float argumentOfPeriapsis, long meanLongitude)
        {
            OrbiterMass = orbiterMass;
            SemiMajorAxis = semiMajorAxis;
            Excentricity = excentricity;
            Inclination = inclination;
            LongitudeOfAscendingNode = longitudeOfAscendingNode;
            ArgumentOfPeriapsis = argumentOfPeriapsis;
            MeanLongitude = meanLongitude;

            // Semi-constants
            Mu = GravitationConstant * OrbiterMass;

            if (Mu == 0)
            {
                Debug.LogError("Kepler : Mu is 0 which means there's a massless object");
                Application.Quit();
            }
            MeanAngularMotion = Mathf.Sqrt(Mu / Mathf.Pow(SemiMajorAxis, 3));
            TrueAnomalyConstant = Mathf.Sqrt((1 + Excentricity) / (1 - Excentricity));

        }

        // Intermediate container for values that depend on time
        public class Snapshot2D
        {
            private readonly float _approximateExcentricAnomaly;

            public float TrueAnomaly { get; }
            public float Distance { get; }
            public Kepler K { get; }
            public long TimeMs { get; }

            // default ctor
            public Snapshot2D(Kepler k, long timeMs) {
                K = k;
                TimeMs = timeMs;

                // Semi-constants
                var meanAnomaly = K.MeanAngularMotion * (timeMs - K.MeanLongitude);
                _approximateExcentricAnomaly = ExcentricAnomaly.Approximate(K.Excentricity, meanAnomaly);

                TrueAnomaly = 2 * Mathf.Atan(K.TrueAnomalyConstant * Mathf.Tan(_approximateExcentricAnomaly / 2));

                Distance = K.SemiMajorAxis * (1 - K.Excentricity * Mathf.Cos(_approximateExcentricAnomaly));
            }

            // Simple 2D (TODO : 3D)
            public float X => Distance * Mathf.Cos(TrueAnomaly);

            // Simple 2D (TODO : 3D)
            public float Z => Distance * Mathf.Sin(TrueAnomaly);
            
        }


        private static class ExcentricAnomaly
        {
            public static float Approximate(float excentricity, float meanAnomaly)
            {
                float accuracyTolerance = 0.0001f;
                int maxIterations = 10; // 3 to 5 is usually good?

                float E1 = meanAnomaly;
                float difference = 1f;
                for (int i = 0; difference > accuracyTolerance && i < maxIterations; i++)
                {
                    float E0 = E1;
                    E1 = E0 - ExcentricAnomaly.F(E0, excentricity, meanAnomaly) / ExcentricAnomaly.DF(E0, excentricity);
                    difference = Mathf.Abs(E1 - E0);
                }
                float eccentricAnomaly = E1;

                return eccentricAnomaly;
            }

            public static float F(float E, float e, float M) 
                => M-E+ e * Mathf.Sin(E);

            public static float DF(float E, float e)
                => -1 + e * Mathf.Cos(E);
        }
    }
}
