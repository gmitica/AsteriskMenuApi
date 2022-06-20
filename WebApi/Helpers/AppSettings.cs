namespace WebApi.Helpers
{

    public class AppSettings
    {
        public string Secret { get; set; }

        // refresh token time to live (in days), inactive tokens are
        // automatically deleted from the database after this time
        public int RefreshTokenTTL { get; set; }


        public string EmailFromDisplayName { get; set; }
        public string EmailHost { get; set; }
        public int EmailPort { get; set; }
        public string EmailUser { get; set; }
        public string EmailPassword { get; set; }

        public string URL { get; set; }

        public int TokenMinutesExpire { get; set; }

    }
}