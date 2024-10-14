namespace Ucu.Poo.RideShare;

public class Passenger : RideUser
{
    public Passenger(string name, string lastName, string id, string profilePicPath)
        : base(name, lastName, id, profilePicPath)
    {
    }

    public override string ToString()
    {
        return "Bienvenido "+this.Name+" "+this.LastName+"!\n El nuevo usuario de UCURide";
    }
}
