using Newtonsoft.Json;

namespace GestaoSalao.Models.ViewModels.Base
{
    public class TokenAPI
    {
        [JsonProperty("token")]
        public string Token { get; set; }
    }
}
