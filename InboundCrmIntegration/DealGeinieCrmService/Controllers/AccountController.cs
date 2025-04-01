using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OpenIdConnect;
using System.Web;
using System.Web.Mvc;

namespace DealGeinieCrmService.Controllers
{
    public class AccountController : Controller
    {
        public void Login()
        {
            if (!Request.IsAuthenticated)
            {
                // 触发 Azure AD 登录
                HttpContext.GetOwinContext().Authentication.Challenge(
                    new AuthenticationProperties { RedirectUri = "/" },
                    OpenIdConnectAuthenticationDefaults.AuthenticationType);
            }
        }

        public void Logout()
        {
            // 注销本地 Cookie 和 Azure AD 会话
            HttpContext.GetOwinContext().Authentication.SignOut(
                CookieAuthenticationDefaults.AuthenticationType,
                OpenIdConnectAuthenticationDefaults.AuthenticationType);
        }
    }
}
