using TwitterUCU;

namespace RideShareUCU
{
    public static class TwitterApiSingleton
    {
        
        private const string consumerKey = "dtOgpyjBBXglAzMEjMMZtFf73";
        private const string consumerKeySecret = "Qzm0FxotJ9YyoXiGLJ4JI9IZFWmYvB4LWpteWPGVYofxSG4FnN";
        private const string accessTokenSecret = "HXtlP1SRnJCL5a37R98hFrIRlEIouZX3Ra4s6JuFOpXZF";
        private const string accessToken = "1396065818-13uONd7FgFVXhW1xhUCQshKgGv4UOnKeDipg4cz";
        private static TwitterImage singleton = null;
        public static TwitterImage Singleton
        {
            get
            {
                if (singleton is null)
                {
                    singleton = new TwitterImage(consumerKey, consumerKeySecret, accessToken, accessTokenSecret);
                }
                return singleton;
            }
        }
    }
}