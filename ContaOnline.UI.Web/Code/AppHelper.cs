using System;
using System.Web;
using OnlineBill.Domain.Interfaces;
using Newtonsoft.Json;
using OnlineBill.Domain.Models;

namespace OnlineBill.UI.Web.Code
{
    public interface IAppHelper
    {
        string GetLoggedUser();
        bool IsUserLoggedIn();
    }

    public class AppHelper: IAppHelper
    {
        private readonly IHttpContextAccessor contextAccessor;
        public AppHelper(IHttpContextAccessor accessor)
        {
            contextAccessor = accessor;
        }

        private HttpContext Context { get { return contextAccessor.HttpContext; } }

        public string? GetLoggedUser()
        {
            var loggedUserId = Context.User.Claims.First(prop => prop.Type == "userId")?.Value;

            return loggedUserId ?? null;
        }

        public bool IsUserLoggedIn()
        {
            return GetLoggedUser() != null;
        }
    }
}
