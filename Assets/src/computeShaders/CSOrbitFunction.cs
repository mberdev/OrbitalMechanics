using Assets.src.orbitFunctions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Assets.src.computeShaders
{
    [StructLayout(LayoutKind.Explicit)]
    public struct CSOrbitFunction
    {
        [FieldOffset(0)]
        public OrbitTypes type; // sizeof(enum-int) == 4

        #region IOffsetOrbitFunction Members
        [FieldOffset(4)]
        public float offsetX;
        [FieldOffset(8)] // sizeof(float) == 4
        public float offsetY;
        [FieldOffset(12)]
        public float offsetZ;
        #endregion

        #region output
        [FieldOffset(16)]
        public float outX;
        [FieldOffset(20)] // sizeof(float) == 4
        public float outY;
        [FieldOffset(24)]
        public float outZ;
        #endregion

        #region ILagueKeplerOrbitFunction Members
        [FieldOffset(28)]
        public float periapsis;


        [FieldOffset(32)] // sizeof(double) == 8
        public float apoapsis;

        [FieldOffset(36)]
        public int durationSeconds;
        #endregion

        [FieldOffset(40)]
        public int previous; // linked list

        [FieldOffset(44)]
        public int id;

        public static int SizeInBytes = 48;
    }
}
