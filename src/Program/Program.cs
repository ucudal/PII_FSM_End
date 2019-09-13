using System;
using System.Collections;
using RideShareUCU;

namespace PII_Herencia
{
    class Program
    {
        static void PrintMessage(RideUser user, bool result)
        {
            if (result)
            {
                Console.WriteLine(user.Name + " " + user.LastName + " fue agregado con éxito!");
            }
            else
            {
                 Console.WriteLine("No se pudo agregar al usuario "+ user.Name + " " + user.LastName);
            }
        }
        static void Main(string[] args)
        {
            Driver driver1 = new Driver("Bill", "Murray", "1111111", @"bill.jpg" ,"1959 Cadillac Blanco", "The great GhostBuster and master of time and groundhogs");
            Passenger passenger = new Passenger("Dan", "Aykroyd","2222222", @"dan.jpg");
            DriverPool pooldriver1 = new DriverPool("Rick", "Moranis", "333333", @"rick.jpg", "VW Beattle", "Master of shrinkage and Keymaster of Gozer", 4);
            UcuRideShare ride = new UcuRideShare();
            
            bool result = ride.Add(driver1);
            PrintMessage(driver1, result);

            driver1.ProfilePic = @"bill2.jpg";
            result = ride.Add(driver1);
            PrintMessage(driver1, result);
            
            result = ride.Add(passenger);
            PrintMessage(passenger, result);

            result = ride.Add(pooldriver1);
            PrintMessage(pooldriver1, result);
            /*
            En éste método deberas mostrar un ejemplo de funcionamiento de tu programa an pseudocódigo. A continuación te 
            planteamos un ejemplo de como hacerlo. Esto no significa que te limites a hacer solamente esto, debes pensar 
            en grande!

            Usuario pasajero1 = nuevo Pasajero()
            Usuario pasajero2 = nuevo Pasajero()
            Usuario pasajero3 = nuevo Pasajero()
            Usuario conductor1 = nuevo Conductor()
            Usuario conductorPool1 = nuevo ConductorPool(maxPasajeros = 3)
            UcuRideShare rideShare = nuevo UcuRideShare()
            
            rideShare.Add(conductor1)
            Se publica en Twitter un nuevo conductor!

            rideShare.Add(conductorPool1)
            Se publica en Twitter un nuevo conductor!
            
            rideShare.Add(pasajero1)
            Se publica en Twitter nuevo registro de pasajero!
            
            rideShare.Add(pasajero2)
            Se publica en Twitter nuevo registro de pasajero!

            rideShare.Add(pasajero3)
            Se publica en Twitter nuevo registro de pasajero!

            */
        }
    }
}
