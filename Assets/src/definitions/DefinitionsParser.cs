using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Linq;


namespace Assets.src.definitions
{


    public sealed class DefinitionsParser
    {
        public static DefinitionRoot[] Parse(string[] json)
        {


            return json.ToList().Select(data =>
            {
                var settings = new JsonSerializerSettings
                {
                    ContractResolver = new DefaultContractResolver
                    {
                        NamingStrategy = new CamelCaseNamingStrategy()
                    }
                };

                return JsonConvert.DeserializeObject<DefinitionRoot>(data, settings);
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
        //        return JsonSerializer.DeserializeAsync<DefinitionRoot>(stream, options);
        //    });


        //    return Task.WhenAll(tasks.Select(t => t.AsTask())).Result;

        //}
    }
}
