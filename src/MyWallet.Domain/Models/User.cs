using System;
using System.Text.RegularExpressions;
using Jose;
using MyWallet.Domain.Models.Core;
using MyWallet.Domain.Shared;

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
            Password = password.Encode();
            Token = token;


        }
        public User(string name, string email, string password) : base(Guid.Empty)
        {
            if (string.IsNullOrEmpty(name)) throw new Exception("name can not to be empty");
            Name = name;
            if (IsValidEmail(email) == false) throw new Exception("Invalid E-mail.");
            Email = email;
            if (string.IsNullOrEmpty(password)) throw new Exception("password can't to be empty");
            Password = password.Encode();
            Token = TokenJwt.Generate(email);
        }

        public User(string email, string password)
        {
            if (IsValidEmail(email) == false) throw new Exception("Invalid E-mail.");
            Email = email;
            if (string.IsNullOrEmpty(password)) throw new Exception("password can't to be empty");
            Password = password.Encode();
        }

        public string Name { get; private set; }
        public string Email { get; private set; }
        public string Password { get; private set; }
        public string Token { get; private set; }

        /// <summary>
        /// Check if the e-mail informed is valid
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        private static bool IsValidEmail(string email) => Regex.IsMatch(email, @"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$", RegexOptions.IgnoreCase);


    }
}