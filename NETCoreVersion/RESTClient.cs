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
        Task<RESTResponse> PerformGetRequestAsync(string url, Dictionary<string, string> args = null, Dictionary<string, string> headers = null);
        /// <summary>
        /// Perform a POST request
        /// </summary>
        /// <param name="url">The API Host url</param>
        /// <param name="args">Query string args</param>
        /// <param name="headers">Dictionary of headers</param>
        /// <param name="content">The message content</param>
        /// <returns></returns>
        Task<RESTResponse> PerformPostRequestAsync(string url, Dictionary<string, string> args = null, Dictionary<string, string> headers = null, HttpContent content = null);
        /// <summary>
        /// Perform a PUT request
        /// </summary>
        /// <param name="url">The API Host url</param>
        /// <param name="args">Query string args</param>
        /// <param name="headers">Dictionary of headers</param>
        /// <param name="content">The message content</param>
        /// <returns></returns>
        Task<RESTResponse> PerformPutRequestAsync(string url, Dictionary<string, string> args = null, Dictionary<string, string> headers = null, HttpContent content = null);
        /// <summary>
        /// Perform a DELETE request
        /// </summary>
        /// <param name="url">The API Host url</param>
        /// <param name="args">Query string args</param>
        /// <param name="headers">Dictionary of headers</param>
        /// <returns></returns>
        Task<RESTResponse> PerformDeleteRequestAsync(string url, Dictionary<string, string> args = null, Dictionary<string, string> headers = null);
        /// <summary>
        /// Perform a PATCH request
        /// </summary>
        /// <param name="url">The API Host url</param>
        /// <param name="args">Query string args</param>
        /// <param name="headers">Dictionary of headers</param>
        /// <param name="content">The message content</param>
        /// <returns></returns>
        Task<RESTResponse> PerformPatchRequestAsync(string url, Dictionary<string, string> args = null, Dictionary<string, string> headers = null, HttpContent content = null);
        /// <summary>
        /// Perform an OPTIONS request
        /// </summary>
        /// <param name="url">The API Host url</param>
        /// <param name="args">Query string args</param>
        /// <param name="headers">Dictionary of headers</param>
        /// <param name="content">The message content</param>
        /// <returns></returns>
        Task<RESTResponse> PerformOptionsRequestAsync(string url, Dictionary<string, string> args = null, Dictionary<string, string> headers = null, HttpContent content = null);
        /// <summary>
        /// Perform a HEAD request
        /// </summary>
        /// <param name="url">The API Host url</param>
        /// <param name="args">Query string args</param>
        /// <param name="headers">Dictionary of headers</param>
        /// <returns></returns>
        Task<RESTResponse> PerformHeadRequestAsync(string url, Dictionary<string, string> args = null, Dictionary<string, string> headers = null);
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
    }
    /// <summary>
    /// Standard response object
    /// </summary>
    public class RESTResponse : IRESTResponse
    {
        /// <summary>
        /// The HttpResponseMessage object
        /// </summary>
        public HttpResponseMessage Response = null;
        /// <summary>
        /// Get the response status code
        /// </summary>
        /// <exception cref="NullReferenceException">If Response object is null</exception>
        /// <returns></returns>
        public HttpStatusCode GetStatusCode()
        {
            if (this.Response == null)
                throw new NullReferenceException();
            return this.Response.StatusCode;
        }
        /// <summary>
        /// Check if the status code is succesful 
        /// </summary>
        /// <exception cref="NullReferenceException">If Response object is null</exception>
        /// <returns></returns>
        public bool IsSuccesStatusCode()
        {
            if (this.Response == null)
                throw new NullReferenceException();
            return (int)this.Response.StatusCode >= 200 && (int)this.Response.StatusCode < 300;
        }
        /// <summary>
        /// Check if the status code is redirect
        /// </summary>
        /// <exception cref="NullReferenceException">If Response object is null</exception>
        /// <returns></returns>
        public bool IsRedirectedStatusCode()
        {
            if (this.Response == null)
                throw new NullReferenceException();
            return (int)this.Response.StatusCode >= 300 && (int)this.Response.StatusCode < 400;
        }
        /// <summary>
        /// Check if the status code is an error
        /// </summary>
        /// <exception cref="NullReferenceException">If Response object is null</exception>
        /// <returns></returns>
        public bool IsErrorStatusCode()
        {
            if (this.Response == null)
                throw new NullReferenceException();
            return (int)this.Response.StatusCode >= 400 && (int)this.Response.StatusCode < 500;
        }
        /// <summary>
        /// Check if the status code is a fault
        /// </summary>
        /// <exception cref="NullReferenceException">If Response object is null</exception>
        /// <returns></returns>
        public bool IsFaultStatusCode()
        {
            if (this.Response == null)
                throw new NullReferenceException();
            return (int)this.Response.StatusCode >= 500 && (int)this.Response.StatusCode < 600;
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
            return this.Response.Content != null ? await this.Response.Content.ReadAsStringAsync() : null;
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
            this.Response = response;
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
        public async Task<RESTResponse> PerformDeleteRequestAsync(string url, Dictionary<string, string> args = null, Dictionary<string, string> headers = null)
        {
            HttpClient httpClient = new HttpClient();
            HttpRequestHeaders headersCollection = null;
            RESTResponse response = null;
            string queryArgs = "";
            if (!this.IsValidUrl(url))
                throw new ArgumentException("The current url is not valid");
            try
            {
                headersCollection = httpClient.DefaultRequestHeaders;
                if (headers != null)
                {
                    foreach (var item in headers)
                    {
                        headersCollection.Add(item.Key, item.Value);
                    }
                }
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
                response = new RESTResponse(httpResponse);

            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
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
        public async Task<RESTResponse> PerformGetRequestAsync(string url, Dictionary<string, string> args = null, Dictionary<string, string> headers = null)
        {
            HttpClient httpClient = new HttpClient();
            HttpRequestHeaders headersCollection = null;
            string queryArgs = "";
            RESTResponse response = null;
            if (!this.IsValidUrl(url))
                throw new ArgumentException("The current url is not valid");
            try
            {
                headersCollection = httpClient.DefaultRequestHeaders;
                if (headers != null)
                {
                    foreach (var item in headers)
                    {
                        headersCollection.Add(item.Key, item.Value);
                    }
                }
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
                response = new RESTResponse(httpResponse);
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message); ;
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
        public async Task<RESTResponse> PerformHeadRequestAsync(string url, Dictionary<string, string> args = null, Dictionary<string, string> headers = null)
        {
            HttpClient httpClient = new HttpClient();
            HttpRequestHeaders headersCollection = null;
            string queryArgs = "";
            RESTResponse response = null;
            if (!this.IsValidUrl(url))
                throw new ArgumentException("The current url is not valid");
            try
            {
                headersCollection = httpClient.DefaultRequestHeaders;
                if (headers != null)
                {
                    foreach (var item in headers)
                    {
                        headersCollection.Add(item.Key, item.Value);
                    }
                }
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
                response = new RESTResponse(httpResponse);
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message); ;
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
        public async Task<RESTResponse> PerformOptionsRequestAsync(string url, Dictionary<string, string> args = null, Dictionary<string, string> headers = null, HttpContent content = null)
        {
            HttpClient httpClient = new HttpClient();
            HttpRequestHeaders headersCollection = null;
            string queryArgs = "";
            RESTResponse response = null;
            try
            {
                headersCollection = httpClient.DefaultRequestHeaders;
                if (headers != null)
                {
                    foreach (var item in headers)
                    {
                        headersCollection.Add(item.Key, item.Value);
                    }
                }
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
                response = new RESTResponse(httpResponse);
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message); ;
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
        public async Task<RESTResponse> PerformPatchRequestAsync(string url, Dictionary<string, string> args = null, Dictionary<string, string> headers = null, HttpContent content = null)
        {
            HttpClient httpClient = new HttpClient();
            HttpRequestHeaders headersCollection = null;
            string queryArgs = "";
            RESTResponse response = null;
            if (!this.IsValidUrl(url))
                throw new ArgumentException("The current url is not valid");
            try
            {
                headersCollection = httpClient.DefaultRequestHeaders;
                if (headers != null)
                {
                    foreach (var item in headers)
                    {
                        headersCollection.Add(item.Key, item.Value);
                    }
                }
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
                response = new RESTResponse(httpResponse);
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message); ;
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
        public async Task<RESTResponse> PerformPostRequestAsync(string url, Dictionary<string, string> args = null, Dictionary<string, string> headers = null, HttpContent content = null)
        {
            HttpClient httpClient = new HttpClient();
            HttpRequestHeaders headersCollection = null;
            string queryArgs = "";
            RESTResponse response = null;
            if (!this.IsValidUrl(url))
                throw new ArgumentException("The current url is not valid");
            try
            {
                headersCollection = httpClient.DefaultRequestHeaders;
                if (headers != null)
                {
                    foreach (var item in headers)
                    {
                        headersCollection.Add(item.Key, item.Value);
                    }
                }
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
                response = new RESTResponse(httpResponse);
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message); ;
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
        public async Task<RESTResponse> PerformPutRequestAsync(string url, Dictionary<string, string> args = null, Dictionary<string, string> headers = null, HttpContent content = null)
        {
            HttpClient httpClient = new HttpClient();
            HttpRequestHeaders headersCollection = null;
            string queryArgs = "";
            RESTResponse response = null;
            if (!this.IsValidUrl(url))
                throw new ArgumentException("The current url is not valid");
            try
            {
                headersCollection = httpClient.DefaultRequestHeaders;
                if (headers != null)
                {
                    foreach (var item in headers)
                    {
                        headersCollection.Add(item.Key, item.Value);
                    }
                }
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
                response = new RESTResponse(httpResponse);
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message); ;
            }
            return response;
        }
    }
}
