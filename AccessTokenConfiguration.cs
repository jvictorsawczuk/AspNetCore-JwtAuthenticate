namespace AspNetCore_JwtAuthenticate
{
    public class AccessTokenConfiguration
    {
       public string Audience { get; set; }
        public string Issuer { get; set; }
        public string Secret { get; set; }
        public double Hours { get; set; }
    }
}