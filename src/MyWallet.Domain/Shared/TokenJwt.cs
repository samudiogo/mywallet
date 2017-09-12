using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using Jose;
namespace MyWallet.Domain.Shared
{
    public class TokenJwt
    {
        private static readonly byte[] SecretKey = 
            SHA512.Create().ComputeHash(Encoding.UTF8.GetBytes(@"E7CEC5F400A1ED277FD3DE9C05FB2B271EF9FDF80B8856F69EB2388A37BC6EC5EE7D03960CA65F12F6194D9298A3E2433131F3423639DBF42D0C0FE08DFAC49D"));

        public static string Generate(object obj) =>
            JWT.Encode(BuildPayload(obj), SecretKey, JwsAlgorithm.HS512);

        private static Dictionary<string,object> BuildPayload(object obj)
        {
            return new Dictionary<string, object>
            {
                {"sub", obj },
                {"exp", DateTime.Now.AddMinutes(30) }
            };
        }
    }
}