using Newtonsoft.Json;


namespace Assets.src.definitions
{
    public sealed class DefinitionRoot
    {
        [JsonProperty("universe")]
        public DefinitionNode Universe { get; set; }
    }

}
