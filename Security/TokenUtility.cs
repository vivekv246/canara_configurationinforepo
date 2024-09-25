using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using ConfigurationInfo.DatabaseContext;
using ConfigurationInfo.Model;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace ConfigurationInfo.Security
{
    public class TokenUtility
    {



        public static ClaimsPrincipal GetPrincipalFromExpiredToken(string token, string jwgSecretKey)
        {

            var key = Encoding.ASCII.GetBytes(jwgSecretKey);
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = false, //you might want to validate the audience and issuer depending on your use case
                ValidateIssuer = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateLifetime = false //here we are saying that we don't care about the token's expiration date
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken securityToken;
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out securityToken);
            var jwtSecurityToken = securityToken as JwtSecurityToken;
            if (jwtSecurityToken == null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                throw new SecurityTokenException("Invalid token");
            return principal;
        }






        private static string RandomString(int length)
        {
            var random = new Random();
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(x => x[random.Next(x.Length)]).ToArray());
        }


        public static bool isTokenExpired(string token, ApplicationDBContext dbContext, string jwgSecretKey)
        {

            var principal = GetPrincipalFromExpiredToken(token, jwgSecretKey);
            var claimsIdentity = principal.Identity as ClaimsIdentity;
            var UserId = claimsIdentity.FindFirst(ClaimTypes.Sid)?.Value.ToString();
            Console.WriteLine(UserId);
            var expiresTokenTime = GetExpiresTokenTime(UserId, dbContext);
            if (!(expiresTokenTime > DateTime.Now.ToUniversalTime()))
            {
                return true;
            }

            return false;
        }


        private static DateTime GetExpiresTokenTime(string UserId, ApplicationDBContext _context)
        {
            DateTime expiresTokenTime = DateTime.Now; ;
            try
            {


                List<SqlParameter> parms = new List<SqlParameter>
            {
                 new SqlParameter { ParameterName = "@UserID", Value = UserId }

            };


                var result = _context.TokenExpiry.FromSqlRaw(@"exec Spd_GetTokenexpiry @UserID ", parms.ToArray());

                foreach (var row in result)
                {
                    expiresTokenTime = row.TokenExpireTime;
                }


            }
            catch (Exception ex)
            {

            }

            return expiresTokenTime;

        }

        private static DateTime UnixTimeStampToDateTime(long unixTimeStamp)
        {
            var dateTimeVal = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            dateTimeVal = dateTimeVal.AddSeconds(unixTimeStamp).ToUniversalTime();

            return dateTimeVal;
        }



    }

}
