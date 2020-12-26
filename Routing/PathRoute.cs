using System.Collections.Generic;

namespace APIGatewayHandlerLibrary.Routing
{
    public class PathRoute : IRoute
    {
        private readonly string _pathPattern;

        /**
         * pathPattern must be the path, with path parameters in braces {}. 
         * E.g. "/api/users/{userID}/data" where {userID} is an expected path parameter. So "/api/user/user1/data" would be a match.
         */
        public PathRoute(string baseUri, string pathPattern) : this(baseUri + pathPattern)
        {
        }

        public PathRoute(string pathPattern)
        {
            _pathPattern = pathPattern;
        }

        public bool Matches(string path, HttpMethod httpMethod, IDictionary<string, string> pathParameters)
        {
            foreach (var keyValuePair in pathParameters)
            {
                path = path.Replace(keyValuePair.Value, "{" + keyValuePair.Key + "}");
            }

            return _pathPattern.Equals(path);
        }
    }
}
