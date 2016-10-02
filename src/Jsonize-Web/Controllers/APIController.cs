using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using JackWFinlay.Jsonize;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace Jsonize_Web.Controllers
{
    public class APIController : Controller
    {
        // GET api/values/5
        [HttpGet]
        public async Task<string> ConvertString(string url)
        {
            JObject json = await GetJson(url);
            return json.ToString();
        }

        [HttpGet]
        public async Task<JsonResult> Convert(string url)
        {
            JObject json = await GetJson(url);
            return Json(json);
        }

        //// POST api/values
        //[HttpPost]
        //public void Convert([FromBody]string url)
        //{


        //}

        private static async Task<JObject> GetJson(string url = "")
        {

            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Clear();

                

                try
                {
                    HttpResponseMessage response = await client.GetAsync(url);

                    if (response.IsSuccessStatusCode)
                    {
                        string html = await response.Content.ReadAsStringAsync();
                        Jsonize jsonize = new Jsonize(html);

                        return jsonize.ParseHtmlAsJson();
                    }
                    else
                    {
                        return JsonConvert.DeserializeObject<JObject>("{ 'error' : 'Incorrect usage.' }");
                    }
                }
                catch
                {
                    return JsonConvert.DeserializeObject<JObject>("{ 'error' : 'Incorrect usage.' }");
                }
            }
        }
    }
}
