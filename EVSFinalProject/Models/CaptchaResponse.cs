using Newtonsoft.Json;
using System.Collections.Generic;

namespace EVSFinalProject.Models
{
    public class CaptchaResponse
    {
        [JsonProperty("success")]
        public bool Success { get; set; }
        [JsonProperty("eroor-code")]
        public List<string> EroorCode { get; set; }
    }
}
