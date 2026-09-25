//------------------------------------------------------------------------------
// <copyright file="StateMachine.cs" company="Universidad Católica del Uruguay">
//     Copyright (c) Programación II. Derechos reservados.
// </copyright>
//------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;

namespace Ucu.Poo.Fsm
{
    /// <summary>
    /// Esta clase representa una máquina de estados. Una máquina de estados
    /// tiene un número finito de estados y cada estado tiene transiciones que
    /// llevan a otros estados dependiendo de las entradas que la máquina recibe
    /// mientras está en ese estado. La máquina procesa una o más entradas y
    /// cambia de estado según las transiciones.
    /// </summary>
    public class StateMachine
    {
        private IDictionary<Type, InputSymbol> alphabet = new Dictionary<Type, InputSymbol>();

        private IDictionary<Type, State> states = new Dictionary<Type, State>();

        /// <summary>
        /// Representa los posibles resultados de <see
        /// cref="StateMachine.AddState(State)"/>.
        /// </summary>
        public enum AddStateResult
        {
            /// <summary>
            /// El estado fue agregado.
            /// </summary>
            Success,

            /// <summary>
            /// Un estado del mismo tipo ya existe.
            /// </summary>
            TypeAlreadyExists,

            /// <summary>
            /// El símbolo que dispara una de las transiciones del estado no fue
            /// previamente agregado con <see
            /// cref="StateMachine.AddToAlphabet(InputSymbol)"/>.
            /// </summary>
            TransitionTriggerTypeNotInAlphabet,
        }

        /// <summary>
        /// Representa los posibles resultados de <see
        /// cref="StateMachine.AddToAlphabet(InputSymbol)"/>.
        /// </summary>
        public enum AddToAlphabetResult
        {
            /// <summary>
            /// El símbolo fue agregado.
            /// </summary>
            Success,

            /// <summary>
            /// Un símbolo del mismo tipo fue agregado previamente.
            /// </summary>
            TypeAlreadyExists,
        }

        /// <summary>
        /// Obtiene el estado actual de la máquina de estados. El estado actual
        /// cambia o no según sus transiciones en el método <see
        /// cref="StateMachine.ProcessEvent(InputSymbol)"/>.
        /// </summary>
        public State CurrentState { get; private set; }

        /// <summary>
        /// Obtiene los símbolos del alfabeto válidos para esta máquina de
        /// estados. Los símbolos se agregan con <see
        /// cref="StateMachine.AddToAlphabet(InputSymbol)"/>.
        /// </summary>
        public IReadOnlyCollection<InputSymbol> Alphabet
        {
            get
            {
                return this.alphabet.Values.ToList<InputSymbol>().AsReadOnly();
            }
        }

        /// <summary>
        /// Obtiene la lista de estados de esta máquina de estados. Los estados
        /// se agregan con el método <see cref="StateMachine.AddState"/>.
        /// </summary>
        public IReadOnlyCollection<State> States
        {
            get { return this.states.Values.ToList<State>().AsReadOnly(); }
        }

        /// <summary>
        /// Agrega un símbolo al alfabeto. No puede haber más de un símbolo del
        /// mismo tipo, es decir, no se puede agregar más de una instancia de la
        /// misma clase sucesora de <see cref="InputSymbol"/>. Varios símbolos
        /// pueden ser agregados con <see
        /// cref="StateMachine.AddToAlphabet(InputSymbol[])"/>.
        /// </summary>
        /// <param name="symbol">El símbolo a agregar.</param>
        /// <returns>Un valor <see cref="AddToAlphabetResult"/> que indica si el
        /// símbolo fue agregado o no.</returns>
        public AddToAlphabetResult AddToAlphabet(InputSymbol symbol)
        {
            if (this.alphabet.ContainsKey(symbol.GetType()))
            {
                return AddToAlphabetResult.TypeAlreadyExists;
            }

            this.alphabet.Add(symbol.GetType(), symbol);
            return AddToAlphabetResult.Success;
        }

