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
    public class AuthorizeConnector
    {
        public static string getToken(Action<string> action, string apiLoginId, string transactionKey, string amount)
        {
            string token = "fake";
            var httpWebRequest = (HttpWebRequest)WebRequest.Create("https://apitest.authorize.net/xml/v1/request.api");
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";

            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                //json to object, change value, serize to json
                AuthorizeRequest request = AuthorizeRequest.CreateBySkelet();
                request.getHostedPaymentPageRequest.merchantAuthentication.name = apiLoginId;
                request.getHostedPaymentPageRequest.merchantAuthentication.transactionKey = transactionKey;
                request.getHostedPaymentPageRequest.transactionRequest.amount = amount;
               
                string json = request.ToJson();
                streamWriter.Write(json);
                streamWriter.Flush();
                streamWriter.Close();
            }

            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();
                AuthorizeResponse response = JObject.Parse(result).ToObject<AuthorizeResponse>();
                if (response.messages.resultCode == AuthorizeResponse.RESULT_OK)
                {
                    token = response.token;
                }
                else
                {
                    action(result);
                }
            }

            action("token is :" + token);
            return token;
        }
    }

    /*{
  "token": "FCfc6VbKGFztf8g4sI0B1bG35quHGGlnJx7G8zRpqV0gha2862KkqRQ/NaGa6y2SIhueCAsP/CQKQDQ0QJr8mOfnZD2D0EfogSWP6tQvG3xlv1LS28wFKZHt2U/DSH64eA3jLIwEdU+++++++++++++shortened_for_brevity++++++++WC1mNVQNKv2Z+ 1msH4oiwoXVleb2Q7ezqHYl1FgS8jDAYzA7ls+AYf05s=.89nE4Beh",
  "messages": {
    "resultCode": "Ok",
    "message": [
      {
        "code": "I00001",
        "text": "Successful."
      }
    ]
  }
}*/
    public class AuthorizeResponse
    {
        public const string RESULT_OK="Ok";
        public string token { get; set; }
        public Messages messages { get; set; }
    }

    public class Messages
    {
        public string resultCode { get; set; }
        public Message[] message { get; set; }
    }

    public class Message
    {
        public string code { get; set; }
        public string text { get; set; }
    }




    public class AuthorizeRequest
    {
        public  static AuthorizeRequest CreateBySkelet()
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
    ""hostedPaymentSettings"": {
      ""setting"": [{
        ""settingName"": ""hostedPaymentReturnOptions"",
        ""settingValue"": ""{\""showReceipt\"": true, \""url\"": \""https://mysite.com/receipt\"", \""urlText\"": \""Continue\"", \""cancelUrl\"": \""https://mysite.com/cancel\"", \""cancelUrlText\"": \""Cancel\""}""
      }, {
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
      }, {
        ""settingName"": ""hostedPaymentIFrameCommunicatorUrl"",
        ""settingValue"": ""{\""url\"": \""https://mysite.com/special\""}""
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
        public Setting[] setting { get; set; }
    }

    public class Setting
    {
        public string settingName { get; set; }
        public string settingValue { get; set; }
    }

}
