using System.Collections.Generic;

namespace Ucu.Poo.RideShare
{
    /// <summary>
    /// Gestiona la lista de usuarios registrados en UCURide y publica los
    /// nuevos registros usando los servicios configurados.
    /// </summary>
    public class UcuRideShare
    {
        /// <summary>
        /// Lista interna de usuarios registrados en el sistema.
        /// </summary>
        List<User> userList;

        /// <summary>
        /// Publicador utilizado para enviar mensajes e imágenes a Discord.
        /// </summary>
        private IPublisher publisher = new DiscordPublisher();

        /// <summary>
        /// Detector de rostros usado para validar fotos de perfil.
        /// </summary>
        private IFaceDetector detector = new FaceDetector();

        /// <summary>
        /// Inicializa una nueva instancia de la clase <see
        /// cref="UcuRideShare"/>.
        /// </summary>
        public UcuRideShare()
        {
            this.userList = new List<User>();
        }

        /// <summary>
        /// Agrega un usuario al sistema si aún no está registrado y si su foto
        /// cumple las validaciones.
        /// </summary>
        /// <param name="user">Usuario que se desea incorporar.</param>
        /// <returns><see langword="true"/> si el usuario fue agregado
        /// correctamente; de lo contrario, <see langword="false"/>.</returns>
        public bool Add(User user)
        {
            if (!this.userList.Contains(user))
            {
                bool result = user.CanPublishNewUser(this.publisher, this.detector);
                if (result)
                {
                    this.userList.Add(user);
                }

                return result;
            }

            return false;
        }
    }
}
