﻿using System.Diagnostics;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace CustomWebApi.Handlers
{
    public class CustomMessageHandler : DelegatingHandler
    {
        protected override async Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request, CancellationToken cancellationToken)
        {
            Debug.WriteLine("Process request");
            // Call the inner handler.
            var response = await base.SendAsync(request, cancellationToken);
            Debug.WriteLine("Process response");
            return response;
        }
    }
}