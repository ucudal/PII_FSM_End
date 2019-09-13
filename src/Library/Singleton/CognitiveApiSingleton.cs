using CognitiveCoreUCU;

namespace RideShareUCU
{
    public static class CognitiveApiSingleton
    {
        private const string apiKey = "a36648d3c5134ab692acd35605d491f7";
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