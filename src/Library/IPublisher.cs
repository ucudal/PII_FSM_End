//------------------------------------------------------------------------------
// <copyright file="IPublisher.cs" company="Universidad Católica del Uruguay">
//     Copyright (c) Programación II. Derechos reservados.
// </copyright>
//------------------------------------------------------------------------------

namespace Ucu.Poo.RideShare
{
    /// <summary>
    /// Define la capacidad de publicar mensajes e imágenes en un canal de comunicación.
    /// </summary>
    public interface IPublisher
    {
        /// <summary>
        /// Envía un mensaje de texto.
        /// </summary>
        /// <param name="text">Texto a publicar.</param>
        void SendMessage(string text);

        /// <summary>
        /// Envía una imagen con un texto descriptivo.
        /// </summary>
        /// <param name="path">Ruta de la imagen a publicar.</param>
        /// <param name="text">Descripción o texto asociado a la imagen.</param>
        void SendImage(string path, string text);
    }
}