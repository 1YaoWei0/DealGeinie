using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Description;
using System.Text;
using System.Threading.Tasks;

namespace InboundCrmIntegration
{
    public interface ICrmCredential
    {
        ClientCredentials Credentials();
    }
}
