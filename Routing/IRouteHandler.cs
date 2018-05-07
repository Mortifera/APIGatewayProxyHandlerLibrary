using Amazon.Lambda.APIGatewayEvents;

namespace APIGatewayHandlerLibrary.Routing
{
    public interface IRouteHandler
    {
        APIGatewayProxyResponse Handle(APIGatewayProxyRequest request);
    }
}