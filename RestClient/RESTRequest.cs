using DewInterfaces.DewRestClient;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;

namespace DewCore.DewRestClient
{
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
        /// Add a new MultipartFormDataContent to HTTPContent request. Be careful, it overwrite the previous HTTPContent, if it exists and is different for MultipartFormData
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public void AddMultipartFormDataContent(string key, string value)
        {
            if (this.content == null)
            {
                this.content = new MultipartFormDataContent();
            }
            else
            {
                if (this.content.GetType() != typeof(MultipartFormDataContent))
                {
                    this.content = new MultipartFormDataContent();
                }
            }
            (this.content as MultipartFormDataContent).Add(new StringContent(value), key);
        }
        /// <summary>
        /// Add a new MultipartFormDataContent to HTTPContent request. Be careful, it overwrite the previous HTTPContent, if it exists and is different for MultipartFormData
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="fileName"></param>
        public void AddMultipartFormDataContent(string key, Stream value, string fileName = "default")
        {
            if (this.content == null)
            {
                this.content = new MultipartFormDataContent();
            }
            else
            {
                if (this.content.GetType() != typeof(MultipartFormDataContent))
                {
                    this.content = new MultipartFormDataContent();
                }
            }
            (this.content as MultipartFormDataContent).Add(new StreamContent(value), key, fileName);
        }
        /// <summary>
        /// Add a new MultipartFormDataContent to HTTPContent request. Be careful, it overwrite the previous HTTPContent, if it exists and is different for MultipartFormData
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="fileName"></param>
        public void AddMultipartFormDataContent(string key, byte[] value, string fileName = "default")
        {
            if (this.content == null)
            {
                this.content = new MultipartFormDataContent();
            }
            else
            {
                if (this.content.GetType() != typeof(MultipartFormDataContent))
                {
                    this.content = new MultipartFormDataContent();
                }
            }
            (this.content as MultipartFormDataContent).Add(new ByteArrayContent(value), key, fileName);

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
