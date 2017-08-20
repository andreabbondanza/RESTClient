using DewCore.Abstract.RestClient;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace DewCore.RestClient
{
    /// <summary>
    /// Certificates for DewRESTClient
    /// </summary>
    public class RESTCertificate : RESTCertificateAbstract
    {
        private List<X509Certificate2> _certs;
        /// <summary>
        /// List of certificates
        /// </summary>
        public List<X509Certificate2> Certs
        {
            get
            {
                return _certs;
            }
            set
            {
                _certs = value;
            }
        }
        private HttpClientHandler _handler;
        /// <summary>
        /// Http client handler
        /// </summary>
        public HttpClientHandler Handler
        {
            get { return _handler; }
            set { _handler = value; }
        }
        /// <summary>
        /// Constructor
        /// </summary>
        public RESTCertificate() { }
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="handler"></param>
        public RESTCertificate(HttpClientHandler handler)
        {
            this._handler = handler;
        }
    }
}
