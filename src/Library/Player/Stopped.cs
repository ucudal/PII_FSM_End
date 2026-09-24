//------------------------------------------------------------------------------
// <copyright file="Stopped.cs" company="Universidad Católica del Uruguay">
//     Copyright (c) Programación II. Derechos reservados.
// </copyright>
//------------------------------------------------------------------------------

using System;

namespace Ucu.Poo.Fsm
{
    /// <summary>
    /// Esta clase representa el estado del reproductor de música cuando está
    /// detenido.
    /// </summary>
    public class Stopped : State
    {
        /// <inheritdoc/>
        public override void OnEnter()
        {
            Console.WriteLine("Entrando a detenido...");
        }

        /// <inheritdoc/>
        public override void OnExit()
        {
            Console.WriteLine("Saliendo de detenido...");
        }
    }
}
