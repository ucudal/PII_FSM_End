//------------------------------------------------------------------------------
// <copyright file="Paused.cs" company="Universidad Católica del Uruguay">
//     Copyright (c) Programación II. Derechos reservados.
// </copyright>
//------------------------------------------------------------------------------

using System;

namespace Ucu.Poo.Fsm
{
    /// <summary>
    /// Esta clase representa el estado del reproductor de música cuando está en
    /// pausa.
    /// </summary>
    public class Paused : State
    {
        /// <inheritdoc/>
        public override void OnEnter()
        {
            Console.WriteLine("Entrando en pausa...");
        }

        /// <inheritdoc/>
        public override void OnExit()
        {
            Console.WriteLine("Saliendo de pausa...");
        }
    }
}
