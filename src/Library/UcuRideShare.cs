using System.Collections.Generic;

namespace Ucu.Poo.RideShare
{
    public class UcuRideShare
    {
        List<RideUser> userList;

        public UcuRideShare()
        {
            this.userList = new List<RideUser>();
        }

        public bool Add(RideUser user)
        {
            if (!this.userList.Contains(user))
            {
                bool result = user.PublishNewUser();
                if (result)
                {
                    this.userList.Add(user);
                }
                return result;
            }
            else
            {
                return false;
            }
        }
    }
}
