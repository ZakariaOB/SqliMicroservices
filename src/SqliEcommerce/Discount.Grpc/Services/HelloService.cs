using Grpc.Core;
using helloGrpc;

namespace Discount.Grpc.Services
{
    public class HelloService : HelloProtoService.HelloProtoServiceBase
    {
        public override Task<HelloReply> SayHello(
            HelloRequest request, 
            ServerCallContext context)
        {
            return Task.FromResult(new HelloReply
            {
                Message = $"Hello, {request.Name}!"
            });
        }
    }
}
