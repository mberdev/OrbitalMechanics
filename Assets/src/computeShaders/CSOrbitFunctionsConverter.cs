using Assets.src.orbitFunctions;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.src.computeShaders.converters
{
    internal static class CSOrbitFunctionsConverter
    {
        public static CSOrbitFunction[] Convert(IOrbitFunction[] functions)
        {
            List<CSOrbitFunction> result = new();

            foreach(var toConvert in functions)
            {
                var f = new CSOrbitFunction();
                f.previous = -1;

                switch (toConvert.TypeEnum) {
                    case OrbitTypes.OFFSET:
                        f.type = OrbitTypes.OFFSET;
                        f.offsetX = toConvert.OffsetX ?? 0;
                        f.offsetY = toConvert.OffsetY ?? 0;
                        f.offsetZ = toConvert.OffsetZ ?? 0;
                        result.Add(f);
                        break;
                    case OrbitTypes.LAGUE_KEPLER:
                        f.type = OrbitTypes.LAGUE_KEPLER;
                        f.offsetX = toConvert.OffsetX ?? 0;
                        f.offsetY = toConvert.OffsetY ?? 0;
                        f.offsetZ = toConvert.OffsetZ ?? 0;
                        f.apoapsis = ((LagueKeplerOrbitFunction)toConvert).Apoapsis;
                        f.periapsis = ((LagueKeplerOrbitFunction)toConvert).Periapsis;
                        f.durationSeconds = ((LagueKeplerOrbitFunction)toConvert).DurationSeconds;
                        result.Add(f);
                        break;
                    default:
                        Debug.LogError("Missing Shader conversion for orbit function type: " + toConvert.TypeEnum);
                        Debug.Break();
                        Application.Quit();
                        break;
                }   
            }

            return result.ToArray();
        }
    }
}
