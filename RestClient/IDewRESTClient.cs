using DewCore.DewRestClient;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace RestClient.Interfaces
{
    /// <summary>
    /// Abstract class for certificates
    /// </summary>
    public abstract class RESTCertificateAbstract
    {

    }
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
        HEAD,
        /// <summary>
        /// DELETE
        /// </summary>
        DELETE
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
        Task<IRESTResponse> PerformRequestAsync(IRESTRequest request);
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
        /// <summary>
        /// Return response body as stream
        /// </summary>
        /// <returns></returns>
        Task<Stream> ReadResponseAsStreamAsync();
        /// <summary>
        /// Return response body as byte array
        /// </summary>
        /// <returns></returns>
        Task<byte[]> ReadResponseAsByteArrayAsync();

    }
    /// <summary>
    /// Request object interface
    /// </summary>
    public interface IRESTRequest
    {
        /// <summary>
        /// Set a http message handler
        /// </summary>
        /// <param name="handler"></param>
        void SetHandler(RESTCertificateAbstract handler);
        /// <summary>
        /// Return the http message handler
        /// </summary>
        /// <returns></returns>
        HttpClientHandler GetHandler();
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
        /// Add a new MultipartFormDataContent to HTTPContent request. Be careful, it overwrite the previous HTTPContent, if it exists and is different for MultipartFormData
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="fileName"></param>
        void AddMultipartFormDataContent(string key, byte[] value, string fileName);
        /// <summary>
        /// Add a new MultipartFormDataContent to HTTPContent request. Be careful, it overwrite the previous HTTPContent, if it exists and is different for MultipartFormData
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        void AddMultipartFormDataContent(string key, string value);
        /// <summary>
        /// Add a new MultipartFormDataContent to HTTPContent request. Be careful, it overwrite the previous HTTPContent, if it exists and is different for MultipartFormData
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="fileName"></param>
        void AddMultipartFormDataContent(string key, Stream value, string fileName);
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
}
