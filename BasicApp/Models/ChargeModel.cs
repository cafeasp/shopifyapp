using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BasicApp.Models
{
    public class ChargeModel
    {
        [JsonProperty("name", NullValueHandling = NullValueHandling.Ignore)]
        public string Name { get; set; }

        [JsonProperty("price", NullValueHandling = NullValueHandling.Ignore)]
        public string Price { get; set; }

        [JsonProperty("return_url", NullValueHandling = NullValueHandling.Ignore)]
        public string ReturnUrl { get; set; }

        [JsonProperty("trial_days", NullValueHandling = NullValueHandling.Ignore)]
        public string TrialDays { get; set; }

        [JsonProperty("test", NullValueHandling = NullValueHandling.Ignore)]
        public string Test { get; set; }
    }
}
    
