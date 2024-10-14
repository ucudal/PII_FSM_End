using Ucu.Poo.RideShare;

namespace Ucu.Poo.RideShare;

public class DriverPool : Driver
{
    public int MaxPassangers{get;set;}
    
    public DriverPool(string name, string lastName, string id, string profilePicPath, string vehiculo, string bio, int maxPassangers) 
        : base(name, lastName, id, profilePicPath, vehiculo, bio)
    {
        this.MaxPassangers = maxPassangers;
    }
    
    public override string ToString()
    {
        return "Bienvenido "+this.Name+"!\nEl nuevo Conductor Pool de UCURide que llevará hasta "+
        this.MaxPassangers+" pasajeros\nBio: " + this.Bio;
    }
}
