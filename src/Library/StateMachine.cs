//------------------------------------------------------------------------------
// <copyright file="StateMachine.cs" company="Universidad Católica del Uruguay">
//     Copyright (c) Programación II. Derechos reservados.
// </copyright>
//------------------------------------------------------------------------------

namespace Ucu.Poo.Fsm;

/// <summary>
/// Representa una máquina de estados. Una máquina de estados es un modelo
/// conceptual que describe sistemas con un número finito de estados y
/// transiciones que representan su comportamiento y la lógica.
/// </summary>
public class StateMachine
{
    private List<State> states = new List<State>();

    /// <summary>
    /// Obtiene o establece el estado actual de la máquina de estados.
    /// </summary>
    public State CurrentState { get; set;  }

    /// <summary>
    /// Obtiene la lista de estados agregados a esta máquina de estados con el
    /// método <see cref="StateMachine.AddState"/>.
    /// </summary>
    public IReadOnlyList<State> States
    {
        get { return this.states.AsReadOnly(); }
    }

    /// <summary>
    /// Agrega un estado a la máquina de estados.
    /// </summary>
    /// <param name="name">El nombre del estado.</param>
    /// <returns>El estado agregado.</returns>
    public State AddState(string name)
    {
        State state = new State(name);
        this.states.Add(state);

        // El primer estado que se agrega queda como estado inicial.
        if (this.CurrentState == null)
        {
            this.CurrentState = state;
        }

        return state;
    }

    /// <summary>
    /// Procesa un evento enviándolo al estado actual
    /// <see cref="StateMachine.CurrentState"/>.
    /// </summary>
    /// <param name="event">El evento a procesar.</param>
    /// <returns><c>true</c> si el evento fue procesado por el estado actual y
    /// la máquina de estados cambió al próximo estado; retorna <c>false</c> en
    /// caso contrario.</returns>
    public bool ProcessEvent(Event @event)
    {
        if (this.CurrentState == null)
        {
            return false;
        }

        State nextState = this.CurrentState.ProcessEvent(@event);
        if (nextState == null)
        {
            return false;
        }

        this.CurrentState = nextState;
        return true;
    }

    /// <summary>
    /// Procesa una secuencia de eventos.
    /// </summary>
    /// <param name="events">La secuencia de eventos.</param>
    /// <returns><c>true</c> si todos los eventos de la secuencia fueron
    /// procesados; retorna <c>false</c> en caso contrario.</returns>
    public bool ProcessEvent(Event[] events)
    {
        foreach (Event @event in events)
        {
            if (!this.ProcessEvent(@event))
            {
                return false;
            }
        }

        return true;
    }
}
