using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Web;
using System.Net;
using System.Net.Http;
using System.IO;
namespace Plug_Ins
{
    public class AuthorizeRequest
    {
        public static AuthorizeRequest CreateBySkelet()
        {
            return JObject.Parse(Skelet).ToObject<AuthorizeRequest>();
        }

        public string ToJson()
        {
            JObject o = (JObject)JToken.FromObject(this);
            return o.ToString();
        }
        public const string Skelet = @"{
  ""getHostedPaymentPageRequest"": {
    ""merchantAuthentication"": {
      ""name"": ""5KP3u95bQpv"",
      ""transactionKey"": ""346HZ32z3fP4hTG2""
    },
    ""transactionRequest"": {
      ""transactionType"": ""authCaptureTransaction"",
      ""amount"": ""20.00"",
    
      ""customer"": {
        ""email"": """"
      },
      ""billTo"": {
        ""firstName"": """",
        ""lastName"": """",
        ""company"": """",
        ""address"": """",
        ""city"": """",
        ""state"": ""TX"",
        ""zip"": """",
        ""country"": """"
      }
    },
    ""hostedPaymentSettings"":{
       ""setting"": [{
        ""settingName"": ""hostedPaymentButtonOptions"",
        ""settingValue"": ""{\""text\"": \""Pay\""}""
      }, {
        ""settingName"": ""hostedPaymentStyleOptions"",
        ""settingValue"": ""{\""bgColor\"": \""blue\""}""
      }, {
        ""settingName"": ""hostedPaymentPaymentOptions"",
        ""settingValue"": ""{\""cardCodeRequired\"": false, \""showCreditCard\"": true, \""showBankAccount\"": true}""
      }, {
        ""settingName"": ""hostedPaymentSecurityOptions"",
        ""settingValue"": ""{\""captcha\"": false}""
      }, {
        ""settingName"": ""hostedPaymentShippingAddressOptions"",
        ""settingValue"": ""{\""show\"": false, \""required\"": false}""
      }, {
        ""settingName"": ""hostedPaymentBillingAddressOptions"",
        ""settingValue"": ""{\""show\"": true, \""required\"": false}""
      }, {
        ""settingName"": ""hostedPaymentCustomerOptions"",
        ""settingValue"": ""{\""showEmail\"": false, \""requiredEmail\"": false, \""addPaymentProfile\"": true}""
      }, {
        ""settingName"": ""hostedPaymentOrderOptions"",
        ""settingValue"": ""{\""show\"": true, \""merchantName\"": \""G and S Questions Inc.\""}""
      }]
    }
  }
}";
        public Gethostedpaymentpagerequest getHostedPaymentPageRequest { get; set; }
    }

    public class Gethostedpaymentpagerequest
    {
        public Merchantauthentication merchantAuthentication { get; set; }
        public Transactionrequest transactionRequest { get; set; }
        public Hostedpaymentsettings hostedPaymentSettings { get; set; }
    }

    public class Merchantauthentication
    {
        public string name { get; set; }
        public string transactionKey { get; set; }
    }

    public class Transactionrequest
    {
        public string transactionType { get; set; }
        public string amount { get; set; }
        public Profile profile { get; set; }
        public Customer customer { get; set; }
        public Billto billTo { get; set; }
    }

    public class Profile
    {
        public string customerProfileId { get; set; }
    }

    public class Customer
    {
        public string email { get; set; }
    }

    public class Billto
    {
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string company { get; set; }
        public string address { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public string zip { get; set; }
        public string country { get; set; }
    }

    public class Hostedpaymentsettings
    {
        public List<Setting> setting { get; set; }
    }
    public class Setting
    {
        public static Setting Create(string name, string value)
        {
            Setting s = new Setting();
            s.settingName = name;
            s.settingValue = value;
            return s;
        }
        public string settingName { get; set; }
        public string settingValue { get; set; }
    }
}
