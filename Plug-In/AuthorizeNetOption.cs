﻿using System;
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
    public class AuthorizeNetOption
    {
        const string defaultOption = @"{
                ApiLoginId:'5KP3u95bQpv',
                TransactionKey:'346HZ32z3fP4hTG2',
                IFrameUrl:'https://helpjack.microsoftcrmportals.com/iframecommunicator/',
                AuthorizeURL:'https://apitest.authorize.net/xml/v1/request.api',
                SucceedURL:'https://helpjack.microsoftcrmportals.com/succeed/',
                CancelURL:'https://helpjack.microsoftcrmportals.com/cancel/'
                }";
        public string ApiLoginId { get; set; }
        public string TransactionKey { get; set; }
        public string IFrameUrl { get; set; }
        public string AuthorizeURL { get; set; }
        public string SucceedURL { get; set; }
        public string CancelURL { get; set; }

        public static AuthorizeNetOption DefaultOption()
        {
            return JObject.Parse(defaultOption).ToObject<AuthorizeNetOption>();
        }
    }
}
