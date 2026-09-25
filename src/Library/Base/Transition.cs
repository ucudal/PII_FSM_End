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
        /// <param name="triggerSymbol">El símbolo de entrada que dispara la
        /// transición.</param>
        /// <param name="nextState">El estado al que se pasa cuando se dispara la
        /// transición.</param>
        public Transition(InputSymbol triggerSymbol, State nextState)
        {
            this.TriggerSymbol = triggerSymbol;
            this.NextState = nextState;
        }

        /// <summary>
        /// Obtiene el símbolo de entrada que dispara la transición.
        /// </summary>
        public InputSymbol TriggerSymbol { get; }

        /// <summary>
        /// Obtiene el estado al que pasa cuando se dispara la transición.
        /// </summary>
        public State NextState { get; }

        /// <summary>
        /// Determina si esta transición se dispara con el símbolo de entrada
        /// que se recibe como argumento.
        /// </summary>
        /// <param name="inputEvent">La entrada a procesar.</param>
        /// <returns>Retorna <c>true</c> si la entrada dispara esta transición;
        /// retorna <c>false</c> en caso contrario.</returns>
        public bool IsTriggeredBy(InputSymbol inputEvent)
        {
            return this.TriggerSymbol.GetType() == inputEvent.GetType();
        }
    }
}
