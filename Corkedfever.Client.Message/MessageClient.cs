using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Corkedfever.Client;
namespace Corkedfever.Client.Message
{
    public class MessageClient : ClientBase
    {
        public MessageClient(string baseUri) : base(baseUri)
        {
            BaseUrl = baseUri;
        }
        

    }
}
