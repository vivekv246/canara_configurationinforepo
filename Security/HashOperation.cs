using System;
using System.Security.Cryptography;
using System.Text;


namespace ConfigurationInfo.Security
{
    public class HashOperation
    {
        //This method use for the hash of request and response body of data
        private static readonly byte[] key = Encoding.ASCII.GetBytes("welcometosecurity");
        public static string ComputeHmac256(string data)
        {
            using (var hmac = new HMACSHA256(key))
            {
                byte[] hashmessage = hmac.ComputeHash(Encoding.ASCII.GetBytes(data));
                return Convert.ToBase64String(hashmessage);
            }
        }

    }
}
