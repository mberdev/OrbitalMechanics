using Assets.src.definitions.tree;
using Assets.src.math;
using Assets.src.orbitFunctions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.src.definitions.converters
{
    internal static class FixedOrbitFunctionsConverter
	{

        public static OffsetOrbitFunction ToOffsetFunction(JsonFixedOrbitFunction function)
        {
            var offset = new Vector3(
                function.OffsetX ?? 0,
                function.OffsetY ?? 0,
                function.OffsetZ ?? 0
            );
            var f = new OffsetOrbitFunction(function.Id, offset);

            return f;
        }


        public static EllipsisXZOrbitFunction ToEllipsisXZFunction(JsonFixedOrbitFunction function)
        {
            var offset = new Vector3(
                function.OffsetX ?? 0,
                function.OffsetY ?? 0,
                function.OffsetZ ?? 0
            );
            if (function.Params == null)
            {
                Debug.LogError($"{nameof(ToEllipsisXZFunction)}: {nameof(JsonFixedOrbitFunction.Params)} is missing in function {function.Id}");
                Application.Quit(); // TODO: better error handling.
            }

            if (!function.Params.TryGetFloat("horizontalAxisX", out var horizontalAxisX)
                || horizontalAxisX == 0)
            {
                Debug.LogError($"{nameof(ToEllipsisXZFunction)}: {nameof(horizontalAxisX)} is missing in function {function.Id}");
                Application.Quit(); // TODO: better error handling.
            }

            if (!function.Params.TryGetFloat("verticalAxisZ", out var verticalAxisZ)
                           || verticalAxisZ == 0)
            {
                Debug.LogError($"{nameof(ToEllipsisXZFunction)}: {nameof(verticalAxisZ)} is missing in function {function.Id}");
                Application.Quit(); // TODO: better error handling.
            }

            if (!function.Params.TryGetLong("durationMs", out var durationMs)
                || durationMs == 0)
            {
                Debug.LogError($"{nameof(ToEllipsisXZFunction)}: {nameof(durationMs)} is missing in function {function.Id}");
                Application.Quit(); // TODO: better error handling.
            }

            var f = new EllipsisXZOrbitFunction(function.Id, offset, horizontalAxisX, verticalAxisZ, durationMs);

            return f;
        }


        public static KeplerOrbitFunction ToKeplerFunction(JsonFixedOrbitFunction function)
        {
            var offset = new Vector3(
                function.OffsetX ?? 0,
                function.OffsetY ?? 0,
                function.OffsetZ ?? 0
            );

            if (function.Params == null)
            {
                Debug.LogError($"{nameof(ToKeplerFunction)}: {nameof(JsonFixedOrbitFunction.Params)} is missing in function {function.Id}");
                Application.Quit(); // TODO: better error handling.
            }

            if (!function.Params!.TryGetFloats(new List<string>
        {
            "semiMajorAxis",
            "excentricity",
            "inclination",
            "longitudeOfAscendingNode",
            "argumentOfPeriapsis",
            "meanLongitude",
        }, out var values))
            {
                Debug.LogError($"{nameof(ToKeplerFunction)}: some parameters missing missing in function {function.Id}");
                Application.Quit(); // TODO: better error handling.
            }


            if (values["semiMajorAxis"] == 0.0f)
            {
                Debug.LogError($"{nameof(ToKeplerFunction)}: {nameof(Kepler.SemiMajorAxis)} is missing in function {function.Id}");
                Application.Quit(); // TODO: better error handling.
            }

            // That's a time offset
            function.Params!.TryGetLong("meanLongitude", out var meanLongitudeMs);

            var kepler = new Kepler(
                orbiterMass: 1.0f,
                values["semiMajorAxis"],
                values["excentricity"],
                values["inclination"],
                values["longitudeOfAscendingNode"],
                values["argumentOfPeriapsis"],
                meanLongitudeMs
            );

            var f = new KeplerOrbitFunction(function.Id, offset, kepler);

            return f;
        }
    }
}
