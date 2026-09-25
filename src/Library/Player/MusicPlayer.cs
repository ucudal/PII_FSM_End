//------------------------------------------------------------------------------
// <copyright file="MusicPlayer.cs" company="Universidad Católica del Uruguay">
//     Copyright (c) Programación II. Derechos reservados.
// </copyright>
//------------------------------------------------------------------------------

namespace Ucu.Poo.Fsm
{
    /// <summary>
    /// Esta clase representa un reproductor de música como una máquina de
    /// estados finitos.
    /// </summary>
    public class MusicPlayer : StateMachine
    {
        /// <summary>
        /// Inicializa una nueva instancia de la clase <see
        /// cref="MusicPlayer"/>.
        /// </summary>
        public MusicPlayer()
        {
            InputSymbol play = new Play();
            InputSymbol pause = new Pause();
            InputSymbol stop = new Stop();
            this.AddToAlphabet(new InputSymbol[] { play, pause, stop });

            State stopped = new Stopped();
            State playing = new Playing();
            State paused = new Paused();

            stopped.AddTransition(play, playing);
            playing.AddTransition(pause, paused);
            playing.AddTransition(stop, stopped);
            paused.AddTransition(play, playing);
            paused.AddTransition(stop, stopped);

            this.AddState(stopped);
            this.AddState(playing);
            this.AddState(paused);
        }
    }
}
