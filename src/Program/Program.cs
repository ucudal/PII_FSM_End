//------------------------------------------------------------------------------
// <copyright file="Program.cs" company="Universidad Católica del Uruguay">
//     Copyright (c) Programación II. Derechos reservados.
// </copyright>
//------------------------------------------------------------------------------

using System;

namespace Ucu.Poo.RideShare
{
    /// <summary>
    /// El programa principal.
    /// </summary>
    public static class Program
    {
        /// <summary>
        /// Punto de entrada al programa principal.
        /// </summary>
        public static void Main()
        {
            Passenger passengerDan = new Passenger("Dan", "Aykroyd", "2222222", @"dan.jpg");
            Driver driverBill = new Driver("Bill", "Murray", "1111111", @"bill.jpg", "1959 Cadillac Blanco", "The great GhostBuster and master of time and groundhogs", false);
            PoolDriver poolDriverRick = new PoolDriver("Rick", "Moranis", "333333", @"rick.jpg", "VW Beattle", "Master of shrinkage and Keymaster of Gozer", true, 4);
            UcuRideShare ride = new UcuRideShare();

            bool result = ride.Add(driverBill);
            PrintMessage(driverBill, result);

            driverBill.Picture = @"bill2.jpg";
            result = ride.Add(driverBill);
            PrintMessage(driverBill, result);

            result = ride.Add(passengerDan);
            PrintMessage(passengerDan, result);

            result = ride.Add(poolDriverRick);
            PrintMessage(poolDriverRick, result);
        }

        private static void PrintMessage(User user, bool result)
        {
            if (result)
            {
                Console.WriteLine(user.Name + " " + user.LastName + " fue agregado con éxito!");
            }
            else
            {
                Console.WriteLine("No se pudo agregar al usuario " + user.Name + " " + user.LastName);
            }
        }
    }
}

//------------------------------------------------------------------------------
// <copyright file="Program.cs" company="Universidad Católica del Uruguay">
//     Copyright (c) Programación II. Derechos reservados.
// </copyright>
//------------------------------------------------------------------------------

// using System;

// namespace Ucu.Poo.Cognitive.Demo
// {
//     /// <summary>
//     /// Programa principal.
//     /// </summary>
//     public static class Program
//     {
//         private static CognitiveFace face;

//         /// <summary>
//         /// Punto de entrada al programa principal.
//         /// </summary>
//         public static void Main()
//         {
//             face = new CognitiveFace();
//             Console.WriteLine("Reconociendo...");
//             Recognize("bill.jpg", true);
//             Recognize("dan.jpg", true);
//             Recognize("rick.jpg", true);
//             Console.WriteLine("Reconocimiento terminado");
//         }

//         private static void Recognize(string imageFilePath, bool markFaces = false)
//         {
//             CognitiveFace.RecognitionResult result = face.Recognize(imageFilePath, markFaces);
//             if (result.Success && result.FaceFound && result.GlassesFound)
//             {
//                 Console.WriteLine($"Se reconoció una cara con lentes en '{imageFilePath}' 🥸");
//             }
//             else if (result.Success && result.FaceFound)
//             {
//                 Console.WriteLine($"Se reconoció una cara en '{imageFilePath}' 😀");
//             }
//             else if (result.Success)
//             {
//                 Console.WriteLine($"Se reconoció la imagen pero no hay una cara en '{imageFilePath}'");
//             }
//             else
//             {
//                 Console.WriteLine($"No se pudo reconocer '{imageFilePath}'");
//             }
//         }
//     }
// }
