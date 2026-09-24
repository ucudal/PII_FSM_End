//------------------------------------------------------------------------------
// <copyright file="Transition.cs" company="Universidad Católica del Uruguay">
//     Copyright (c) Programación II. Derechos reservados.
// </copyright>
//------------------------------------------------------------------------------

namespace Ucu.Poo.Fsm
{
    /// <summary>
    /// Esta clase representa una transición a un estado en una máquina de estados
    /// finitos <see cref="StateMachine"/>.
    /// </summary>
    public class Transition
    {
        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="Transition"/>.
        /// </summary>
        /// <param name="triggerInput">La entrada que dispara la
        /// transición.</param>
        /// <param name="nextState">El estado al que se pasa cuando se dispara la
        /// transición.</param>
        public Transition(Input triggerInput, State nextState)
        {
            this.TriggerInput = triggerInput;
            this.NextState = nextState;
        }

        /// <summary>
        /// Obtiene la entrada que dispara la transición.
        /// </summary>
        public Input TriggerInput { get; }

        /// <summary>
        /// Obtiene el estado al que se pasa cuando se dispara la transición.
        /// </summary>
        public State NextState { get; }

        /// <summary>
        /// Determina si esta transición se dispara con la entrada que se recibe
        /// como argumento.
        /// </summary>
        /// <param name="inputEvent">La entrada a procesar.</param>
        /// <returns>Retorna <c>true</c> si la entrada dispara esta transición;
        /// retorna <c>false</c> en caso contrario.</returns>
        public bool IsTriggeredBy(Input inputEvent)
        {
            return this.TriggerInput.GetType() == inputEvent.GetType();
        }
    }
}