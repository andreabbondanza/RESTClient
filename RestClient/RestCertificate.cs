using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace RestClient
{
    class RESTCertificate
    {
        private X509Certificate2 certificate;
        private HttpClientHandler handler;
    }
}
