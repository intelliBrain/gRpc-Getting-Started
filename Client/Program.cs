using System;
using System.Threading.Tasks;
using Grpc.Net.Client;
using MyWeather;
using Weather;
using Echo;

namespace Client
{
    class Program
    {
        static async Task Main(string[] args)
        {
            // The port number(5001) must match the port of the gRPC server.
            using var channel = GrpcChannel.ForAddress("https://localhost:5001");

            var weatherclient = new WeatherService.WeatherServiceClient(channel);
            var currentData = await weatherclient.RequestCurrentWeatherDataAsync(new WeatherRequest { Location = "MyTown" });
            var historicData = await weatherclient.RequestHistoricDataAsync(new WeatherRequest { Location = "MyTown" });

            var client = new Greeter.GreeterClient(channel);
            var reply = await client.SayHelloAsync(new HelloRequest { Name = "GreeterClient" });

            Console.WriteLine(
                $"The current weather for {currentData.Location} is {currentData.Temperature} degrees F with winds {currentData.Windspeed} from {currentData.Winddirection}");
            Console.WriteLine(
                $"The high temps for the past 10 days were ");
            foreach (var data in historicData.Data)
            {
                Console.WriteLine($"{data.Temperature}");
            }


            var echoClient = new EchoService.EchoServiceClient(channel);
            var response = echoClient.EchoMe(
                new EchoRequest() 
                { 
                    Text = $"ABCDEFG-{DateTime.Now.ToShortDateString()}" 
                });

            Console.WriteLine($"[{response.Original}] => [{response.Result}]");


            response = echoClient.ReverseEcho(
                new EchoRequest()
                {
                    Text = $"ABCDEFG-{DateTime.Now.ToShortDateString()}"
                });

            Console.WriteLine($"[{response.Original}] => [{response.Result}]");


            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
    }
}
