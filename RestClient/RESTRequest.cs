using DewCore.Abstract.RestClient;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;

namespace DewCore.RestClient
{
    /// <summary>
    /// REST Request class
    /// </summary>
    public class RESTRequest : IRESTRequest
    {
        /// <summary>
        /// Headers
        /// </summary>
        private Dictionary<string, string> _headers = new Dictionary<string, string>();
        /// <summary>
        /// Query args
        /// </summary>
        private Dictionary<string, string> _queryArgs = new Dictionary<string, string>();
        /// <summary>
        /// url
        /// </summary>
        private string _url = "";
        /// <summary>
        /// Content
        /// </summary>
        private HttpContent _content = null;
        /// <summary>
        /// Method
        /// </summary>
        private Method _method = Method.GET;
        /// <summary>
        /// Represent the http message handler used to certificate the request
        /// </summary>
        private RESTCertificate _handler = null;
        /// <summary>
        /// Add the content to the request
        /// </summary>
        /// <param name="content"></param>
        public void AddContent(HttpContent content)
        {
            this._content = content;
        }
        /// <summary>
        /// Add a new MultipartFormDataContent to HTTPContent request. Be careful, it overwrite the previous HTTPContent, if it exists and is different for MultipartFormData
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public void AddMultipartFormDataContent(string key, string value)
        {
            if (this._content == null)
            {
                this._content = new MultipartFormDataContent();
            }
            else
            {
                if (this._content.GetType() != typeof(MultipartFormDataContent))
                {
                    this._content = new MultipartFormDataContent();
                }
            }
            (this._content as MultipartFormDataContent).Add(new StringContent(value), key);
        }
        /// <summary>
        /// Add a new MultipartFormDataContent to HTTPContent request. Be careful, it overwrite the previous HTTPContent, if it exists and is different for MultipartFormData
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="fileName"></param>
        public void AddMultipartFormDataContent(string key, Stream value, string fileName = "default")
        {
            if (this._content == null)
            {
                this._content = new MultipartFormDataContent();
            }
            else
            {
                if (this._content.GetType() != typeof(MultipartFormDataContent))
                {
                    this._content = new MultipartFormDataContent();
                }
            }
            (this._content as MultipartFormDataContent).Add(new StreamContent(value), key, fileName);
        }
        /// <summary>
        /// Add a new MultipartFormDataContent to HTTPContent request. Be careful, it overwrite the previous HTTPContent, if it exists and is different for MultipartFormData
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="fileName"></param>
        public void AddMultipartFormDataContent(string key, byte[] value, string fileName = "default")
        {
            if (this._content == null)
            {
                this._content = new MultipartFormDataContent();
            }
            else
            {
                if (this._content.GetType() != typeof(MultipartFormDataContent))
                {
                    this._content = new MultipartFormDataContent();
                }
            }
            (this._content as MultipartFormDataContent).Add(new ByteArrayContent(value), key, fileName);

        }
        /// <summary>
        /// Add header to the request
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public void AddHeader(string key, string value)
        {
            this._headers.Add(key, value);
        }
        /// <summary>
        /// Add a query arg
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public void AddQueryArgs(string key, string value)
        {
            this._queryArgs.Add(key, value);
        }
        /// <summary>
        /// Set the url
        /// </summary>
        /// <exception cref="UriFormatException"></exception>
        /// <param name="url"></param>
        public void SetUrl(string url)
        {
            if (IsValidUrl(url))
                this._url = url;
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
            return this._method;
        }
        /// <summary>
        /// Set the method
        /// </summary>
        /// <param name="method"></param>
        public void SetMethod(Method method)
        {
            this._method = method;
        }
        /// <summary>
        /// Return the _headers
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, string> GetHeaders()
        {
            return this._headers;
        }
        /// <summary>
        /// Return the request content
        /// </summary>
        /// <returns></returns>
        public HttpContent GetContent()
        {
            return this._content;
        }
        /// <summary>
        /// Get the query args
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, string> GetQueryArgs()
        {
            return this._queryArgs;
        }
        /// <summary>
        /// Return the request url
        /// </summary>
        /// <returns></returns>
        public string GetUrl()
        {
            return this._url;
        }
        /// <summary>
        /// Set the http message handler
        /// </summary>
        /// <param name="handler"></param>
        public void SetHandler(RESTCertificateAbstract handler)
        {
            var myhandler = handler as RESTCertificate;
            this._handler = myhandler;
            if (myhandler.Handler.ClientCertificates.Count == 0)
            {
                foreach (var item in myhandler.Certs)
                {
                    myhandler.Handler.ClientCertificates.Add(item);
                }
            }
        }
        /// <summary>
        /// Return the current http message handler
        /// </summary>
        /// <returns></returns>
        public HttpClientHandler GetHandler()
        {
            return _handler?.Handler;
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
