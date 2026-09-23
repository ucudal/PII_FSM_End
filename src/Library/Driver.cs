namespace Ucu.Poo.RideShare
{
    /// <summary>
    /// Representa a un conductor voluntario del servicio UCURide. Un conductor
    /// tiene un vehículo, una bio y puede tener que usar lentes.
    /// </summary>
    public class Driver : User
    {
        /// <summary>
        /// Obtiene o establece el vehículo del conductor.
        /// </summary>
        public string Vehicle { get; set; }

        /// <summary>
        /// Obtiene o establece la biografía breve del conductor.
        /// </summary>
        public string Bio { get; set; }

        /// <summary>
        /// Obtiene o establece un valor que indica si el conductor necesita
        /// usar lentes.
        /// </summary>
        public bool NeedsGlasses { get; set; }

        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="Driver"/>.
        /// </summary>
        /// <param name="name">Nombre del conductor.</param>
        /// <param name="lastName">Apellido del conductor.</param>
        /// <param name="id">Cédula o identificador del conductor.</param>
        /// <param name="picture">Ruta de la foto de perfil.</param>
        /// <param name="vehicle">Vehículo del conductor.</param>
        /// <param name="bio">Biografía breve del conductor.</param>
        /// <param name="needsGlasses">Indica si la foto del conductor debe incluir lentes.</param>
        public Driver(string name, string lastName, string id, string picture,
            string vehicle, string bio, bool needsGlasses)
            : base(name, lastName, id, picture)
        {
            this.Vehicle = vehicle;
            this.Bio = bio;
            this.NeedsGlasses = needsGlasses;
        }

        /// <summary>
        /// Devuelve el mensaje de bienvenida para un nuevo conductor
        /// registrado.
        /// </summary>
        /// <returns>El texto del mensaje.</returns>
        public override string GetIntro()
        {
            return
                $"Bienvenido {this.Name}!\n" +
                $"Un nuevo conductor de UCURide\n " +
                $"Bio: {this.Bio}";
        }

        /// <summary>
        /// Valida la foto del conductor, considerando si requiere lentes.
        /// </summary>
        /// <param name="detector">Detector de rostros utilizado para validar
        /// la imagen.</param>
        /// <returns><see langword="true"/> si la foto cumple con los requisitos
        /// del conductor; de lo contrario, <see langword="false"/>.</returns>
        protected override bool IsValidPicture(IFaceDetector detector)
        {
            if (this.NeedsGlasses)
            {
                return detector.HasGlasses(this.Picture);
            }

            return base.IsValidPicture(detector);
        }
    }
}
