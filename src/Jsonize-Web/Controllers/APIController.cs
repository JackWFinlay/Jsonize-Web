﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using JackWFinlay.Jsonize;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NullValueHandling = JackWFinlay.Jsonize.NullValueHandling;

namespace Jsonize_Web.Controllers
{
    public class ApiController : Controller
    {
        [HttpGet]
        public async Task<dynamic> Convert(string url, string format = "json",
                                            string emptyTextNodeHandling = "ignore",
                                            string nullValueHandling = "ignore", 
                                            string textTrimHandling = "trim",
                                            string classAttributeHandling = "array",
                                            string renderJavascript = "false")
        {
            JsonizeConfiguration jsonizeConfiguration = new JsonizeConfiguration()
            {
                EmptyTextNodeHandling = emptyTextNodeHandling.ToLower().Equals("ignore") ? EmptyTextNodeHandling.Ignore : EmptyTextNodeHandling.Include,
                NullValueHandling = nullValueHandling.ToLower().Equals("ignore") ? NullValueHandling.Ignore : NullValueHandling.Include,
                TextTrimHandling = textTrimHandling.ToLower().Equals("trim") ? TextTrimHandling.Trim : TextTrimHandling.Include,
                ClassAttributeHandling = classAttributeHandling.ToLower().Equals("array") ? ClassAttributeHandling.Array : ClassAttributeHandling.String
            };

            try
            {
                Jsonize jsonize;
                if (renderJavascript.ToLower().Equals("true"))
                {
                    using (var client = new HttpClient())
                    {
                        client.DefaultRequestHeaders.Add("url", url);
                        var response = await client.GetAsync((Environment.GetEnvironmentVariable("DepthchargeRenderUrl") ?? Environment.GetEnvironmentVariable("APPSETTING_DepthchargeRenderUrl")) + "/api/render");
                        var html = await response.Content.ReadAsStringAsync();
                        jsonize = Jsonize.FromHtmlString(html);
                    }

                }
                else
                {
                    jsonize = await Jsonize.FromHttpUrl(url);
                }

                if (format.ToLower().Equals("json"))
                {
                    JObject json = jsonize.ParseHtmlAsJson(jsonizeConfiguration);
                    return Json(json);
                }
                else
                {
                    string jsonString = jsonize.ParseHtmlAsJsonString(jsonizeConfiguration);
                    return jsonString;
                }
            }
            catch (Exception e)
            {
                if (format.ToLower().Equals("json"))
                {
                    return JsonConvert.DeserializeObject<JObject>("{ 'error' : 'Incorrect usage.' }");
                }
                else
                {
                    return "{ 'error' : 'Incorrect usage.' }";
                }
            }
        }
    }
}
