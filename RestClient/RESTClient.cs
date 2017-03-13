using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net;
using System.Net.Http.Headers;

namespace DewCore.RestClient
{
    /// <summary>
    /// Http header validation type
    /// </summary>
    public enum HeadersValidation
    {
        /// <summary>
        /// Yes
        /// </summary>
        Yes,
        /// <summary>
        /// No
        /// </summary>
        No
    }
    /// <summary>
    /// RESTClient http status macrotypes
    /// </summary>
    public enum HttpStatusType
    {
        /// <summary>
        /// 2xx
        /// </summary>
        Successful,
        /// <summary>
        /// 3xx
        /// </summary>
        Redirected,
        /// <summary>
        /// 4xx
        /// </summary>
        Error,
        /// <summary>
        /// 5xxx
        /// </summary>
        Fault
    }
    /// <summary>
    /// HTTP methods
    /// </summary>
    public enum Method
    {
        /// <summary>
        /// POST
        /// </summary>
        POST,
        /// <summary>
        /// PUT
        /// </summary>
        PUT,
        /// <summary>
        /// GET
        /// </summary>
        GET,
        /// <summary>
        /// PATCH
        /// </summary>
        PATCH,
        /// <summary>
        /// OPTIONS
        /// </summary>
        OPTIONS,
        /// <summary>
        /// HEAD
        /// </summary>
        HEAD
    }

    /// <summary>
    /// Rest client library interface
    /// </summary>
    public interface IRESTClient
    {
        /// <summary>
        /// Check if an url is valid
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        bool IsValidUrl(string url);
        /// <summary>
        /// Perform a GET request
        /// </summary>
        /// <param name="url">The API Host url</param>
        /// <param name="args">Query string args</param>
        /// <param name="headers">Dictionary of headers</param>
        /// <returns></returns>
        Task<IRESTResponse> PerformGetRequestAsync(string url, Dictionary<string, string> args = null, Dictionary<string, string> headers = null);
        /// <summary>
        /// Perform a POST request
        /// </summary>
        /// <param name="url">The API Host url</param>
        /// <param name="args">Query string args</param>
        /// <param name="headers">Dictionary of headers</param>
        /// <param name="content">The message content</param>
        /// <returns></returns>
        Task<IRESTResponse> PerformPostRequestAsync(string url, Dictionary<string, string> args = null, Dictionary<string, string> headers = null, HttpContent content = null);
        /// <summary>
        /// Perform a PUT request
        /// </summary>
        /// <param name="url">The API Host url</param>
        /// <param name="args">Query string args</param>
        /// <param name="headers">Dictionary of headers</param>
        /// <param name="content">The message content</param>
        /// <returns></returns>
        Task<IRESTResponse> PerformPutRequestAsync(string url, Dictionary<string, string> args = null, Dictionary<string, string> headers = null, HttpContent content = null);
        /// <summary>
        /// Perform a DELETE request
        /// </summary>
        /// <param name="url">The API Host url</param>
        /// <param name="args">Query string args</param>
        /// <param name="headers">Dictionary of headers</param>
        /// <returns></returns>
        Task<IRESTResponse> PerformDeleteRequestAsync(string url, Dictionary<string, string> args = null, Dictionary<string, string> headers = null);
        /// <summary>
        /// Perform a PATCH request
        /// </summary>
        /// <param name="url">The API Host url</param>
        /// <param name="args">Query string args</param>
        /// <param name="headers">Dictionary of headers</param>
        /// <param name="content">The message content</param>
        /// <returns></returns>
        Task<IRESTResponse> PerformPatchRequestAsync(string url, Dictionary<string, string> args = null, Dictionary<string, string> headers = null, HttpContent content = null);
        /// <summary>
        /// Perform an OPTIONS request
        /// </summary>
        /// <param name="url">The API Host url</param>
        /// <param name="args">Query string args</param>
        /// <param name="headers">Dictionary of headers</param>
        /// <param name="content">The message content</param>
        /// <returns></returns>
        Task<IRESTResponse> PerformOptionsRequestAsync(string url, Dictionary<string, string> args = null, Dictionary<string, string> headers = null, HttpContent content = null);
        /// <summary>
        /// Perform a HEAD request
        /// </summary>
        /// <param name="url">The API Host url</param>
        /// <param name="args">Query string args</param>
        /// <param name="headers">Dictionary of headers</param>
        /// <returns></returns>
        Task<IRESTResponse> PerformHeadRequestAsync(string url, Dictionary<string, string> args = null, Dictionary<string, string> headers = null);
        /// <summary>
        /// Perform a request using an IRESTRequest object
        /// </summary>
        /// <param name="request">The IRESTRequest object</param>
        /// <returns></returns>
        Task<IRESTResponse> PerformRequest(IRESTRequest request);
        /// <summary>
        /// Return IRESTResponse object from standard HttpResponseMessage
        /// </summary>
        /// <param name="httpResponseMessage"></param>
        /// <returns></returns>
        IRESTResponse GetRESTResponse(HttpResponseMessage httpResponseMessage);
    }
    /// <summary>
    /// Response interface
    /// </summary>
    public interface IRESTResponse
    {
        /// <summary>
        /// Get the response status code
        /// </summary>
        /// <exception cref="NullReferenceException">If Response object is null</exception>
        /// <returns></returns>
        HttpStatusCode GetStatusCode();
        /// <summary>
        /// Return response body as string
        /// </summary>
        /// <returns></returns>
        Task<string> ReadResponseAsStringAsync();
        /// <summary>
        /// Return the HttpResponseMessage directly
        /// </summary>
        /// <returns></returns>
        HttpResponseMessage GetHttpResponse();

    }
    /// <summary>
    /// Request object interface
    /// </summary>
    public interface IRESTRequest
    {

