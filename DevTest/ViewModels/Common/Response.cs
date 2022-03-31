using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace LTHL.VIEW_MODELS.Common
{
    public class Response<T>
    {
        [JsonProperty(PropertyName = "isError")]
        public bool IsError { get; set; }

        [JsonProperty(PropertyName = "message")]
        public string Message { get; set; }

        [JsonProperty(PropertyName = "exception")]
        public string Exception { get; set; } = "";

        [JsonProperty(PropertyName = "data")]
        public T Data { get; set; }
       
    }
}
