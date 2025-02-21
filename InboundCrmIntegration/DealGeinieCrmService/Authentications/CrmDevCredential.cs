using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Client;
using System;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.ServiceModel.Description;

namespace DealGeinieCrmService.Authentications
{
    public class CrmDevCredential : ICrmCredential
    {
        private static CrmDevCredential _instance;
        private static readonly object _lock = new object();

        public string AuthToken { get; private set; }

        private static readonly string _name = "willie.Yao";
        private static readonly string _password = "pass@word";
        private static readonly string _domain = "huamei.com";
        private static readonly string _uri = "https://crmdev.huamei.com/XRMServices/2011/Organization.svc";

        public IOrganizationService OrganizationServiceProxy { get; private set; }

        private CrmDevCredential()
        {
            InitializeCredentials();
        }

        public static CrmDevCredential Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_lock)
                    {
                        if (_instance == null)
                        {
                            _instance = new CrmDevCredential();
                        }
                    }
                }
                return _instance;
            }
        }

        private void Authenticate()
        {
            AuthToken = "some_generated_token";
        }

        public void RefreshToken()
        {
            Authenticate();
        }

        public void InitializeCredentials()
        {
            ServicePointManager.ServerCertificateValidationCallback += ValidateCertificate;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            var clientCredentials = new ClientCredentials
            {
                UserName =
                {
                    UserName = $"{_name}@{_domain}",
                    Password = _password
                }
            };

            OrganizationServiceProxy = new OrganizationServiceProxy(new Uri(_uri), null, clientCredentials, null);
        }

        private static bool ValidateCertificate(object sender, X509Certificate cert, X509Chain chain, System.Net.Security.SslPolicyErrors sslPolicyErrors)
        {
            return true;
        }

        public ClientCredentials Credentials()
        {
            throw new NotImplementedException();
        }
    }
}
