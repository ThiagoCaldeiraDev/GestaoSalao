using Seguro.Models.Intefaces;
using Seguro.Models.ViewModels.Base;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using RestSharp;

namespace Seguro.ExternalServices
{
    public class BaseApi : IBaseApi
    {
        #region Propriedades e Construtor

        private readonly string _domain;

        public BaseApi(IConfiguration configuration)
        {
            _domain = configuration["WebServices:Domain"];
        }

        #endregion

        #region GET

        public RetornoWS BuscarDadosGet(string apiPath)
        {
            try
            {
                var token = GetToken();
                var rest = new RestClient(_domain);

                var request = new RestRequest(apiPath, Method.Get)
                {
                    RequestFormat = DataFormat.Json,
                    OnBeforeDeserialization = resp => { resp.ContentType = "application/json"; }
                };

                if (!string.IsNullOrEmpty(token))
                    request.AddHeader("authorization", "bearer " + token);

                request.AddHeader("Content-Type", "application/json");

                var response = rest.Execute(request);

                return new RetornoWS
                {
                    Content = response.Content,
                    StatusCode = (int)response.StatusCode,
                    HttpStatusCode = response.StatusCode
                };
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion

        #region Private Methods

        private string GetToken()
        {
            var resultToken = AssignedToken();
            if (!resultToken.IsSuccessCallback) 
                return string.Empty;

            var dadosToken = JsonConvert.DeserializeObject<TokenAPI>(resultToken.Content);
            return dadosToken.Token;
        }

        private RetornoWS AssignedToken()
        {
            try
            {
                var rest = new RestClient(_domain);

                var request = new RestRequest("Session/Create", Method.Post)
                {
                    RequestFormat = DataFormat.Json,
                    OnBeforeDeserialization = resp => { resp.ContentType = "application/json"; }
                };

                request.AddHeader("Content-Type", "application/json");

                var bodyLogin = new
                {
                    login = "admin",
                    password = "admin@123"
                };
                request.AddJsonBody(bodyLogin);
                
                var response = rest.Execute(request);

                return new RetornoWS
                {
                    Content = response.Content,
                    StatusCode = (int)response.StatusCode,
                    HttpStatusCode = response.StatusCode
                };
            }
            catch (Exception e)
            {
                throw;
            }
        }

        #endregion
    }
}