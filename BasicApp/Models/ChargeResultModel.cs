using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BasicApp.Models
{
    public class ChargeResultModel
    {
        public RecurringApplicationCharge recurring_application_charge { get; set; }
    }
    public class RecurringApplicationCharge
    {
        [JsonProperty("id", NullValueHandling = NullValueHandling.Ignore)]
        public int Id { get; set; }
        [JsonProperty("name", NullValueHandling = NullValueHandling.Ignore)]
        public string Name { get; set; }
        [JsonProperty("api_client_id", NullValueHandling = NullValueHandling.Ignore)]
        public int ApiClientId { get; set; }
        [JsonProperty("price", NullValueHandling = NullValueHandling.Ignore)]
        public string Price { get; set; }
        [JsonProperty("status", NullValueHandling = NullValueHandling.Ignore)]
        public string Status { get; set; }
        [JsonProperty("return_url", NullValueHandling = NullValueHandling.Ignore)]
        public string ReturnUrl { get; set; }
        [JsonProperty("billing_on", NullValueHandling = NullValueHandling.Ignore)]
        public string BillingOn { get; set; }
        [JsonProperty("created_at", NullValueHandling = NullValueHandling.Ignore)]
        public string Created_at { get; set; }
        [JsonProperty("updated_at", NullValueHandling = NullValueHandling.Ignore)]
        public string UpdatedAt { get; set; }

        [JsonProperty("test", NullValueHandling = NullValueHandling.Ignore)]
        public bool Test { get; set; }
        public string activated_on { get; set; }
        [JsonProperty("trial_ends_on", NullValueHandling = NullValueHandling.Ignore)]
        public string TrialEndsOn { get; set; }
        [JsonProperty("cancelled_on", NullValueHandling = NullValueHandling.Ignore)]
        public string CancelledOn { get; set; }
        public int trial_days { get; set; }
        [JsonProperty("decorated_return_url", NullValueHandling = NullValueHandling.Ignore)]
        public string DecoratedReturnUrl { get; set; }
        [JsonProperty("confirmation_url", NullValueHandling = NullValueHandling.Ignore)]
        public string ConfirmationUrl { get; set; }
    }
}