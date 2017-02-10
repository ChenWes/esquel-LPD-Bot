using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Net;
using System.Net.Http;
using Newtonsoft.Json;
using System.Text;

namespace esquel_LPD_Bot.Common
{
    public class HttpClientHelper
    {
        public async Task<string> HttpClient_PostAsync(string pi_ApiUrl, string pi_Json,string pi_UserName,string pi_Password)
        {
            if (string.IsNullOrEmpty(pi_UserName) || string.IsNullOrEmpty(pi_Password))
            {
                throw new Exception("LPD Project Auth User Name Or Password Is Empty .");
            }

            string l_returnString = string.Empty;

            var handler = new HttpClientHandler()
            {
                AutomaticDecompression = System.Net.DecompressionMethods.GZip
            };
            handler.UseDefaultCredentials = true;//use default network
            handler.Credentials = new NetworkCredential(pi_UserName, pi_Password);//setting auth

            using (var client = new HttpClient(handler))
            {
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                var response = await client.PostAsync(pi_ApiUrl, new StringContent(pi_Json, Encoding.UTF8, "application/json"));
                response.EnsureSuccessStatusCode();
                string return_json = await response.Content.ReadAsStringAsync();
                if (!string.IsNullOrEmpty(return_json))
                {
                    l_returnString = return_json;
                }
            }

            return l_returnString;
        }

        public async Task<string> HttpClient_PostAsync(string pi_ApiUrl, string pi_Json)
        {
            Common.LPDAuthHelper LPDAuthClass = new Common.LPDAuthHelper();
            return await HttpClient_PostAsync(pi_ApiUrl, pi_Json, LPDAuthClass.UserName, LPDAuthClass.PassWord);
        }
    }
}