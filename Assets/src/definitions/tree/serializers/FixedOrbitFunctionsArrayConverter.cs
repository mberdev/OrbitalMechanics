#nullable enable

using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.src.orbitFunctions;
using UnityEngine;
using System.Xml.Linq;

namespace Assets.src.definitions.tree.jsonConverters
{
    internal class FixedOrbitFunctionsArrayConverter : JsonConverter
    {
    
        public override bool CanConvert(Type objectType)
        {
            return objectType is IOrbitFunction;
        }

        public override object? ReadJson(JsonReader reader, Type objectType, object? existingValue, JsonSerializer serializer)
        {
            JArray ja = JArray.Load(reader);
            List<IOrbitFunction> objects = new();

            var orbitTypeFieldName = ToFakeCamelCase(nameof(IOrbitFunction.Type));

            foreach (JObject jo in ja)
            {
                try
                {
                    if (!jo.TryGetValue(orbitTypeFieldName, out var typeToken))
                    {
                        // TODO: Proper error handling
                        Debug.LogError($"Json is missing field '{orbitTypeFieldName}'. Json was : {jo}");
                        continue;
                    }

                    IOrbitFunction? f;
                    switch (typeToken.Value<string>())
                    {
                        case OffsetOrbitFunction.StaticTypeStr:
                            if (!TryDeserialize<OffsetOrbitFunction>(jo, serializer, out f))
                            {
                                continue;
                            }
                            break;

                        case EllipsisXZOrbitFunction.StaticTypeStr:
                            if (!TryDeserialize<EllipsisXZOrbitFunction>(jo, serializer, out f))
                            {
                                continue;
                            }
                            break;

                        case KeplerOrbitFunction.StaticTypeStr:
                            if (!TryDeserialize<KeplerOrbitFunction>(jo, serializer, out f))
                            {
                                continue;
                            }
                            break;

                        case LagueKeplerOrbitFunction.StaticTypeStr:
                            if (!TryDeserialize<LagueKeplerOrbitFunction>(jo, serializer, out f))
                            {
                                continue;
                            }
                            break;

                        default:
                            throw new Exception("Type field not found.");
                    }

                    objects.Add(f!);
                }
                catch (Exception)
                {
                    Debug.LogError($"Failed to deserialize {jo} to {nameof(IOrbitFunction)}");
                    continue;
                }
            }

            return objects.ToArray();
        }

        private static bool TryDeserialize<T>(JObject jo, JsonSerializer serializer, out IOrbitFunction? f) where T : IOrbitFunction
        {
            try
            {
                f = jo.ToObject<T>(serializer);

                if (f == null)
                    return false;

                if (string.IsNullOrEmpty(f.Id))
                {
                    Debug.LogError($"{nameof(FixedOrbitTimeTracker)}: Must have an id! Object {jo}");
                    return false;
                }
                return true;
            }
            catch (Exception)
            {
                Debug.LogError($"Failed to deserialize {jo} to {nameof(IOrbitFunction)}");
            }

            f = null;
            return false;
        }

        /// <summary>
        /// Almost standard camel case, apart from the weird rules regarding acronyms
        /// </summary>
        public static string ToFakeCamelCase(string str)
        {
            if (!string.IsNullOrEmpty(str) && str.Length > 1)
            {
                return char.ToLowerInvariant(str[0]) + str.Substring(1);
            }
            else
            {
                return str;
            }
        }

        public override void WriteJson(JsonWriter writer, object? value, JsonSerializer serializer)
        {
            throw new NotImplementedException(); // Implement this if you need serialization back to JSON
        }
    }
}
