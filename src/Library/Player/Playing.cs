//------------------------------------------------------------------------------
// <copyright file="Playing.cs" company="Universidad Católica del Uruguay">
//     Copyright (c) Programación II. Derechos reservados.
// </copyright>
//------------------------------------------------------------------------------

using System;

namespace Ucu.Poo.Fsm
{
    /// <summary>
    /// Esta clase representa el estado del reproductor de música cuando está
    /// reproduciendo música.
    /// </summary>
    public class Playing : State
    {
        /// <inheritdoc/>
        public override void OnEnter()
        {
            Console.WriteLine("Comenzando a reproducir...");
        }

        /// <inheritdoc/>
        public override void OnExit()
        {
            Console.WriteLine("Deteniendo la reproducción...");
        }
    }
}
