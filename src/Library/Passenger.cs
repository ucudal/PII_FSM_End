namespace Ucu.Poo.RideShare
{
    /// <summary>
    /// Representa a un pasajero del sistema UCURide. Los pasajeros cuentan con
    /// la misma información base que cualquier usuario y pueden registrarse con
    /// una foto de perfil válida.
    /// </summary>
    public class Passenger : User
    {
        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="Passenger"/>.
        /// </summary>
        /// <param name="name">Nombre del pasajero.</param>
        /// <param name="lastName">Apellido del pasajero.</param>
        /// <param name="id">Cédula o identificador del pasajero.</param>
        /// <param name="profilePicPath">Ruta de la foto de perfil del
        /// pasajero.</param>
        public Passenger(string name, string lastName, string id, string profilePicPath)
            : base(name, lastName, id, profilePicPath)
        {
        }

        /// <summary>
        /// Devuelve un mensaje de bienvenida con los datos del pasajero.
        /// </summary>
        /// <returns>El texto del mensaje.</returns>
        public override string GetIntro()
        {
            return $"Bienvenido {this.Name} {this.LastName}!\n El nuevo usuario de UCURide";
        }
    }
}
