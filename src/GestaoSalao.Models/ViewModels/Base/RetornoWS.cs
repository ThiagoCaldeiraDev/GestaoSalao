using Newtonsoft.Json;
using System.Net;

namespace GestaoSalao.Models.ViewModels.Base
{
    public class RetornoWS
    {
        public HttpStatusCode HttpStatusCode { get; set; }
        public int StatusCode { get; set; }
        public string Content { get; set; }

        public bool IsContentEmpty => Content.Equals("[]");
        public bool IsSuccessCallback => HttpStatusCode is HttpStatusCode.OK or HttpStatusCode.Created;

        public List<string> ErrosValidation
        {
            get
            {
                if (StatusCode != 422) 
                    return new List<string>();

                var informationErros = JsonConvert.DeserializeObject<ErroValidacaoVM>(Content);
                return informationErros?.Detailed;

            }
        }
    }
}
