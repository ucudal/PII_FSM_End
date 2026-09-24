//------------------------------------------------------------------------------
// <copyright file="FaceDetector.cs" company="Universidad Católica del Uruguay">
//     Copyright (c) Programación II. Derechos reservados.
// </copyright>
//------------------------------------------------------------------------------

using System;
using Ucu.Poo.Cognitive;

namespace Ucu.Poo.RideShare
{
    /// <summary>
    /// Implementa la validación de fotos de perfil usando el servicio de
    /// reconocimiento facial externo.
    /// </summary>
    public class FaceDetector : IFaceDetector
    {
        /// <summary>
        /// Cliente de reconocimiento facial configurado con la clave de Azure.
        /// </summary>
        private CognitiveFace face;

        /// <summary>
        /// Inicializa una nueva instancia de la clase <see
        /// cref="FaceDetector"/>. Lee la clave de Azure desde la variable de
        /// entorno correspondiente.
        /// </summary>
        public FaceDetector()
        {
            var subscriptionKey = Environment.GetEnvironmentVariable("AZURE_FACE_SUBSCRIPTION_KEY");
            if (string.IsNullOrWhiteSpace(subscriptionKey))
            {
                Console.WriteLine("Falta la variable de entorno 'AZURE_FACE_SUBSCRIPTION_KEY'.");
                return;
            }

            this.face = new CognitiveFace(subscriptionKey);
        }

        /// <summary>
        /// Verifica si una imagen contiene al menos una cara.
        /// </summary>
        /// <param name="imageFilePath">Ruta de la imagen a evaluar.</param>
        /// <returns><see langword="true"/> si se detecta al menos una cara; de
        /// lo contrario, <see langword="false"/>.</returns>
        public bool HasFace(string imageFilePath)
        {
            CognitiveFace.RecognitionResult result = this.face.Recognize(imageFilePath);
            return result.Success && result.FaceFound;
        }

        /// <summary>
        /// Verifica si una imagen contiene una cara con lentes.
        /// </summary>
        /// <param name="imageFilePath">Ruta de la imagen a evaluar.</param>
        /// <returns><see langword="true"/> si la imagen contiene una cara con lentes; de lo contrario, <see langword="false"/>.</returns>
        public bool HasGlasses(string imageFilePath)
        {
            CognitiveFace.RecognitionResult result = this.face.Recognize(imageFilePath);
            return result.Success && result.FaceFound && result.GlassesFound;
        }
    }
}
