using System;
using System.Web;
using OnlineBill.Domain.Interfaces;
using Newtonsoft.Json;
using OnlineBill.Domain.Models;

namespace OnlineBill.UI.Web.Code
{
    public interface IAppHelper
    {
        void RegisterUser(User user);
        User GetLoggedUser();
        bool IsUserLoggedIn();
    }

    public class AppHelper: IAppHelper
    {
        private readonly IHttpContextAccessor contextAccessor;
        private User? _userContext;
        public AppHelper(IHttpContextAccessor accessor)
        {
            contextAccessor = accessor;
        }

        private HttpContext Context { get { return contextAccessor.HttpContext; } }

        public void RegisterUser(User user) 
        {
            string userJson = JsonConvert.SerializeObject(user);

            Context.Session.SetString("user", userJson);
        }

        public User GetLoggedUser()
        {
            string userJson = Context.Session.GetString("user");

            if (userJson != null)
            {
                _userContext = JsonConvert.DeserializeObject<User>(userJson);

                return _userContext;
            }

            return null;
        }

        public bool IsUserLoggedIn()
        {
            return GetLoggedUser() != null;
        }
    }
}
