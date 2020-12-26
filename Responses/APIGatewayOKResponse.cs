using Amazon.Lambda.APIGatewayEvents;
using Newtonsoft.Json;

namespace APIGatewayHandlerLibrary.Responses
{
    public class APIGatewayOKResponse<T> : APIGatewayProxyResponse
    {
        public APIGatewayOKResponse(T responseBody)
        {
            Body = JsonConvert.SerializeObject(responseBody);
            StatusCode = 200;
        }
    }
}
