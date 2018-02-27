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
        public static string getToken(
            AuthorizeNetOption option,
            string amount, Action<string> action)
        {
            string token = "fake";
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(option.AuthorizeURL);
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";

            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                //json to object, change value, serize to json
                AuthorizeRequest request = AuthorizeRequest.CreateBySkelet();
                request.getHostedPaymentPageRequest.merchantAuthentication.name = option.ApiLoginId;
                request.getHostedPaymentPageRequest.merchantAuthentication.transactionKey = option.TransactionKey;
                request.getHostedPaymentPageRequest.transactionRequest.amount = amount;
                request.getHostedPaymentPageRequest.hostedPaymentSettings.setting.Add(Setting.Create(
                     "hostedPaymentIFrameCommunicatorUrl",
                     "{\"url\": \"" + option.IFrameUrl + "\"}"
                    ));
                
                request.getHostedPaymentPageRequest.hostedPaymentSettings.setting.Add(Setting.Create(
                     "hostedPaymentReturnOptions",
                     "{\"showReceipt\": true, \"url\": \"" +
                     option.SucceedURL +
                     "\", \"urlText\": \"Continue\", \"cancelUrl\": \"" +
                     option.CancelURL +
                     "\", \"cancelUrlText\": \"Cancel Pay\"}"
                    ));
                request.getHostedPaymentPageRequest.hostedPaymentSettings.setting.Add(Setting.Create(
                    "hostedPaymentOrderOptions",
                    "{\"show\": true, \"merchantName\":\"" + option.MerchantName + "\"}"
                    ));
                

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
}
