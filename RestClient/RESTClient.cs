using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using DewCore.Abstract.RestClient;
using DewCore.RestClient.Extensions;
using DewCore.Abstract.Logger;
using DewCore.Logger;
using System.Net;
using System.Linq;

namespace DewCore.RestClient
{
    /// <summary>
    /// RESTClient class - a class for REST Requests
    /// </summary>
    public class RESTClient : IRESTClient
    {
        private CookieContainer _cookieContainer = new CookieContainer();
        private CancellationToken _cancellationToken = default(CancellationToken);
        private HttpClientHandler _handler = null;
        private static ILogger _debugger = new DewDebug();
        /// <summary>
        /// Enable debug
        /// </summary>
        public static bool DebugOn = false;
        private HeadersValidation doValidation = HeadersValidation.Yes;
        private HttpClient GetClient()
        {
            return _handler != null ? new HttpClient(_handler) : new HttpClient();
        }
        private void SetCookies(CookieCollection coll, string url)
        {
            if (coll.Count > 0)
            {
                if (_handler == null)
                    _handler = new HttpClientHandler();
                _handler.CookieContainer = new CookieContainer();
                var uri = new Uri(url);
                string baseUrl = uri.Scheme + "//" + uri.Host + ":" + uri.Port;
                _handler.CookieContainer.Add(new Uri(baseUrl), coll);
            }
        }
        private void Log(string text)
        {
            if (DebugOn)
            {
                _debugger.WriteLine(text);
            }
        }
        /// <summary>
        /// Get cookie
        /// </summary>
        /// <param name="key"></param>
        /// <param name="baseUrl"></param>
        /// <returns></returns>
        public Cookie GetCookie(string key, string baseUrl)
        {
            foreach (var item in _cookieContainer.GetCookies(new Uri(baseUrl)))
            {
                var c = item as Cookie;
                if (c.Name == key)
                    return c;
            };
            return null;
        }
        /// <summary>
        /// Get cookie
        /// </summary>
        /// <param name="baseUrl"></param>
        /// <returns></returns>
        public CookieCollection GetCookies(string baseUrl)
        {
            return _cookieContainer.GetCookies(new Uri(baseUrl));
        }
        /// <summary>
        /// Set logger
        /// </summary>
        /// <param name="debugger"></param>
        public static void SetDebugger(ILogger debugger)
        {
            RESTClient._debugger = debugger;
        }
        private HttpRequestHeaders SetHeaders(HttpRequestHeaders headers, Dictionary<string, string> myHeaders)
        {
            var headersCollection = headers;
            if (myHeaders != null)
            {
                foreach (var item in myHeaders)
                {
                    if (this.doValidation == HeadersValidation.Yes)
                        headersCollection.Add(item.Key, item.Value);
                    else
                        headersCollection.TryAddWithoutValidation(item.Key, item.Value);
                }
            }
            return headersCollection;
        }
        /// <summary>
        /// Return an instance of IRESTResponse
        /// </summary>
        /// <param name="httpResponseMessage"></param>
        /// <returns></returns>
        public IRESTResponse GetRESTResponse(HttpResponseMessage httpResponseMessage)
        {
            return new RESTResponse(httpResponseMessage);
        }

