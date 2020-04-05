using Amazon.Lambda.APIGatewayEvents;
using Newtonsoft.Json;

namespace APIGatewayHandlerLibrary.Responses
{
    public class APIGatewayOKEmptyResponse : APIGatewayOKResponse<string>
    {
        public APIGatewayOKEmptyResponse() : base("")
        {
            
        }
    }
}