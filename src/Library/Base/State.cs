//------------------------------------------------------------------------------
// <copyright file="State.cs" company="Universidad Católica del Uruguay">
//     Copyright (c) Programación II. Derechos reservados.
// </copyright>
//------------------------------------------------------------------------------

using System.Collections.Generic;

namespace Ucu.Poo.Fsm
{
    /// <summary>
    /// Representa un estado en una máquina de estados finitos <see
    /// cref="StateMachine"/>.
    /// </summary>
    public abstract class State
    {
        private List<Transition> transitions = new List<Transition>();

        /// <summary>
        /// Obtiene la lista de transiciones de salida de este estado. Las
        /// transiciones debieron ser oportunamente agregadas con el método <see
        /// cref="State.AddTransition"/>.
        /// </summary>
        public IReadOnlyList<Transition> Transitions
        {
            get { return this.transitions.AsReadOnly(); }
        }

        /// <summary>
        /// Agrega una nueva transición de salida a este estado.
        /// </summary>
        /// <param name="triggerSymbol">La entrada que dispara la
        /// transición.</param>
        /// <param name="nextState">El próximo estado cuando se dispara esta
        /// transición.</param>
        public void AddTransition(
            InputSymbol triggerSymbol,
            State nextState)
        {
            Transition transition = new Transition(triggerSymbol, nextState);
            this.transitions.Add(transition);
        }

        /// <summary>
        /// Determina el estado siguiente dada una entrada si dispara alguna de
        /// las transiciones de este estado. Ver <see
        /// cref="Transition.IsTriggeredBy(InputSymbol)"/>.
        /// </summary>
        /// <param name="input">La entrada a procesar.</param>
        /// <returns>Retorna el estado siguiente de la transición disparada por
        /// la entrada procesado, o <c>null</c> si la entrada no dispara una
        /// transición.</returns>
        public State GetNextState(InputSymbol input)
        {
            State nextState = null;

            foreach (Transition transition in this.transitions)
            {
                if (transition.IsTriggeredBy(input))
                {
                    nextState = transition.NextState;
                }

                if (nextState != null)
                {
                    return nextState;
                }
            }

            return null;
        }

        /// <summary>
        /// Este método es usado por la máquina de estados <see
        /// cref="StateMachine"/> cuando una transición lleva a este estado.
        /// </summary>
        public abstract void OnEnter();

        /// <summary>
        /// Este método es usado por la máquina de estados <see
        /// cref="StateMachine"/> cuando una transición sale de este estado.
        /// </summary>
        public abstract void OnExit();
    }
}