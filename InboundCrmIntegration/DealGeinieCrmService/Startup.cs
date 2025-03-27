using Microsoft.IdentityModel.Protocols;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Owin;
using Microsoft.Owin.Security.Jwt;
using Owin;
using System;
using System.Web.Http;

[assembly: OwinStartup(typeof(DealGeinieCrmService.Startup))]
namespace DealGeinieCrmService
{
    public class Startup
    {
        private string clientId = "1993d3b0-6ce9-4391-a39d-5ee4c14dc16d";
        private string tenantId = "109199d4-f7c0-4027-bcd7-9937e7fb177b";

        public void Configuration(IAppBuilder app)
        {            
            app.UseJwtBearerAuthentication(new JwtBearerAuthenticationOptions
            {
                TokenValidationParameters = new TokenValidationParameters
                {
                    ValidIssuer = $"https://sts.windows.net/{tenantId}/",
                    ValidAudience = "api://" + clientId,
                    IssuerSigningKeyResolver = (token, securityToken, kid, validationParameters) =>
                    {
                        var metadataAddress = $"https://login.microsoftonline.com/{tenantId}/.well-known/openid-configuration";
                        var metadata = new ConfigurationManager<OpenIdConnectConfiguration>(metadataAddress, new OpenIdConnectConfigurationRetriever()).GetConfigurationAsync().Result;
                        return metadata.SigningKeys;
                    },
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.FromMinutes(5)
                }
            });

            // Initialize the OAuth server provider         
            HttpConfiguration config = new HttpConfiguration();            
            WebApiConfig.Register(config);
            app.UseWebApi(config);
        }
    }
}