        /// <summary>
        /// Agrega al mismo tiempo varios símbolos al alfabeto. No puede haber
        /// más de un símbolo del mismo tipo, es decir, no se puede agregar más
        /// de una instancia de la misma clase sucesora de <see
        /// cref="InputSymbol"/>. Los símbolos también pueden ser agregados de a
        /// uno con <see cref="StateMachine.AddToAlphabet(InputSymbol)"/>.
        /// </summary>
        /// <param name="symbols">El conjunto de símbolos a agregar.</param>
        /// <returns>Un valor <see cref="AddToAlphabetResult.Success"/> que
        /// indica que todos los símbolos fueron agregado, o <see
        /// cref="AddToAlphabetResult.TypeAlreadyExists"/> si al menos uno de
        /// ellos no lo fue.</returns>
        public AddToAlphabetResult AddToAlphabet(InputSymbol[] symbols)
        {
            AddToAlphabetResult result = AddToAlphabetResult.Success;
            foreach (InputSymbol symbol in symbols)
            {
                if (this.AddToAlphabet(symbol) == AddToAlphabetResult.TypeAlreadyExists)
                {
                    result = AddToAlphabetResult.TypeAlreadyExists;
                }
            }

            return result;
        }

        /// <summary>
        /// Agrega un estado a la máquina de estados. El primer estado que se
        /// agrega se convierte en el estado inicial y se asigna en <see
        /// cref="StateMachine.CurrentState"/>. No puede haber más de un estado
        /// del mismo tipo, es decir, no se puede agregar más de una instancia
        /// de la misma clase sucesora de <see cref="State"/>. Todos los
        /// símbolos que disparan las transiciones en <see
        /// cref="State.Transitions"/> deben haber sido previamente agregados al
        /// alfabeto.
        /// </summary>
        /// <param name="state">El estado a agregar.</param>
        /// <returns>Retorna un valor <see cref="AddStateResult"/> que
        /// indica si el estado fue agregado, o por qué razón no fue agregado.
        /// </returns>
        public AddStateResult AddState(State state)
        {
            if (this.states.ContainsKey(state.GetType()))
            {
                return AddStateResult.TypeAlreadyExists;
            }

            // Todos los símbolos que disparan las transiciones deben estar en
            // el alfabeto.
            foreach (Transition transition in state.Transitions)
            {
                if (!this.alphabet.ContainsKey(transition.TriggerSymbol.GetType()))
                {
                    return AddStateResult.TransitionTriggerTypeNotInAlphabet;
                }
            }

            this.states.Add(state.GetType(), state);

            // El primer estado que se agrega queda como estado inicial.
            if (this.CurrentState == null)
            {
                this.CurrentState = state;
            }

            return AddStateResult.Success;
        }

        /// <summary>
        /// Procesa una entrada enviándola al estado actual <see
        /// cref="StateMachine.CurrentState"/>. El estado actual cambia o no
        /// según sus transiciones. Si el estado actual cambia a otro estado, se
        /// invoca <see cref="State.OnExit"/> en el estado actual y <see
        /// cref="State.OnEnter"/> en el nuevo estado.
        /// </summary>
        /// <param name="input">La entrada a procesar.</param>
        /// <returns>Retorna <c>true</c> si la entrada fue procesada por el
        /// estado actual y la máquina de estados cambió al próximo estado;
        /// retorna <c>false</c> en caso contrario.</returns>
        public bool ProcessEvent(InputSymbol input)
        {
            if (this.CurrentState == null)
            {
                return false;
            }

            State nextState = this.CurrentState.GetNextState(input);
            if (nextState == null)
            {
                return false;
            }

            this.CurrentState.OnExit();
            this.CurrentState = nextState;
            this.CurrentState.OnEnter();

            return true;
        }

        /// <summary>
        /// Procesa una secuencia de entradas invocando <see
        /// cref="StateMachine.ProcessEvent(InputSymbol)"/> para cada una de ellas.
        /// </summary>
        /// <param name="inputEvents">La secuencia de entradas a procesar.</param>
        /// <returns>Retorna <c>true</c> si todas las entradas de la secuencia
        /// fueron procesados; retorna <c>false</c> en caso contrario.</returns>
        public bool ProcessEvents(InputSymbol[] inputEvents)
        {
            foreach (InputSymbol input in inputEvents)
            {
                if (!this.ProcessEvent(input))
                {
                    return false;
                }
            }

            return true;
        }
    }
}
