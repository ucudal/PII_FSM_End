//------------------------------------------------------------------------------
// <copyright file="State.cs" company="Universidad Católica del Uruguay">
//     Copyright (c) Programación II. Derechos reservados.
// </copyright>
//------------------------------------------------------------------------------

namespace Ucu.Poo.Fsm;

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
    /// <param name="triggerEvent">El evento que dispara la transición.</param>
    /// <param name="nextState">El próximo estado cuando se dispara esta
    /// transición.</param>
    public void AddTransition(
        Event triggerEvent,
        State nextState)
    {
        Transition transition = new Transition(triggerEvent, nextState);
        this.transitions.Add(transition);
    }

    /// <summary>
    /// Determina el estado siguiente dado un evento si dispara alguna de las
    /// transiciones de este estado. Ver <see
    /// cref="Transition.IsTriggeredBy(Event)"/>.
    /// </summary>
    /// <param name="inputEvent">El evento a procesar.</param>
    /// <returns>Retorna el estado siguiente de la transición disparada por el
    /// evento procesado, o <c>null</c> si el evento no dispara una
    /// transición.</returns>
    public State GetNextState(Event inputEvent)
    {
        State nextState = null;

        foreach (Transition edge in this.transitions)
        {
            if (edge.IsTriggeredBy(inputEvent))
            {
                nextState = edge.NextState;
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
