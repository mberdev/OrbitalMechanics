using Assets.src.definitions.tree.jsonConverters;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Linq;
using System.Threading.Tasks;

namespace Assets.src.definitions
{


    public sealed class DefinitionsParser
    {
        public static async Task<JsonDefinitionRoot[]> ParseAsync(string[] json)
        {

            //TODO: Parallelize and/or async (damn Newtonsoft!)
            return json.ToList().Select(data =>
            {
                var settings = new JsonSerializerSettings
                {
                    ContractResolver = new DefaultContractResolver
                    {
                        NamingStrategy = new CamelCaseNamingStrategy()
                    },
                };

                // Custom converters
                settings.Converters.Add(new FixedOrbitFunctionsArrayConverter());

                return JsonConvert.DeserializeObject<JsonDefinitionRoot>(data, settings);
            }).ToArray();

        }

        // System.Text.Json , synchronous
        //public static DefinitionRoot[] Parse(string[] json)
        //{

        //    return json.ToList().Select(data =>
        //    {
        //        var options = new JsonSerializerOptions
        //        {
        //            PropertyNameCaseInsensitive = true
        //        };
        //        return JsonSerializer.Deserialize<DefinitionRoot>(data, options);
        //    }).ToArray();

        //}

        // System.Text.Json , Asynchronous
        //public static DefinitionRoot[] ParseAsync(Stream[] jsonStreams)
        //{

        //    var tasks = jsonStreams.Select(stream =>
        //    {
        //        var options = new JsonSerializerOptions
        //        {
        //            PropertyNameCaseInsensitive = true
        //        };
        //        return System.Text.Json.JsonSerializer.DeserializeAsync<DefinitionRoot>(stream, options);
        //    });


        //    //    return Task.WhenAll(tasks.Select(t => t.AsTask()))
        //              .Result;
        //              .GetAwaiter().GetResult();

        //    //}
        //}
    }
}
