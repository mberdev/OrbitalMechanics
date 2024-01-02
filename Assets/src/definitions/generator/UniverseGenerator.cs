using Assets.src.orbitFunctions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.src.definitions.generator
{
    /// <summary>
    /// A basic class to quickly generate a universe with thousands of objects.
    /// </summary>
    internal class UniverseGenerator
    {
        public static System.Random rnd = new System.Random(0);

        public string[] DepthNames { get; } = new string[] { "sun", "planet", "satellite", "asteroid" };

        public int[] DepthCount = new int[] { 1, 10, 10, 10 };
        public float[] OrbitSemiMajor = new float[] { 0.0f, 20.0f, 5.0f, 2.0f };

        public (JsonDefinitionRoot root, int count) Generate()
        {
            int count = 0;
            if (DepthNames.Length != DepthCount.Length || DepthCount.Length != OrbitSemiMajor.Length)
            {
                Debug.LogError("Fix your terrible, terrible hard-coded arrays, mate");
                Application.Quit();
                return (null!, count);
            }

            var root = new JsonDefinitionRoot();
            var universe = new JsonDefinitionNode();
            universe.Id = "universe";
            universe.Mesh = null;
            universe.FixedOrbitFunctions = null;

            root.Universe = universe;
            JsonDefinitionNode sun = MakeSun(diameter: 5.0f);
            universe.Children = new JsonDefinitionNode[] { sun };
            count += 1;

            count += GenerateDepth(sun, depth: 1);

            return (root, count);
        }

        private static JsonDefinitionNode MakeSun(float diameter)
        {
            var sun = new JsonDefinitionNode();
            sun.Id = "sun";
            sun.FixedOrbitFunctions = new IOrbitFunction[]
            {
                MakeOffset("sun", 0)
            };
            AddSunMesh(sun, diameter);
            return sun;
        }

        private static void AddSunMesh(JsonDefinitionNode sun, float diameter)
        {
            sun.Mesh = new JsonMesh()
            {
                Type = "SUN",
                Params = new Dictionary<string, string>()
                {
                    { "diameter", $"{diameter}" },
                }
            };
        }

        private static void AddSphereMesh(JsonDefinitionNode parent, float diameter, Color color)
        {
            parent.Mesh = new JsonMesh()
            {
                Type = "COLORED_SPHERE",
                Params = new Dictionary<string, string>()
                {
                    { "diameter", $"{diameter}" },
                    { "color", $"#{ColorUtility.ToHtmlStringRGB(color)}" },
                }
            };
        }

        private int GenerateDepth(JsonDefinitionNode parent, int depth)
        {
            var count = 0;

            if (depth >= DepthCount.Length)
            {
                return 0;
            }

            var standardExcentricity = 0.3f;
            var standardDurationSeconds = 10;

            var parentDiameter = float.Parse(parent.Mesh.Params["diameter"]);

            var standardPeriapsis = parentDiameter * 2.0f;
            var standardApoasis = parentDiameter * 6.0f;

            var diameterRatio = 6.0f;
            var diameter = (parentDiameter / diameterRatio);

            var countToGenerate = DepthCount[depth];
            var name = DepthNames[depth];
            var semiMajor = OrbitSemiMajor[depth];

            parent.Children = Enumerable.Range(0, countToGenerate).Select(i =>
            {
                var randomDiameter = diameter + RandomPercent(diameter, 500); // add 0 to 500%

                // Randomly shift in time
                long meanLongitude = rnd.Next(0, 360) * 100000;
                var randomSemiMajor = semiMajor + RandomPercent(semiMajor, 300); // add 0 to 300%

                var randomExcentricity = standardExcentricity + RandomPercent(standardExcentricity, 100); // somewhere between 0.3 and 0.6?
                var randomDurationSeconds = standardDurationSeconds + RandomPercent(standardDurationSeconds, 300);
                var randomPeriapsis = standardPeriapsis + RandomPercent(standardPeriapsis, 300);
                var randomApoapsis = randomPeriapsis + RandomPercent(standardApoasis, 300);



                var child = new JsonDefinitionNode();
                child.Id = $"{name}{i}";
                child.FixedOrbitFunctions = new IOrbitFunction[]
                {
                    //MakeKepler(child.Id, i, randomSemiMajor, meanLongitude, randomExcentricity),
                    MakeLagueKepler(child.Id, i, randomPeriapsis, randomApoapsis, (int)randomDurationSeconds)
                };

                var color = RandomColor();
                AddSphereMesh(child, randomDiameter, color);

                // Recursion
                count += (1 + GenerateDepth(child, depth + 1));

                return child;
            }).ToArray();

            return count;

        }

        private static float RandomPercent(float value, int maxRandomPercents)
        {
            float randomPercents = ((float)rnd.Next(0, maxRandomPercents)) / 100.0f;
            return value * randomPercents;
        }
        private static Color RandomColor()
        {
            var r = rnd.Next(128, 255) / 256.0f;
            var g = rnd.Next(128, 255) / 256.0f;
            var b = rnd.Next(128, 255) / 256.0f;
            return new Color(r, g, b, 1.0f);
        }

        private static OffsetOrbitFunction MakeOffset(string id, int index)
        {
            return new OffsetOrbitFunction(id: $"{id}Offset{index}");

        }

        private static KeplerOrbitFunction MakeKepler(string id, int index, float semiMajor, long meanLongitude, float excentricity)
        {

            return new KeplerOrbitFunction(
            
                id: $"{id}Kepler{index}",
                offsetX: null,
                offsetY: null,
                offsetZ: null,
                orbiterMass: 10.0f, // TODO : decide.

                semiMajorAxis: semiMajor,
                excentricity,
                inclination: 0,
                longitudeOfAscendingNode: 10,
                argumentOfPeriapsis: 10,
                meanLongitude                
            );
        }

        private static LagueKeplerOrbitFunction MakeLagueKepler(string id, int index, float periapsis, float apoapsis, int durationSeconds)
        {

            return new LagueKeplerOrbitFunction(

                id: $"{id}Kepler{index}",
                periapsis,
                apoapsis,
                durationSeconds,

                offsetX: null,
                offsetY: null,
                offsetZ: null
            );
        }
    }
}
