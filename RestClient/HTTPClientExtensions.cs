using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DewCore.DewRestClient
{
    /// <summary>
    /// Extend the wrappers for httpclient
    /// </summary>
    public static class HttpClientExtensions
    {
        /// <summary>
        /// Extension PATCH method wrapper
        /// </summary>
        /// <param name="client"></param>
        /// <param name="requestUri">The URI the request to sent to</param>
        /// <param name="iContent">The message content</param>
        /// <param name="cancellationToken"></param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="InvalidOperationException"></exception>
        /// <returns></returns>
        public static async Task<HttpResponseMessage> PatchAsync(this HttpClient client, Uri requestUri, HttpContent iContent, CancellationToken cancellationToken = default(CancellationToken))
        {
            var method = new HttpMethod("PATCH");
            var request = new HttpRequestMessage(method, requestUri)
            {
                Content = iContent
            };
            HttpResponseMessage response = new HttpResponseMessage();
            try
            {
                response = await client.SendAsync(request, cancellationToken);
            }
            catch (TaskCanceledException e)
            {
                Debug.WriteLine("ERROR: " + e.ToString());
            }
            return response;
        }
        /// <summary>
        /// Extension HEAD method wrapper
        /// </summary>
        /// <param name="client"></param>
        /// <param name="requestUri">The URI the request to sent to</param>
        /// /// <param name="cancellationToken"></param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="InvalidOperationException"></exception>
        /// <returns></returns>
        public static async Task<HttpResponseMessage> HeadAsync(this HttpClient client, Uri requestUri, CancellationToken cancellationToken = default(CancellationToken))
        {
            var method = new HttpMethod("HEAD");
            var request = new HttpRequestMessage(method, requestUri);
            HttpResponseMessage response = new HttpResponseMessage();
            try
            {
                response = await client.SendAsync(request, cancellationToken);
            }
            catch (TaskCanceledException e)
            {
                Debug.WriteLine("ERROR: " + e.ToString());
            }
            return response;
        }
        /// <summary>
        /// Extension OPTIONS method wrapper
        /// </summary>
        /// <param name="client"></param>
        /// <param name="requestUri">The URI the request to sent to</param>
        /// <param name="iContent">The message content</param>
        /// <param name="cancellationToken"></param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="InvalidOperationException"></exception>
        /// <returns></returns>
        public static async Task<HttpResponseMessage> OptionsAsync(this HttpClient client, Uri requestUri, HttpContent iContent, CancellationToken cancellationToken = default(CancellationToken))
        {
            var method = new HttpMethod("OPTIONS");
            var request = new HttpRequestMessage(method, requestUri)
            {
                Content = iContent
            };
            HttpResponseMessage response = new HttpResponseMessage();
            try
            {
                response = await client.SendAsync(request, cancellationToken);
            }
            catch (TaskCanceledException e)
            {
                Debug.WriteLine("ERROR: " + e.ToString());
            }
            return response;
        }
    }
}
