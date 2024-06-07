using AppMobilenBlog.ServiceReference;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace AppMobilenBlog.Services.Abstract
{
    public abstract class ADataStore
    {
        protected nBlogService nBlogService;

        public ADataStore()
        {
            var httpHandler = new HttpClientHandler();
#if DEBUG
            httpHandler.ClientCertificateOptions = ClientCertificateOption.Manual;
            httpHandler.ServerCertificateCustomValidationCallback =
            (httpRequestMessage, cert, cetChain, policyErrors) => true;
#endif   
            var httpclient = new HttpClient(httpHandler);
            nBlogService = new nBlogService("https://localhost:7056", httpclient);
        }
    }
}
