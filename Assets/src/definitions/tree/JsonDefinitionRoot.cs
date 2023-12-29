using Newtonsoft.Json;


namespace Assets.src.definitions
{
    public sealed class JsonDefinitionRoot
    {
        [JsonProperty("universe")]
        public JsonDefinitionNode Universe { get; set; }
    }

}
