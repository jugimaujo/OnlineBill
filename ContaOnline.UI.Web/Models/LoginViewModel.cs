﻿namespace OnlineBill.UI.Web.Models
{
    public class LoginViewModel
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public bool RememberMe { get; set; }
        public bool? NotRememberMe { get; set; }
    }
}
