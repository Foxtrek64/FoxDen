using Aspire.Hosting.ApplicationModel;

namespace FoxDen.AppHost.Swagger
{
    public sealed class SwaggerUIAnnotation(string[] documentNames, string path, EndpointReference endpointReference) : IResourceAnnotation
    {
        public string[] DocumentNames => documentNames;
        public string Path => path;
        public EndpointReference EndpointReference => endpointReference;
    }
}
