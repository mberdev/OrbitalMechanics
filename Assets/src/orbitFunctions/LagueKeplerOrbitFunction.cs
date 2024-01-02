using Newtonsoft.Json;

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
            float? offsetX,
            float? offsetY,
            float? offsetZ,

            double periapsis, 
            double apoapsis
            )
        {
            Id = id;
            OffsetX = offsetX;
            OffsetY = offsetY;
            OffsetZ = offsetZ;

            Periapsis = periapsis;
            Apoapsis = apoapsis;
        }
        
    }
}
