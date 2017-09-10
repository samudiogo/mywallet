using System;
using System.Text.RegularExpressions;
using MyWallet.Domain.Models.Core;

namespace MyWallet.Domain.Models
{
    public class User : Entity
    {
        protected User() { }

        public User(Guid id, string name, string email, string password, string token) : base(id)
        {
            if (string.IsNullOrEmpty(name)) throw new Exception("name can not to be empty");
            Name = name;
            if (IsValidEmail(email) == false) throw new Exception("Invalid E-mail.");
            Email = email;
            if (string.IsNullOrEmpty(password)) throw new Exception("password can't to be empty");
            Password = password;
            Token = token;


        }

        public string Name { get; }
        public string Email { get; }
        public string Password { get; }
        public string Token { get; }

        /// <summary>
        /// Check if the e-mail informed is valid
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        private static bool IsValidEmail(string email) => Regex.IsMatch(email, @"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$", RegexOptions.IgnoreCase);


    }
}