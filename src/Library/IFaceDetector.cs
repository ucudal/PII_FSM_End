namespace Ucu.Poo.RideShare
{
    /// <summary>
    /// Define la capacidad de detección de caras y lentes.
    /// </summary>
    public interface IFaceDetector
    {
        /// <summary>
        /// Determina si una imagen contiene al menos una cara.
        /// </summary>
        /// <param name="imageFilePath">Ruta de la imagen a analizar.</param>
        /// <returns><see langword="true"/> si se detecta una cara; de lo contrario,
        /// <see langword="false"/>.</returns>
        bool HasFace(string imageFilePath);

        /// <summary>
        /// Determina si una imagen contiene una cara con lentes.
        /// </summary>
        /// <param name="imageFilePath">Ruta de la imagen a analizar.</param>
        /// <returns><see langword="true"/> si la imagen contiene cara y lentes; de
        /// lo contrario, <see langword="false"/>.</returns>
        bool HasGlasses(string imageFilePath);
    }
}
