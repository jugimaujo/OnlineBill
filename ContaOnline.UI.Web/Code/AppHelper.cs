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
        bool IsDeletable(BaseDomain model);
    }

    public class AppHelper: IAppHelper
    {
        private readonly IHttpContextAccessor contextAccessor;
        private readonly IBillRepository _billRepository;

        public AppHelper(IHttpContextAccessor accessor, IBillRepository billRepository)
        {
            contextAccessor = accessor;
            _billRepository = billRepository;
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
        
        public bool IsDeletable(BaseDomain model)
        {
            var billList = _billRepository.GetAll(model.UserId);

            return !billList.Where(bill => bill.CheckingAccountId == model.Id ||
                                           bill.CategoryId == model.Id ||
                                           bill.ContactId == model.Id
                                           ).Any();
        }
    }
}