        /// <summary>
        /// Check if a string is a valid URL
        /// </summary>
        /// <param name="url">The URL</param>
        /// <returns>True if url is valid, false else</returns>
        public bool IsValidUrl(string url)
        {
            bool Result = false;
            Uri MyUri = null;
            try
            {
                MyUri = new Uri(url);
            }
            catch (UriFormatException)
            {
                MyUri = null;
            }
            if (MyUri != null)
            {
                if (MyUri.Scheme.ToLower() == "http" || MyUri.Scheme.ToLower() == "https")
                    Result = true;
            }
            return Result;
        }
        /// <summary>
        /// Perform a DELETE request
        /// </summary>
        /// <param name="url">The api url</param>
        /// <param name="args">The querystring args</param>
        /// <param name="headers">The _headers</param>
        /// <exception cref="ArgumentException">The url is not valid</exception>
        /// <exception cref="InvalidOperationException">Probably misused header value</exception>
        /// <returns>RESTResponse, null if something goes wrong</returns>
        public async Task<IRESTResponse> PerformDeleteRequestAsync(string url, Dictionary<string, string> args = null, Dictionary<string, string> headers = null)
        {
            IRESTResponse response = null;
            using (HttpClient httpClient = GetClient())
            {
                HttpRequestHeaders headersCollection = null;
                string queryArgs = "";
                if (!this.IsValidUrl(url))
                    throw new ArgumentException("The current url is not valid");
                try
                {
                    headersCollection = SetHeaders(httpClient.DefaultRequestHeaders, headers);
                    if (args != null)
                    {
                        queryArgs = "?";
                        foreach (var item in args)
                        {
                            queryArgs = queryArgs + item.Key + "=" + item.Value + "&";
                        }
                        queryArgs = queryArgs.Substring(0, queryArgs.Length - 1);
                    }
                    //Send the DELETE request
                    this.Log($"Performing DELETE Request to: {url} with args:{queryArgs}");
                    HttpResponseMessage httpResponse = await httpClient.DeleteAsync(new Uri(url + queryArgs), this._cancellationToken);
                    this.Log($"With response status code: {httpResponse.StatusCode}");
                    response = this.GetRESTResponse(httpResponse);
                }
                catch (Exception)
                {
                    throw;
                }
            }
            return response;
        }
        /// <summary>
        /// Perform a GET request
        /// </summary>
        /// <param name="url">The api url</param>
        /// <param name="args">The querystring args</param>
        /// <param name="headers">The _headers</param>
        /// <exception cref="ArgumentException">The url is not valid</exception>
        /// <exception cref="InvalidOperationException">Probably misused header value</exception>
        /// <returns>RESTResponse, null if something goes wrong</returns>
        public async Task<IRESTResponse> PerformGetRequestAsync(string url, Dictionary<string, string> args = null, Dictionary<string, string> headers = null)
        {

            IRESTResponse response = null;
            using (HttpClient httpClient = GetClient())
            {
                HttpRequestHeaders headersCollection = null;
                string queryArgs = "";

                if (!this.IsValidUrl(url))
                    throw new ArgumentException("The current url is not valid");
                try
                {
                    headersCollection = SetHeaders(httpClient.DefaultRequestHeaders, headers);
                    if (args != null)
                    {
                        queryArgs = "?";
                        foreach (var item in args)
                        {
                            queryArgs = queryArgs + item.Key + "=" + item.Value + "&";
                        }
                        queryArgs = queryArgs.Substring(0, queryArgs.Length - 1);
                    }
                    //Send the GET request
                    this.Log($"Performing GET Request to: {url} with args:{queryArgs}");
                    HttpResponseMessage httpResponse = await httpClient.GetAsync(new Uri(url + queryArgs), this._cancellationToken);
                    this.Log($"With response status code: {httpResponse.StatusCode}");
                    response = this.GetRESTResponse(httpResponse);
                }
                catch (Exception)
                {
                    throw;
                }
            }
            return response;
        }
        /// <summary>
        /// Perform a HEAD request
        /// </summary>
        /// <param name="url">The api url</param>
        /// <param name="args">The querystring args</param>
        /// <param name="headers">The _headers</param>
        /// <exception cref="ArgumentException">The url is not valid</exception>
        /// <exception cref="InvalidOperationException">Probably misused header value</exception>
        /// <returns>RESTResponse, null if something goes wrong</returns>
        public async Task<IRESTResponse> PerformHeadRequestAsync(string url, Dictionary<string, string> args = null, Dictionary<string, string> headers = null)
        {
            IRESTResponse response = null;
            using (HttpClient httpClient = GetClient())
            {
                HttpRequestHeaders headersCollection = null;
                string queryArgs = "";

                if (!this.IsValidUrl(url))
                    throw new ArgumentException("The current url is not valid");
                try
                {
                    headersCollection = SetHeaders(httpClient.DefaultRequestHeaders, headers);
                    if (args != null)
                    {
                        queryArgs = "?";
                        foreach (var item in args)
                        {
                            queryArgs = queryArgs + item.Key + "=" + item.Value + "&";
                        }
                        queryArgs = queryArgs.Substring(0, queryArgs.Length - 1);
                    }
                    //Send the HEAD request
                    this.Log($"Performing HEAD Request to: {url} with args:{queryArgs}");
                    HttpResponseMessage httpResponse = await httpClient.HeadAsync(new Uri(url + queryArgs), this._cancellationToken);
                    this.Log($"With response status code: {httpResponse.StatusCode}");
                    response = this.GetRESTResponse(httpResponse);
                }
                catch (Exception)
                {
                    throw;
                }
            }
            return response;
        }
        /// <summary>
        /// Perform an OPTIONS request
        /// </summary>
        /// <param name="url">The API Host url</param>
        /// <param name="args">Query string args</param>
        /// <param name="headers">Dictionary of _headers</param>
        /// <param name="content">The message content</param>
        /// <exception cref="ArgumentException">The url is not valid</exception>
        /// <exception cref="InvalidOperationException">Probably misused header value</exception>
        /// <returns>RESTResponse, null if something goes wrong</returns>
        public async Task<IRESTResponse> PerformOptionsRequestAsync(string url, Dictionary<string, string> args = null, Dictionary<string, string> headers = null, HttpContent content = null)
        {
            IRESTResponse response = null;
            using (HttpClient httpClient = GetClient())
            {
                HttpRequestHeaders headersCollection = null;
                string queryArgs = "";
                try
                {
                    headersCollection = SetHeaders(httpClient.DefaultRequestHeaders, headers);
                    if (args != null)
                    {
                        queryArgs = "?";
                        foreach (var item in args)
                        {
                            queryArgs = queryArgs + item.Key + "=" + item.Value + "&";
                        }
                        queryArgs = queryArgs.Substring(0, queryArgs.Length - 1);
                    }
                    //Send the OPTIONS request
                    this.Log($"Performing OPTIONS Request to: {url} with args:{queryArgs}");
                    HttpResponseMessage httpResponse = await httpClient.PatchAsync(new Uri(url + queryArgs), content, this._cancellationToken);
                    this.Log($"With response status code: {httpResponse.StatusCode}");
                    response = this.GetRESTResponse(httpResponse);
                }
                catch (Exception)
                {
                    throw;
                }
            }
            return response;
        }
        /// <summary>
        /// Perform a PATCH request
        /// </summary>
        /// <param name="url">The API Host url</param>
        /// <param name="args">Query string args</param>
        /// <param name="headers">Dictionary of _headers</param>
        /// <param name="content">The message content</param>
        /// <exception cref="ArgumentException">The url is not valid</exception>
        /// <exception cref="InvalidOperationException">Probably misused header value</exception>
        /// <returns>RESTResponse, null if something goes wrong</returns>
        public async Task<IRESTResponse> PerformPatchRequestAsync(string url, Dictionary<string, string> args = null, Dictionary<string, string> headers = null, HttpContent content = null)
        {
            IRESTResponse response = null;
            using (HttpClient httpClient = GetClient())
            {
                HttpRequestHeaders headersCollection = null;
                string queryArgs = "";
                if (!this.IsValidUrl(url))
                    throw new ArgumentException("The current url is not valid");
                try
                {
                    headersCollection = SetHeaders(httpClient.DefaultRequestHeaders, headers);
                    if (args != null)
                    {
                        queryArgs = "?";
                        foreach (var item in args)
                        {
                            queryArgs = queryArgs + item.Key + "=" + item.Value + "&";
                        }
                        queryArgs = queryArgs.Substring(0, queryArgs.Length - 1);
                    }
                    //Send the PATCH request
                    this.Log($"Performing PATCH Request to: {url} with args:{queryArgs}");
                    HttpResponseMessage httpResponse = await httpClient.PatchAsync(new Uri(url + queryArgs), content, this._cancellationToken);
                    this.Log($"With response status code: {httpResponse.StatusCode}");
                    response = this.GetRESTResponse(httpResponse);
                }
                catch (Exception)
                {
                    throw;
                }
            }
            return response;
        }
        /// <summary>
        /// Perform a POST request
        /// </summary>
        /// <param name="url">The API Host url</param>
        /// <param name="args">Query string args</param>
        /// <param name="headers">Dictionary of _headers</param>
        /// <param name="content">The message content</param>
        /// <exception cref="ArgumentException">The url is not valid</exception>
        /// <exception cref="InvalidOperationException">Probably misused header value</exception>
        /// <returns>RESTResponse, null if something goes wrong</returns>
        public async Task<IRESTResponse> PerformPostRequestAsync(string url, Dictionary<string, string> args = null, Dictionary<string, string> headers = null, HttpContent content = null)
        {
            IRESTResponse response = null;
            using (HttpClient httpClient = GetClient())
            {
                HttpRequestHeaders headersCollection = null;
                string queryArgs = "";
                if (!this.IsValidUrl(url))
                    throw new ArgumentException("The current url is not valid");
                try
                {
                    headersCollection = SetHeaders(httpClient.DefaultRequestHeaders, headers);
                    if (args != null)
                    {
                        queryArgs = "?";
                        foreach (var item in args)
                        {
                            queryArgs = queryArgs + item.Key + "=" + item.Value + "&";
                        }
                        queryArgs = queryArgs.Substring(0, queryArgs.Length - 1);
                    }

                    //Send the POST request
                    this.Log($"Performing POST Request to: {url} with args:{await content?.ReadAsStringAsync()}");
                    HttpResponseMessage httpResponse = await httpClient.PostAsync(new Uri(url + queryArgs), content, this._cancellationToken);
                    this.Log($"With response status code: {httpResponse.StatusCode}");
                    response = this.GetRESTResponse(httpResponse); ;
                }
                catch (Exception)
                {
                    throw;
                }
            }
            return response;
        }
        /// <summary>
        /// Perform a PUT request
        /// </summary>
        /// <param name="url">The API Host url</param>
        /// <param name="args">Query string args</param>
        /// <param name="headers">Dictionary of _headers</param>
        /// <param name="content">The message content</param>
        /// <exception cref="ArgumentException">The url is not valid</exception>
        /// <exception cref="InvalidOperationException">Probably misused header value</exception>
        /// <returns>RESTResponse, null if something goes wrong</returns>
        public async Task<IRESTResponse> PerformPutRequestAsync(string url, Dictionary<string, string> args = null, Dictionary<string, string> headers = null, HttpContent content = null)
        {
            IRESTResponse response = null;
            using (HttpClient httpClient = GetClient())
            {
                HttpRequestHeaders headersCollection = null;
                string queryArgs = "";
                if (!this.IsValidUrl(url))
                    throw new ArgumentException("The current url is not valid");
                try
                {
                    headersCollection = SetHeaders(httpClient.DefaultRequestHeaders, headers);
                    if (args != null)
                    {
                        queryArgs = "?";
                        foreach (var item in args)
                        {
                            queryArgs = queryArgs + item.Key + "=" + item.Value + "&";
                        }
                        queryArgs = queryArgs.Substring(0, queryArgs.Length - 1);
                    }
                    //Send the PUT request
                    this.Log($"Performing PUT Request to: {url} with args:{await content?.ReadAsStringAsync()}");
                    HttpResponseMessage httpResponse = await httpClient.PutAsync(new Uri(url + queryArgs), content, this._cancellationToken);
                    this.Log($"With response status code: {httpResponse.StatusCode}");
                    response = this.GetRESTResponse(httpResponse);
                }
                catch (Exception)
                {
                    throw;
                }
            }
            return response;
        }
        /// <summary>
        /// Perform a Request by an IRESTRequest object
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<IRESTResponse> PerformRequestAsync(IRESTRequest request)
        {
            IRESTResponse response = null;
            _handler = request.GetHandler();
            SetCookies(request.GetCookieCollection(), request.GetUrl());
            switch (request.GetMethod())
            {
                case Method.POST:
                    {
                        response = await this.PerformPostRequestAsync(request.GetUrl(), request.GetQueryArgs(), request.GetHeaders(), request.GetContent());
                        break;
                    }
                case Method.PUT:
                    {
                        response = await this.PerformPutRequestAsync(request.GetUrl(), request.GetQueryArgs(), request.GetHeaders(), request.GetContent());
                        break;
                    }
                case Method.GET:
                    {
                        response = await this.PerformGetRequestAsync(request.GetUrl(), request.GetQueryArgs(), request.GetHeaders());
                        break;
                    }
                case Method.PATCH:
                    {
                        response = await this.PerformPatchRequestAsync(request.GetUrl(), request.GetQueryArgs(), request.GetHeaders(), request.GetContent());
                        break;
                    }
                case Method.OPTIONS:
                    {
                        response = await this.PerformOptionsRequestAsync(request.GetUrl(), request.GetQueryArgs(), request.GetHeaders(), request.GetContent());
                        break;
                    }
                case Method.HEAD:
                    {
                        response = await this.PerformHeadRequestAsync(request.GetUrl(), request.GetQueryArgs(), request.GetHeaders());
                        break;
                    }
                case Method.DELETE:
                    {
                        response = await this.PerformDeleteRequestAsync(request.GetUrl(), request.GetQueryArgs(), request.GetHeaders());
                        break;
                    }
            }
            return response;
        }
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="cancellationToken"></param>
        public RESTClient(CancellationToken cancellationToken = default(CancellationToken)) { }
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="h"></param>
        /// <param name="cancellationToken"></param>
        public RESTClient(HeadersValidation h, CancellationToken cancellationToken = default(CancellationToken))
        {
            this.doValidation = h;
        }
        /// <summary>
        /// Set yes if you want validation when set _headers
        /// </summary>
        /// <param name="h"></param>
        public void SetValidation(HeadersValidation h)
        {
            this.doValidation = h;
        }
    }

}
