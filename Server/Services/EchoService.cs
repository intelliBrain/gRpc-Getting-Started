using Echo;
using Grpc.Core;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Server
{
    public class EchoService : Echo.EchoService.EchoServiceBase
    {
        private readonly ILogger<EchoService> _logger;

        public EchoService(ILogger<EchoService> logger)
        {
            _logger = logger;
        }

        public override Task<EchoResponse> EchoMe(EchoRequest request,
            ServerCallContext context)
        {
            var response = new EchoResponse() { Original = request.Text, Result = request.Text };
            return Task.FromResult(response);
        }

        public override Task<EchoResponse> ReverseEcho(EchoRequest request,
            ServerCallContext context)
        {
            var response = new EchoResponse() { Original = request.Text, Result = request.Text.Reverse() };
            return Task.FromResult(response);
        }
        private static string Reverse(string s)
        {
            char[] charArray = s.ToCharArray();
            Array.Reverse(charArray);
            return new string(charArray);
        }
    }
}