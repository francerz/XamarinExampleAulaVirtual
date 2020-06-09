using Newtonsoft.Json;

namespace AulaVirtual.WebService
{
    public class RestPost
    {
        [JsonProperty("id")]
        public int ID { get; set; }
    }
}