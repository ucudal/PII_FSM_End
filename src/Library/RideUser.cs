
using System;

namespace RideShareUCU
{
    public abstract class RideUser
    {
        public string Name {get;set;}
        public string LastName {get;set;}
        public string ID{get;set;}
        public double Stars{get; private set;}
        public string ProfilePic{get; set;}
        public RideUser(string name, string lastName, string id, string profilePicPath)
        {
            this.Name = name;
            this.LastName = lastName;
            this.ID = id;
            this.Stars = 0;   
            this.ProfilePic = profilePicPath;
        }
        public virtual bool PublishNewUser()
        {
            if (IsValidPicture())
            {
                string result = TwitterApiSingleton.Singleton.PublishToTwitter(this.ToString(), this.ProfilePic);
                return result == "OK";
            }
            else
            {
                return false;
            }
        }
        internal virtual bool IsValidPicture()
        {
            CognitiveApiSingleton.Singleton.Recognize(this.ProfilePic);
            return CognitiveApiSingleton.Singleton.FaceFound;
        }

    }
}