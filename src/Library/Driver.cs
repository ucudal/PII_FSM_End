namespace RideShareUCU
{
    public class Driver : RideUser
    {
        public string Vehiculo {get; set;}
        public string Bio {get; set; }
        public Driver(string name, string lastName, string id, string profilePicPath, string vehiculo, string bio) : base(name, lastName, id, profilePicPath)
        {
            this.Vehiculo = vehiculo;
            this.Bio = bio;
        }
        internal override bool IsValidPicture()
        {
            bool hasFace = base.IsValidPicture();
            return hasFace && CognitiveApiSingleton.Singleton.SmileFound;
        }
        public override string ToString()
        {
            return "Bienvenido "+this.Name+"!\nEl nuevo Conductor de UCURide\nBio: " + this.Bio;
        }
    }
}