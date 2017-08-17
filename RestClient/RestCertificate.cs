using RestClient.Interfaces;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace DewCore.DewRestClient
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
        }
    }
}
