using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using RestSharp;
using Izzi_Statistics_Override_WPF.Model;

namespace Izzi_Statistics_Override_WPF.Util
{
    public class UtilWebRequest
    {
        public enum enumMethod
        {
            GET,
            POST,
            DELETE,
            PUT,
            PATCH
        }
        private static readonly HttpClient _client = new HttpClient();
        public async Task<string> OFSC_API (string endpoint, enumMethod enumMethod, string data)
        {
            string settingUser = Properties.Settings.Default.key_user;
            string settingPass = Properties.Settings.Default.key_pass;
            string url = "https://sky-mx2.test.fs.ocs.oraclecloud.com" + endpoint;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Ssl3;
            _client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", Convert.ToBase64String(Encoding.ASCII.GetBytes($"{settingUser}:{settingPass}")));
            var request = new HttpRequestMessage();
            switch (enumMethod.ToString())
            {
                case "PUT":
                    request.Method = HttpMethod.Put;
                    break;
                case "POST":
                    request.Method = HttpMethod.Post;
                    break;
                case "PATCH":
                    request.Method = new HttpMethod("PATCH");
                    break;
                case "GET":
                    request.Method = HttpMethod.Get;
                    break;
                case "DELETE":
                    request.Method = HttpMethod.Delete;
                    break;
                default:
                    break;
            }
            request.RequestUri = new System.Uri(url);
            if (!string.IsNullOrEmpty(data))
            {
                request.Content = new StringContent(data, Encoding.UTF8, "application/json");
            }
            try
            {
                var response = await _client.SendAsync(request);
                if(response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    return result;
                }
                else
                {
                    return $"Error: {response.StatusCode}";
                }
            }
            catch (Exception ex)
            {
                return $"Error: {ex.Message} | Detalle {ex.InnerException.Message}";
            }
        }
        public async Task<OFSC_Response> UpdateActivityDurationStatistics(string data)
        {
            string settingUser = Properties.Settings.Default.key_user;
            string settingPass = Properties.Settings.Default.key_pass;
            string settingUrl = Properties.Settings.Default.key_url;
            string endpoint = "/rest/ofscStatistics/v1/activityDurationStats";
            string token = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", Convert.ToBase64String(Encoding.ASCII.GetBytes($"{settingUser}:{settingPass}"))).ToString();
            var client = new RestClient(settingUrl + endpoint);
            var request = new RestRequest();
            request.AddHeader("Authorization", token);
            request.AddHeader("Contenty-Type", "application/json");
            request.Method = Method.PATCH;
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Ssl3;
            OFSC_Response result = new OFSC_Response();
            if (!string.IsNullOrEmpty(data))
            {
                List<JObject> objectList = JsonConvert.DeserializeObject<List<JObject>>(data);
                JObject json = new JObject();
                json["items"] = JToken.FromObject(objectList);
                request.AddParameter("", json.ToString(), ParameterType.RequestBody);
                try
                {
                    IRestResponse response = await client.ExecuteAsync(request);
                    result.statusCode = (int)response.StatusCode;
                    result.content = response.Content;
                    result.errorMessage = response.ErrorMessage;
                    return result;
                }
                catch (Exception ex)
                {
                    result.statusCode = 0;
                    result.content = ex.Message;
                    result.errorMessage = ex.InnerException.Message;
                }
            }
            result.statusCode = 0;
            result.content = "Error en la plantilla de datos";
            result.errorMessage = "Error en la plantilla de datos";
            return result;
        }
    }
}