using AulaVirtual.Modelo;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using WebHelpers.Http;
using WebHelpers.Uri;

namespace AulaVirtual.WebService
{
    public static class ResourceAlumnos
    {
        public static Uri Endpoint => new Uri(ApiConstants.API_URL + "/alumnos");

        public static async Task<List<Alumno>> Consultar(Dictionary<string, string>filtros = null)
        {
            var endpoint = new UriBuilder(Endpoint);
            if (filtros != null) {
                var endpointParams = new QueryParameters(endpoint.Query);
                endpointParams.Append(filtros);
                endpoint.Query = endpointParams.ToString();
            }

            var client = new HttpClient();
            var response = await client.GetAsync(endpoint.Uri);
            var content = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode) {
                return JsonConvert.DeserializeObject<List<Alumno>>(content);
            } else {
                var result = JsonConvert.DeserializeObject<RestError>(content);
                throw new Exception(result.Error);
            }
        }

        public static async Task<Alumno> ConsultarPorId(int id)
		{
            var endpoint = new UriBuilder(Endpoint);
            endpoint.Path += "/" + id;

            var client = new HttpClient();
            var response = await client.GetAsync(endpoint.Uri);
            var content = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode) {
                return JsonConvert.DeserializeObject<Alumno>(content);
			} else {
                var result = JsonConvert.DeserializeObject<RestError>(content);
                throw new Exception(result.Error);
			}
        }

        /// <exception cref="HttpRequestException"/>
        public static async Task<bool> Insertar(Alumno alumno)
        {
            if (alumno.ID != 0) {
                return false;
            }

            var json = JsonConvert.SerializeObject(alumno);
            var data = new StringContent(json, Encoding.UTF8, MimeTypes.APPLICATION_JSON);

            var client = new HttpClient();
            var response = await client.PostAsync(Endpoint, data);
            var content = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode) {
                var result = JsonConvert.DeserializeObject<RestPost>(content);
                alumno.ID = result.ID;
                return true;
            } else {
                var result = JsonConvert.DeserializeObject<RestError>(content);
                throw new Exception(result.Error);
            }
        }

        public static async Task<bool> Actualizar(Alumno alumno)
        {
            if (alumno.ID == 0) {
                return false;
            }
            var json = JsonConvert.SerializeObject(alumno);
            var data = new StringContent(json, Encoding.UTF8, MimeTypes.APPLICATION_JSON);

            var endpoint = new UriBuilder(Endpoint);
            endpoint.Path += "/" + alumno.ID;

            var client = new HttpClient();
            var response = await client.PutAsync(endpoint.Uri, data);

            if (response.IsSuccessStatusCode) {
                return true;
            } else {
                var content = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<RestError>(content);
                throw new Exception(result.Error);
            }
        }
        public static async Task<bool> Eliminar(Alumno alumno)
		{
            if (alumno.ID == 0) {
                return false;
			}

            var endpoint = new UriBuilder(Endpoint);
            endpoint.Path += "/" + alumno.ID;

            var client = new HttpClient();
            var response = await client.DeleteAsync(endpoint.Uri);

            if (response.IsSuccessStatusCode) {
                return true;
			} else {
                var content = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<RestError>(content);
                throw new Exception(result.Error);
			}
		}
    }
}