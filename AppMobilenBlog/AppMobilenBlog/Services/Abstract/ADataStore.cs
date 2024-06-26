﻿using AppMobilenBlog.ServiceReference;
using System.Net.Http;

namespace AppMobilenBlog.Services.Abstract
{
    public abstract class ADataStore
    {
        protected nBlogService nBlogService;
        protected HttpClient HttpClient;

        public ADataStore()
        {
            var httpHandler = new HttpClientHandler();
#if DEBUG
            httpHandler.ClientCertificateOptions = ClientCertificateOption.Manual;
            httpHandler.ServerCertificateCustomValidationCallback =
            (httpRequestMessage, cert, cetChain, policyErrors) => true;
#endif   
            HttpClient = new HttpClient(httpHandler);
            nBlogService = new nBlogService("https://localhost:7056", HttpClient);
        }
    }
}
