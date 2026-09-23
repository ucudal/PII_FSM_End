namespace Ucu.Poo.RideShare
{
    /// <summary>
    /// Representa a un usuario general de UCURide. Tanto los conductores como
    /// los pasajeros comparten estas propiedades y el comportamiento básico
    /// para validar y publicar su imagen. Como los usuarios son o bien
    /// pasajeros o bien conductores, esta clase es abstracta, para que no se
    /// pueda crear instancias; su único propósito es ser heredada.
    /// </summary>
    public abstract class User
    {
        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="User"/>.
        /// </summary>
        /// <param name="name">Nombre del usuario.</param>
        /// <param name="lastName">Apellido del usuario.</param>
        /// <param name="id">Identificador del usuario.</param>
        /// <param name="picture">Ruta de la imagen de perfil.</param>
        protected User(string name, string lastName, string id, string picture)
        {
            this.Name = name;
            this.LastName = lastName;
            this.ID = id;
            this.Stars = 0;
            this.Picture = picture;
        }

        /// <summary>
        /// Obtiene o establece el nombre del usuario.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Obtiene o establece el apellido del usuario.
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Obtiene o establece la cédula o identificador del usuario.
        /// </summary>
        public string ID { get; set; }

        /// <summary>
        /// Obtiene o establece la calificación del usuario.
        /// </summary>
        public double Stars { get; set; }

        /// <summary>
        /// Obtiene o establece la ruta de la foto de perfil del usuario.
        /// </summary>
        public string Picture { get; set; }

        /// <summary>
        /// Valida la imagen del usuario y la publica si cumple con los
        /// requisitos del sistema.
        /// </summary>
        /// <param name="publisher">Publicador que recibe la imagen para
        /// compartirla.</param>
        /// <param name="detector">Detector de caras que valida la foto.</param>
        /// <returns>
        /// <see langword="true"/> si la imagen es válida y se puede publicar;
        /// en caso contrario, <see langword="false"/>.
        /// </returns>
        public virtual bool CanPublishNewUser(
            IPublisher publisher, IFaceDetector detector)
        {
            bool result = this.IsValidPicture(detector);
            if (result)
            {
                publisher.SendImage(this.Picture, this.ToString());
            }

            return result;
        }

        /// <summary>
        /// Devuelve un mensaje de presentación del usuario que depende del
        /// sucesor.
        /// </summary>
        /// <returns>El texto del mensaje.</returns>
        public abstract string GetIntro();

        /// <summary>
        /// Determina si la imagen de perfil del usuario es válida según el tipo
        /// concreto de usuario.
        /// </summary>
        /// <param name="detector">Detector de rostros que se usará para evaluar
        /// la foto.</param>
        /// <returns><see langword="true"/> si la imagen cumple la validación;
        /// de lo contrario, <see langword="false"/>.</returns>
        protected virtual bool IsValidPicture(IFaceDetector detector)
        {
            return detector.HasFace(this.Picture);
        }
    }
}
