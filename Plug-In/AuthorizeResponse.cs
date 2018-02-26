using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plug_Ins
{
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
        public const string RESULT_OK = "Ok";
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

}
