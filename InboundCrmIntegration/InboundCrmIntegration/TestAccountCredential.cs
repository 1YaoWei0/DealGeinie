using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Description;
using System.Text;
using System.Threading.Tasks;

namespace InboundCrmIntegration
{
    public class TestAccountCredential : ICrmCredential
    {
        public string Name { get; set; }

        public string Password { get; set; }

        public string Domain { get; set; }

        public string Uri { get; set; }

        public TestAccountCredential()
        {
            Name = "willie.Yao";
            Password = "pass@word123";
            Domain = "huamei.com";
            Uri = "https://crmdev.huamei.com/XRMServices/2011/Organization.svc";
        }

        public string UserName()
        {
            return Name + "@" + Domain;
        }

        public ClientCredentials Credentials()
        {
            ClientCredentials clientCredentials = new ClientCredentials();

            clientCredentials.UserName.UserName = UserName();
            clientCredentials.UserName.Password = Password;

            return clientCredentials;
        }
    }
}
