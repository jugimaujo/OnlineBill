using System;

namespace OnlineBill.Domain.Models
{
    public class User
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public bool? RememberMe { get; set; }

        public List<string> Validate()
        {
            var list = new List<string>();
            if (string.IsNullOrEmpty(Name))
            {
                list.Add("Name must be passed.");
            }

            if (string.IsNullOrEmpty(Email))
            {
                list.Add("Email must be passed.");
            }

            if (string.IsNullOrEmpty(Password) || Password.Length < 5)
            {
                list.Add("Password must have at least 5 characters.");
            }

            return list;
        }
    }
}
