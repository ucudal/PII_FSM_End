//------------------------------------------------------------------------------
// <copyright file="Program.cs" company="Universidad Católica del Uruguay">
//     Copyright (c) Programación II. Derechos reservados.
// </copyright>
//------------------------------------------------------------------------------

using Ucu.Poo.Fsm;
using System;

namespace Ucu.Poo.Fsm
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
            StateMachine player = new MusicPlayer();
            Input[] buttons = new Input[] { new Play(), new Pause(), new Play(), new Stop() };
            player.ProcessEvents(buttons);
        }
    }
}
