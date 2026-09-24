//------------------------------------------------------------------------------
// <copyright file="StateMachine.cs" company="Universidad Católica del Uruguay">
//     Copyright (c) Programación II. Derechos reservados.
// </copyright>
//------------------------------------------------------------------------------

namespace Ucu.Poo.Fsm;

/// <summary>
/// Esta clase representa una máquina de estados. Una máquina de estados tiene
/// un número finito de estados y cada estado tiene transiciones que llevan a
/// otros estados dependiendo de los eventos que ocurren en ese estado. La
/// máquina procesa uno o más eventos y cambia de estado según las transiciones.
/// </summary>
public class StateMachine
{
    private List<State> states = new List<State>();

    /// <summary>
    /// Obtiene el estado actual de la máquina de estados. El estado actual
    /// cambia o no según sus transiciones en el método <see
    /// cref="StateMachine.ProcessEvent(Event)"/>.
    /// </summary>
    public State CurrentState { get; private set; }

    /// <summary>
    /// Obtiene la lista de estados agregados a esta máquina de estados con el
    /// método <see cref="StateMachine.AddState"/>.
    /// </summary>
    public IReadOnlyList<State> States
    {
        get { return this.states.AsReadOnly(); }
    }

    /// <summary>
    /// Agrega un estado a la máquina de estados. El primer estado que se agrega
    /// se convierte en el estado incial y se asigna en <see
    /// cref="StateMachine.CurrentState"/>. Los estados son únicos, es decir, no
    /// puede haber dos estados de la misma clase.
    /// </summary>
    /// <param name="state">El estado a agregar.</param>
    public void AddState(State state)
    {
        foreach (State existing in this.states)
        {
            if (existing.GetType() == state.GetType())
            {
                return;
            }
        }

        this.states.Add(state);

        // El primer estado que se agrega queda como estado inicial.
        if (this.CurrentState == null)
        {
            this.CurrentState = state;
        }
    }

    /// <summary>
    /// Procesa un evento enviándolo al estado actual <see
    /// cref="StateMachine.CurrentState"/>. El estado actual cambia o no según
    /// sus transiciones. Si el estado actual cambia a otro estado, se invoca
    /// <see cref="State.OnExit"/> en el estado actual y <see
    /// cref="State.OnEnter"/> en el nuevo estado.
    /// </summary>
    /// <param name="inputEvent">El evento a procesar.</param>
    /// <returns>Retorna <c>true</c> si el evento fue procesado por el estado
    /// actual y la máquina de estados cambió al próximo estado; retorna
    /// <c>false</c> en caso contrario.</returns>
    public bool ProcessEvent(Event inputEvent)
    {
        if (this.CurrentState == null)
        {
            return false;
        }

        State nextState = this.CurrentState.GetNextState(inputEvent);
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
    /// Procesa una secuencia de eventos invocando <see
    /// cref="StateMachine.ProcessEvent(Event)"/> para cada uno de los eventos.
    /// </summary>
    /// <param name="inputEvents">La secuencia de eventos a procesar.</param>
    /// <returns>Retorna <c>true</c> si todos los eventos de la secuencia fueron
    /// procesados; retorna <c>false</c> en caso contrario.</returns>
    public bool ProcessEvents(Event[] inputEvents)
    {
        foreach (Event input in inputEvents)
        {
            if (!this.ProcessEvent(input))
            {
                return false;
            }
        }

        return true;
    }
}
