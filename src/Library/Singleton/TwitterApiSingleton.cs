using TwitterUCU;

namespace RideShareUCU
{
    public static class TwitterApiSingleton
    {       
        private const string consumerKey = "g7rkPB5uI2xOqELAhlNrorSU4";
        private const string consumerKeySecret = "8hOTyS71GrTH9Ool3rXykAJRY5AmgSPiy78b1wYUPcvfIzXeEc";
        private const string accessTokenSecret = "675fHmUzeaPajtj3pO64w5xd3p9YI3kco7kSvKhzeEvYe";
        private const string accessToken = "1396065818-8vnV9HJFW5ArcfFg2zE9hLA68CZYFXO8Cjv6o2E";
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