using CognitiveCoreUCU;

namespace RideShareUCU
{
    public static class CognitiveApiSingleton
    {
        private const string apiKey = "620e818a46524ceb92628cde08068242";
        private static CognitiveFace singleton = null;
        public static CognitiveFace Singleton
        {
            get
            {
                if (singleton is null)
                {
                    singleton = new CognitiveFace(apiKey);
                }
                return singleton;
            }
        } 
    }
}