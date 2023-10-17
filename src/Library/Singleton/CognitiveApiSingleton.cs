using Ucu.Poo.Cognitive;

namespace RideShareUCU
{
    public static class CognitiveApiSingleton
    {
        private static CognitiveFace singleton = null;
        public static CognitiveFace Singleton
        {
            get
            {
                if (singleton is null)
                {
                    singleton = new CognitiveFace(true);
                }
                return singleton;
            }
        }
    }
}