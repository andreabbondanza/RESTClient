using DewInterfaces.DewRestClient;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace DewCore.DewRestClient
{
    /// <summary>
    /// Response object
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
        /// Return response body as stream
        /// </summary>
        /// <returns></returns>
        public async Task<Stream> ReadResponseAsStreamAsync()
        {
            return this.response.Content != null ? await this.response.Content.ReadAsStreamAsync() : null;
        }
        /// <summary>
        /// Return response body as bytearray
        /// </summary>
        /// <returns></returns>
        public async Task<byte[]> ReadResponseAsByteArrayAsync()
        {
            return this.response.Content != null ? await this.response.Content.ReadAsByteArrayAsync() : null;
        }

        /// <summary>
        /// Constructor with args
        /// </summary>
        /// <param name="response">The HttpResponseMessage</param>
        public RESTResponse(HttpResponseMessage response)
        {
            this.response = response;
        }
    }
}
