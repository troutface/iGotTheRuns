using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Web;

namespace iGotTheRuns.Helpers
{
        public class BrowserJsonFormatter : JsonMediaTypeFormatter
        {
            // NOTE: custom formatter that accepts text/html requests and returns application/json responses. Renders JSON to the browser instead of XML.
            //       Related: register this formatter in WebApiConfig.cs: "config.Formatters.Add(new BrowserJsonFormatter());"
            public BrowserJsonFormatter()
            {
                this.SupportedMediaTypes.Add(new MediaTypeHeaderValue("text/html"));
                this.SerializerSettings.Formatting = Formatting.Indented;
                this.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            }

            public override void SetDefaultContentHeaders(Type type, HttpContentHeaders headers, MediaTypeHeaderValue mediaType)
            {
                base.SetDefaultContentHeaders(type, headers, mediaType);
                headers.ContentType = new MediaTypeHeaderValue("application/json");
            }
        }
}