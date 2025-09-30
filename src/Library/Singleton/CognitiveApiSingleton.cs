using Ucu.Poo.Cognitive;

namespace Ucu.Poo.RideShare
{
    public static class CognitiveApiSingleton
    {
        private static CognitiveFace singleton = null;
        public static CognitiveFace Singleton
        {
            get
            {
                if (singleton == null)
                {
                    singleton = new CognitiveFace(true);
                }
                return singleton;
            }
        }
    }
}
