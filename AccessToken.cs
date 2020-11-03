using System;

namespace AspNetCore_JwtAuthenticate
{
    public class AccessToken
    {
        public string Token { get; set; }

        public DateTime CreateIn {get;set;}

        public DateTime ExpiresIn { get; set; }
    }
}