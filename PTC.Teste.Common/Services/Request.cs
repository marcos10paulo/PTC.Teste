using PTC.Teste.Common.Interface;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using PTC.Teste.Dto;

namespace PTC.Teste.Common.Services
{
    public class Request : IRequest
    {
        private readonly string UrlApi;
        private readonly string UserApi;
        private readonly string PassApi;

        public Request(IConfiguration configuration)
        {
            UrlApi = configuration.GetSection("UrlApi").Value;
            UserApi = configuration.GetSection("UserApi").Value;
            PassApi = configuration.GetSection("PassApi").Value;
        }

        public T Get<T>(string entidade, string metodo, Dictionary<string, IConvertible> parametros = null)
        {
            try
            {
                using var client = new HttpClient();

                string urlCompleta = $"{UrlApi}/api/{entidade}/{metodo}/";
                string urlParametros = "";
                bool primeiro = true;

                if (parametros != null)
                {
                    foreach (KeyValuePair<string, IConvertible> item in parametros)
                    {
                        if (primeiro)
                        {
                            urlParametros = "?";
                            primeiro = false;
                        }
                        else
                        {
                            urlParametros += "&";
                        }

                        urlParametros += $"{item.Key}={item.Value}";
                    }
                }

                var byteArray = Encoding.ASCII.GetBytes($"{UserApi}:{PassApi}");
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));

                using var response = client.GetAsync(urlCompleta + urlParametros).Result;

                string apiResponse = response.Content.ReadAsStringAsync().Result;

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    if (typeof(T).Name == "String")
                    {
                        return (T)(object)apiResponse;
                    }
                    else
                    {
                        return JsonConvert.DeserializeObject<T>(apiResponse);
                    }
                }
                else
                {
                    return default;
                }
            }
            catch
            {
                return default;
            }
        }

        public string Post<T>(T model, string entidade, string metodo) where T : class
        {
            using var client = new HttpClient();
            StringContent content = new(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");

            try
            {
                var byteArray = Encoding.ASCII.GetBytes($"{UserApi}:{PassApi}");
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));

                using var response = client.PostAsync($"{UrlApi}/api/{entidade}/{metodo}", content).Result;
                string apiResponse = response.Content.ReadAsStringAsync().Result;

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    return "";
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
                {
                    return apiResponse;
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    return "Não autorizado!";
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.InternalServerError)
                {
                    return "Erro no servidor!" + apiResponse;
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    return "Não encontrado!";
                }
                else
                {
                    return "Resposta não identificada!";
                }
            }
            catch (Exception ex)
            {
                return "Erro no Servidor. Por favor contate o administrador." + ex.Message;
            }
        }

        public T Post<T>(string entidade, string metodo, string condicao = "", int? usuarioId = null) where T : class
        {
            using var client = new HttpClient();

            StringContent content = new(JsonConvert.SerializeObject(new FiltroDTO() { Condicao = condicao }), Encoding.UTF8, "application/json");

            try
            {
                var byteArray = Encoding.ASCII.GetBytes($"{UserApi}:{PassApi}");
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));

                using var response = client.PostAsync($"{UrlApi}/api/{entidade}/{metodo}", content).Result;

                string apiResponse = response.Content.ReadAsStringAsync().Result;

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    return JsonConvert.DeserializeObject<T>(apiResponse);
                }
                else
                {
                    return null;
                }
            }
            catch
            {
                return null;
            }
        }

        public string Delete(string entidade, string metodo, int id)
        {
            using var client = new HttpClient();
            try
            {
                var byteArray = Encoding.ASCII.GetBytes($"{UserApi}:{PassApi}");
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));

                using var response = client.DeleteAsync($"{UrlApi}/api/{entidade}/{metodo}/{id}").Result;
                string apiResponse = response.Content.ReadAsStringAsync().Result;

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    return "";
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
                {
                    return apiResponse;
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    return "Não autorizado!";
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.InternalServerError)
                {
                    return "Erro no servidor!" + apiResponse;
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    return "Não encontrado!";
                }
                else
                {
                    return "Resposta não identificada!";
                }
            }
            catch (Exception ex)
            {
                return "Erro no Servidor. Por favor contate o administrador." + ex.Message;
            }
        }

        public string Post(string entidade, string metodo)
        {
            using var client = new HttpClient();
            StringContent content = new("{}", Encoding.UTF8, "application/json");

            try
            {
                var byteArray = Encoding.ASCII.GetBytes($"{UserApi}:{PassApi}");
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));

                using var response = client.PostAsync($"{UrlApi}/api/{entidade}/{metodo}", content).Result;
                string apiResponse = response.Content.ReadAsStringAsync().Result;

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    return "";
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
                {
                    return apiResponse;
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    return "Não autorizado!";
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.InternalServerError)
                {
                    return "Erro no servidor!" + apiResponse;
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    return "Não encontrado!";
                }
                else
                {
                    return "Resposta não identificada!";
                }
            }
            catch (Exception ex)
            {
                return "Erro no Servidor. Por favor contate o administrador." + ex.Message;
            }
        }
    }
}