        /// <summary>
        /// Add header to the request
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        void AddHeader(string key, string value);
        /// <summary>
        /// Return the headers
        /// </summary>
        /// <returns></returns>
        Dictionary<string, string> GetHeaders();
        /// <summary>
        /// Add the content to the request
        /// </summary>
        /// <param name="content"></param>
        void AddContent(HttpContent content);
        /// <summary>
        /// Return the request content
        /// </summary>
        /// <returns></returns>
        HttpContent GetContent();
        /// <summary>
        /// Add a query arg
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        void AddQueryArgs(string key, string value);
        /// <summary>
        /// Get the query args
        /// </summary>
        /// <returns></returns>
        Dictionary<string, string> GetQueryArgs();
        /// <summary>
        /// Set the url
        /// </summary>
        /// <exception cref="UriFormatException"></exception>
        /// <param name="url"></param>
        void SetUrl(string url);
        /// <summary>
        /// Return the request url
        /// </summary>
        /// <returns></returns>
        string GetUrl();
        /// <summary>
        /// Return the request method
        /// </summary>
        /// <returns></returns>
        Method GetMethod();
        /// <summary>
        /// Set the method
        /// </summary>
        /// <param name="http"></param>
        void SetMethod(Method http);
    }
    /// <summary>
    /// Standard response object
    /// </summary>
    public class RESTResponse : IRESTResponse, IDisposable
    {
        /// <summary>
        /// The HttpResponseMessage object
        /// </summary>
        private HttpResponseMessage response = null;
        /// <summary>
        /// Get the response status code
        /// </summary>
        /// <exception cref="NullReferenceException">If Response object is null</exception>
        /// <returns></returns>
        public HttpStatusCode GetStatusCode()
        {
            if (this.response == null)
                throw new NullReferenceException();
            return this.response.StatusCode;
        }
        /// <summary>
        /// Check if the status code is succesful 
        /// </summary>
        /// <exception cref="NullReferenceException">If Response object is null</exception>
        /// <returns></returns>
        public bool IsSuccesStatusCode()
        {
            if (this.response == null)
                throw new NullReferenceException();
            return (int)this.response.StatusCode >= 200 && (int)this.response.StatusCode < 300;
        }
        /// <summary>
        /// Check if the status code is redirect
        /// </summary>
        /// <exception cref="NullReferenceException">If Response object is null</exception>
        /// <returns></returns>
        public bool IsRedirectedStatusCode()
        {
            if (this.response == null)
                throw new NullReferenceException();
            return (int)this.response.StatusCode >= 300 && (int)this.response.StatusCode < 400;
        }
        /// <summary>
        /// Check if the status code is an error
        /// </summary>
        /// <exception cref="NullReferenceException">If Response object is null</exception>
        /// <returns></returns>
        public bool IsErrorStatusCode()
        {
            if (this.response == null)
                throw new NullReferenceException();
            return (int)this.response.StatusCode >= 400 && (int)this.response.StatusCode < 500;
        }
        /// <summary>
        /// Check if the status code is a fault
        /// </summary>
        /// <exception cref="NullReferenceException">If Response object is null</exception>
        /// <returns></returns>
        public bool IsFaultStatusCode()
        {
            if (this.response == null)
                throw new NullReferenceException();
            return (int)this.response.StatusCode >= 500 && (int)this.response.StatusCode < 600;
        }
        /// <summary>
        /// Return the http status type
        /// </summary>
        /// <exception cref="NullReferenceException">If Response object is null</exception>
        /// <returns></returns>
        public HttpStatusType GetHttpStatusCodeType()
        {
            HttpStatusType result = HttpStatusType.Successful;
            if (this.IsRedirectedStatusCode())
                result = HttpStatusType.Redirected;
            if (this.IsErrorStatusCode())
                result = HttpStatusType.Error;
            if (this.IsFaultStatusCode())
                result = HttpStatusType.Fault;
            return result;
        }
        /// <summary>
        /// Return response body as string
        /// </summary>
        /// <returns></returns>
        public async Task<string> ReadResponseAsStringAsync()
        {
            return this.response.Content != null ? await this.response.Content.ReadAsStringAsync() : null;
        }
        /// <summary>
        /// Return directly the HttpResponseMessage
        /// </summary>
        /// <returns></returns>
        public HttpResponseMessage GetHttpResponse()
        {
            return this.response;
        }
        /// <summary>
        /// Dispose object
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);

        }
        /// <summary>
        /// Internal dispose handler
        /// </summary>
        /// <param name="disposing"></param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (this.response != null) this.response.Dispose();
            }
        }


        /// <summary>
        /// Constructor
        /// </summary>
        public RESTResponse() { }
        /// <summary>
        /// Constructor with args
        /// </summary>
        /// <param name="response">The HttpResponseMessage</param>
        public RESTResponse(HttpResponseMessage response)
        {
            this.response = response;
        }
    }
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
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="InvalidOperationException"></exception>
        /// <returns></returns>
        public static async Task<HttpResponseMessage> PatchAsync(this HttpClient client, Uri requestUri, HttpContent iContent)
        {
            var method = new HttpMethod("PATCH");
            var request = new HttpRequestMessage(method, requestUri)
            {
                Content = iContent
            };
            HttpResponseMessage response = new HttpResponseMessage();
            try
            {
                response = await client.SendAsync(request);
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
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="InvalidOperationException"></exception>
        /// <returns></returns>
        public static async Task<HttpResponseMessage> HeadAsync(this HttpClient client, Uri requestUri)
        {
            var method = new HttpMethod("HEAD");
            var request = new HttpRequestMessage(method, requestUri);
            HttpResponseMessage response = new HttpResponseMessage();
            try
            {
                response = await client.SendAsync(request);
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
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="InvalidOperationException"></exception>
        /// <returns></returns>
        public static async Task<HttpResponseMessage> OptionsAsync(this HttpClient client, Uri requestUri, HttpContent iContent)
        {
            var method = new HttpMethod("OPTIONS");
            var request = new HttpRequestMessage(method, requestUri)
            {
                Content = iContent
            };
            HttpResponseMessage response = new HttpResponseMessage();
            try
            {
                response = await client.SendAsync(request);
            }
            catch (TaskCanceledException e)
            {
                Debug.WriteLine("ERROR: " + e.ToString());
            }
            return response;
        }
    }
    /// <summary>
    /// RESTClient class - a class for REST Requests
    /// </summary>
    public class RESTClient : IRESTClient
    {
        private HeadersValidation doValidation = HeadersValidation.Yes;

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
                    HttpResponseMessage httpResponse = await httpClient.DeleteAsync(new Uri(url + queryArgs));
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
                    HttpResponseMessage httpResponse = await httpClient.GetAsync(new Uri(url + queryArgs));
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
                    HttpResponseMessage httpResponse = await httpClient.HeadAsync(new Uri(url + queryArgs));
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
                    //Send the PATCH request
                    HttpResponseMessage httpResponse = await httpClient.PatchAsync(new Uri(url + queryArgs), content);
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
                    //Send the PUT request
                    HttpResponseMessage httpResponse = await httpClient.PatchAsync(new Uri(url + queryArgs), content);
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
                    HttpResponseMessage httpResponse = await httpClient.PostAsync(new Uri(url + queryArgs), content);
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
                    HttpResponseMessage httpResponse = await httpClient.PutAsync(new Uri(url + queryArgs), content);
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
    /// <summary>
    /// REST Request class
    /// </summary>
    public class RESTRequest : IRESTRequest
    {
        /// <summary>
        /// Headers
        /// </summary>
        private Dictionary<string, string> headers = new Dictionary<string, string>();
        /// <summary>
        /// Query args
        /// </summary>
        private Dictionary<string, string> queryArgs = new Dictionary<string, string>();
        /// <summary>
        /// url
        /// </summary>
        private string url = "";
        /// <summary>
        /// Content
        /// </summary>
        private HttpContent content = null;
        /// <summary>
        /// Method
        /// </summary>
        private Method method = Method.GET;
        /// <summary>
        /// Add the content to the request
        /// </summary>
        /// <param name="content"></param>
        public void AddContent(HttpContent content)
        {
            this.content = content;
        }
        /// <summary>
        /// Add header to the request
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public void AddHeader(string key, string value)
        {
            this.headers.Add(key, value);
        }
        /// <summary>
        /// Add a query arg
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public void AddQueryArgs(string key, string value)
        {
            this.queryArgs.Add(key, value);
        }
        /// <summary>
        /// Set the url
        /// </summary>
        /// <exception cref="UriFormatException"></exception>
        /// <param name="url"></param>
        public void SetUrl(string url)
        {
            if (IsValidUrl(url))
                this.url = url;
            else
                throw new UriFormatException();
        }
        /// <summary>
        /// Check if a string is a valid URL
        /// </summary>
        /// <param name="url">The URL</param>
        /// <returns>True if url is valid, false else</returns>
        public static bool IsValidUrl(string url)
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
        /// Return the request method
        /// </summary>
        /// <returns></returns>
        public Method GetMethod()
        {
            return this.method;
        }
        /// <summary>
        /// Set the method
        /// </summary>
        /// <param name="method"></param>
        public void SetMethod(Method method)
        {
            this.method = method;
        }
        /// <summary>
        /// Return the headers
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, string> GetHeaders()
        {
            return this.headers;
        }
        /// <summary>
        /// Return the request content
        /// </summary>
        /// <returns></returns>
        public HttpContent GetContent()
        {
            return this.content;
        }
        /// <summary>
        /// Get the query args
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, string> GetQueryArgs()
        {
            return this.queryArgs;
        }
        /// <summary>
        /// Return the request url
        /// </summary>
        /// <returns></returns>
        public string GetUrl()
        {
            return this.url;
        }
        /// <summary>
        /// Constructor with url
        /// </summary>
        /// <param name="url"></param>
        public RESTRequest(string url)
        {
            this.SetUrl(url);
        }
        /// <summary>
        /// Constructor
        /// </summary>
        public RESTRequest() { }
    }
}
