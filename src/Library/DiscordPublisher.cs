using System;
using Ucu.Poo.Discord;

namespace Ucu.Poo.RideShare
{
    /// <summary>
    /// Implementa la publicación de mensajes e imágenes en un canal de Discord
    /// para UCURide.
    /// </summary>
    public class DiscordPublisher : IPublisher
    {
        /// <summary>
        /// Cliente de Discord configurado para enviar mensajes al canal
        /// deseado.
        /// </summary>
        private DiscordClient discordClient;

        /// <summary>
        /// Identificador del canal de Discord donde se publican los avisos.
        /// </summary>
        private ulong channelId;

        /// <summary>
        /// Inicializa una nueva instancia de la clase <see
        /// cref="DiscordPublisher"/>. Obtiene el token del bot y el
        /// identificador del canal desde variables de entorno.
        /// </summary>
        public DiscordPublisher()
        {
            var botToken = Environment.GetEnvironmentVariable("DISCORD_BOT_TOKEN");
            var channelText = Environment.GetEnvironmentVariable("CHANNEL_ID");
            if (string.IsNullOrWhiteSpace(botToken) || string.IsNullOrWhiteSpace(channelText))
            {
                Console.WriteLine("Faltan las variables de entorno 'DISCORD_BOT_TOKEN' o 'CHANNEL_ID'.");
                return;
            }

            this.channelId = ulong.Parse(channelText);
            this.discordClient = new DiscordClient();

            discordClient.Login(botToken);
        }

        /// <summary>
        /// Envía un mensaje de texto al canal configurado.
        /// </summary>
        /// <param name="text">Texto del mensaje a enviar.</param>
        public void SendMessage(string text)
        {
            discordClient.SendMessage(this.channelId, "¡Hola desde C#!");

        }

        /// <summary>
        /// Envía una imagen con un texto asociado al canal configurado.
        /// </summary>
        /// <param name="path">Ruta de la imagen a publicar.</param>
        /// <param name="text">Mensaje asociado a la imagen.</param>
        public void SendImage(string path, string text)
        {
            discordClient.SendImage(this.channelId, path, text);
        }
    }
}
