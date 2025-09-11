using Yarp.ReverseProxy.Configuration;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddHttpClient();

var routes = new[]
{
    new RouteConfig
    {
        RouteId = "orders",
        ClusterId = "ordersCluster",
        Match = new RouteMatch { Path = "/api/orders/{**catch-all}" }
    },
    new RouteConfig
    {
        RouteId = "clients",
        ClusterId = "clientsCluster",
        Match = new RouteMatch { Path = "/api/clients/{**catch-all}" }
    }
};

var clusters = new[]
{
    new ClusterConfig
    {
        ClusterId = "ordersCluster",
        Destinations = new Dictionary<string, DestinationConfig>
        {
            { "orders", new DestinationConfig { Address = "http://localhost:5001/" } }
        }
    },
    new ClusterConfig
    {
        ClusterId = "clientsCluster",
        Destinations = new Dictionary<string, DestinationConfig>
        {
            { "clients", new DestinationConfig { Address = "http://localhost:5002/" } }
        }
    }
};

builder.Services.AddReverseProxy()
    .LoadFromMemory(routes, clusters);

var app = builder.Build();
app.MapControllers();
app.MapReverseProxy();
app.Run();
