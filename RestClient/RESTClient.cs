using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net;
using System.Net.Http.Headers;
using DewInterfaces.DewRestClient;
using DewInterfaces;

namespace DewCore.DewRestClient
{
    /// <summary>
    /// Log into debug output
    /// </summary>
    public class DewDebug : IDewLogger
    {
        /// <summary>
        /// Write text
        /// </summary>
        /// <param name="text"></param>
        public void Write(string text)
        {
            Debug.Write(text);
        }
        /// <summary>
        /// Write formatted text
        /// </summary>
        /// <param name="text"></param>
        /// <param name="args"></param>
        public void Write(string text, object[] args)
        {
            Debug.Write(String.Format(text, args));
        }
        /// <summary>
        /// Write text and new line
        /// </summary>
        /// <param name="text"></param>
        public void WriteLine(string text)
        {
            Debug.WriteLine(text);
        }
        /// <summary>
        /// Write formatted text and new line
        /// </summary>
        /// <param name="text"></param>
        /// <param name="args"></param>
        public void WriteLine(string text, object[] args)
        {
            Debug.Write(String.Format(text, args));
        }
    }
    /// <summary>
    /// RESTClient class - a class for REST Requests
    /// </summary>
    public class RESTClient : IRESTClient
    {
        private static IDewLogger debugger = new DewDebug();
        /// <summary>
        /// Enable debug
        /// </summary>
        public static bool DebugOn = false;
        private HeadersValidation doValidation = HeadersValidation.Yes;
        private void Log(string text)
        {
            if(DebugOn)
            {
                debugger.WriteLine(text);
            }
        }
        /// <summary>
        /// Set logger
        /// </summary>
        /// <param name="debugger"></param>
        public static void SetDebugger(IDewLogger debugger)
        {
            RESTClient.debugger = debugger;
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
        /// <param name="headers">The headers</param>
        /// <exception cref="ArgumentException">The url is not valid</exception>
        /// <exception cref="InvalidOperationException">Probably misused header value</exception>
        /// <returns>RESTResponse, null if something goes wrong</returns>
        public async Task<IRESTResponse> PerformDeleteRequestAsync(string url, Dictionary<string, string> args = null, Dictionary<string, string> headers = null)
        {
            IRESTResponse response = null;
            using (HttpClient httpClient = new HttpClient())
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
                    HttpResponseMessage httpResponse = await httpClient.DeleteAsync(new Uri(url + queryArgs));
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
        /// <param name="headers">The headers</param>
        /// <exception cref="ArgumentException">The url is not valid</exception>
        /// <exception cref="InvalidOperationException">Probably misused header value</exception>
        /// <returns>RESTResponse, null if something goes wrong</returns>
        public async Task<IRESTResponse> PerformGetRequestAsync(string url, Dictionary<string, string> args = null, Dictionary<string, string> headers = null)
        {

            IRESTResponse response = null;
            using (HttpClient httpClient = new HttpClient())
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
                    HttpResponseMessage httpResponse = await httpClient.GetAsync(new Uri(url + queryArgs));
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
        /// <param name="headers">The headers</param>
        /// <exception cref="ArgumentException">The url is not valid</exception>
        /// <exception cref="InvalidOperationException">Probably misused header value</exception>
        /// <returns>RESTResponse, null if something goes wrong</returns>
        public async Task<IRESTResponse> PerformHeadRequestAsync(string url, Dictionary<string, string> args = null, Dictionary<string, string> headers = null)
        {
            IRESTResponse response = null;
            using (HttpClient httpClient = new HttpClient())
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
                    HttpResponseMessage httpResponse = await httpClient.HeadAsync(new Uri(url + queryArgs));
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
        /// <param name="headers">Dictionary of headers</param>
        /// <param name="content">The message content</param>
        /// <exception cref="ArgumentException">The url is not valid</exception>
        /// <exception cref="InvalidOperationException">Probably misused header value</exception>
        /// <returns>RESTResponse, null if something goes wrong</returns>
        public async Task<IRESTResponse> PerformOptionsRequestAsync(string url, Dictionary<string, string> args = null, Dictionary<string, string> headers = null, HttpContent content = null)
        {
            IRESTResponse response = null;
            using (HttpClient httpClient = new HttpClient())
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
                    HttpResponseMessage httpResponse = await httpClient.PatchAsync(new Uri(url + queryArgs), content);
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
        /// <param name="headers">Dictionary of headers</param>
        /// <param name="content">The message content</param>
        /// <exception cref="ArgumentException">The url is not valid</exception>
        /// <exception cref="InvalidOperationException">Probably misused header value</exception>
        /// <returns>RESTResponse, null if something goes wrong</returns>
        public async Task<IRESTResponse> PerformPatchRequestAsync(string url, Dictionary<string, string> args = null, Dictionary<string, string> headers = null, HttpContent content = null)
        {
            IRESTResponse response = null;
            using (HttpClient httpClient = new HttpClient())
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
                    HttpResponseMessage httpResponse = await httpClient.PatchAsync(new Uri(url + queryArgs), content);
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
        /// <param name="headers">Dictionary of headers</param>
        /// <param name="content">The message content</param>
        /// <exception cref="ArgumentException">The url is not valid</exception>
        /// <exception cref="InvalidOperationException">Probably misused header value</exception>
        /// <returns>RESTResponse, null if something goes wrong</returns>
        public async Task<IRESTResponse> PerformPostRequestAsync(string url, Dictionary<string, string> args = null, Dictionary<string, string> headers = null, HttpContent content = null)
        {
            IRESTResponse response = null;
            using (HttpClient httpClient = new HttpClient())
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
                    this.Log($"Performing POST Request to: {url} with args:{queryArgs}");
                    HttpResponseMessage httpResponse = await httpClient.PostAsync(new Uri(url + queryArgs), content);
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
        /// <param name="headers">Dictionary of headers</param>
        /// <param name="content">The message content</param>
        /// <exception cref="ArgumentException">The url is not valid</exception>
        /// <exception cref="InvalidOperationException">Probably misused header value</exception>
        /// <returns>RESTResponse, null if something goes wrong</returns>
        public async Task<IRESTResponse> PerformPutRequestAsync(string url, Dictionary<string, string> args = null, Dictionary<string, string> headers = null, HttpContent content = null)
        {
            IRESTResponse response = null;
            using (HttpClient httpClient = new HttpClient())
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
                    this.Log($"Performing PUT Request to: {url} with args:{queryArgs}");
                    HttpResponseMessage httpResponse = await httpClient.PutAsync(new Uri(url + queryArgs), content);
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
        public async Task<IRESTResponse> PerformRequest(IRESTRequest request)
        {
            IRESTResponse response = null;
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
            }
            return response;
        }
        /// <summary>
        /// Constructor
        /// </summary>
        public RESTClient() { }
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="h"></param>
        public RESTClient(HeadersValidation h)
        {
            this.doValidation = h;
        }
        /// <summary>
        /// Set yes if you want validation when set headers
        /// </summary>
        /// <param name="h"></param>
        public void SetValidation(HeadersValidation h)
        {
            this.doValidation = h;
        }
    }

}
