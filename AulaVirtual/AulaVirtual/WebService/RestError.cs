using Newtonsoft.Json;

namespace AulaVirtual.WebService
{
    public class RestError
    {
        [JsonProperty("error")]
        public string Error { get; set; }
    }
}
