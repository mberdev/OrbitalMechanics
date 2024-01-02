//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using UnityEngine;

//namespace Assets.src.math
//{
//    /// <summary>
//    /// A lot of this knowledge comes from https://www.youtube.com/watch?v=UXD97l7ZT0w&t=1319s
//    /// </summary>
//    public class LagueKepler
//    {
//        public static float GravitationConstant = 6.674f;




//        public LagueKepler(double periapsis, double apoapsis)
//        {
//            CalculateSemiConstants(periapsis, apoapsis);
//        }

//        #region semi constants
//        public double SemiMajorLength { get; private set; }
//        public double LinearEccentricity { get; private set; }
//        public double Eccentricity { get; private set; }
//        public double SemiMinorLength { get; private set; }

//        private void CalculateSemiConstants(double periapsis, double apoapsis)
//        {
//            SemiMajorLength = (periapsis + apoapsis) / 2;
//            LinearEccentricity = SemiMajorLength - periapsis; // distance between center of ellipse and focus
//            Eccentricity = LinearEccentricity / SemiMajorLength; // from circle to increasingly elliptical

//            if (Eccentricity <= 0 || Eccentricity >= 0.5)
//            {
//                throw new Exception("Eccentricity value not supported");
//            }
//            SemiMinorLength = Math.Sqrt(Math.Pow(SemiMajorLength, 2) - Math.Pow(LinearEccentricity, 2));
        
//        }
//        #endregion

//        public long X_FromAngleRadiants(Vector2 centreOfMass, double angleRadiants)
//        {
//            var ellipseCenterX = centreOfMass.x + LinearEccentricity;
//            //EllipseCenterY = centreOfMass.y;
//            return (long)(SemiMajorLength * Math.Cos(angleRadiants) + ellipseCenterX);
//        }

//        public long Y_FromAngleRadiants(Vector2 centreOfMass, double angleRadiants)
//        {
//            var ellipseCenterY = centreOfMass.y;
//            return (long)(SemiMajorLength * Math.Sin(angleRadiants) + ellipseCenterY);
//        }
//    }
//}
