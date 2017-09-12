using System;
using System.Security.Cryptography;
using System.Text;

namespace MyWallet.Domain.Shared
{
    public static class PwdHash
    {
        public static string Encode(this string password)
        {
            return Convert.ToBase64String(SHA512.Create()
                .ComputeHash(Encoding.UTF8.GetBytes(password)));
        }
    }
}