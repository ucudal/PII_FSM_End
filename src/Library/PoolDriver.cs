//------------------------------------------------------------------------------
// <copyright file="PoolDriver.cs" company="Universidad Católica del Uruguay">
//     Copyright (c) Programación II. Derechos reservados.
// </copyright>
//------------------------------------------------------------------------------

namespace Ucu.Poo.RideShare
{
    /// <summary>
    /// Representa a un conductor del tipo pool, que acepta varios pasajeros en
    /// un mismo viaje.
    /// </summary>
    public class PoolDriver : Driver
    {
        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="PoolDriver"/>.
        /// </summary>
        /// <param name="name">Nombre del conductor.</param>
        /// <param name="lastName">Apellido del conductor.</param>
        /// <param name="id">Cédula o identificador del conductor.</param>
        /// <param name="profilePicPath">Ruta de la foto de perfil.</param>
        /// <param name="vehicle">Vehículo del conductor.</param>
        /// <param name="bio">Biografía del conductor.</param>
        /// <param name="needsGlasses">Indica si la foto requiere
        /// lentes.</param>
        /// <param name="capacity">Capacidad máxima de pasajeros del
        /// viaje.</param>
        public PoolDriver(string name, string lastName, string id, string profilePicPath, string vehicle, string bio, bool needsGlasses, int capacity)
            : base(name, lastName, id, profilePicPath, vehicle, bio, needsGlasses)
        {
            this.Capacity = capacity;
        }

        /// <summary>
        /// Obtiene o establece la cantidad máxima de pasajeros que puede
        /// transportar el conductor pool.
        /// </summary>
        public int Capacity { get; set; }

        /// <summary>
        /// Devuelve un mensaje de bienvenida específico para un conductor pool.
        /// </summary>
        /// <returns>El texto del mensaje.</returns>
        public override string GetIntro()
        {
            return
                $"Bienvenido {this.Name}!\n" +
                $"Un nuevo conductor pool de UCURide que llevará hasta {this.Capacity} pasajeros.\n " +
                $"Bio: {this.Bio}";
        }
    }
}
