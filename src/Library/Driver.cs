namespace Ucu.Poo.RideShare;

public class Driver : RideUser
{
    public string Vehiculo {get; set;}
    public string Bio {get; set; }

    public Driver(string name, string lastName, string id, string profilePicPath, string vehiculo, string bio)
        : base(name, lastName, id, profilePicPath)
    {
        this.Vehiculo = vehiculo;
        this.Bio = bio;
    }

    public override string ToString()
    {
        return "Bienvenido "+this.Name+"!\nEl nuevo Conductor de UCURide\nBio: " + this.Bio;
    }
}
