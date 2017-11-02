using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using DewCore.Abstract.Internet;

namespace DewCore.Abstract.RestClient
{
    /// <summary>
    /// Rest client library interface
    /// </summary>
    public interface IRESTClient : IInternetClient<IRESTResponse, IRESTRequest>
    {

    }
    /// <summary>
    /// Response interface
    /// </summary>
    public interface IRESTResponse : IInternetResponse
    {

    }
    /// <summary>
    /// Request object interface
    /// </summary>
    public interface IRESTRequest : IInternetRequest
    {
        /// <summary>
        /// Set a http message handler
        /// </summary>
        /// <param name="handler"></param>
        void SetHandler(HttpClientHandler handler);
        /// <summary>
        /// Return the http message handler
        /// </summary>
        /// <returns></returns>
        HttpClientHandler GetHandler();
    }
}
