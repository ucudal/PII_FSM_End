//------------------------------------------------------------------------------
// <copyright file="State.cs" company="Universidad Católica del Uruguay">
//     Copyright (c) Programación II. Derechos reservados.
// </copyright>
//------------------------------------------------------------------------------

namespace Ucu.Poo.Fsm;

/// <summary>
/// Representa un estado en la máquina de estados.
/// </summary>
public class State
{
    private List<Transition> transitions = new List<Transition>();

    /// <summary>
    /// Inicializa una nueva instancia de la clase <see cref="State"/>.
    /// </summary>
    /// <param name="name">El nombre del estado.</param>
    public State(string name)
    {
        this.Name = name;
    }

    /// <summary>
    /// Obtiene el nombre del estado. El nombre del estado no es utilizado en el
    /// procesamiento de los eventos.
    /// </summary>
    public string Name { get;  }

    /// <summary>
    /// Obtiene la lista de transiciones de salida de este estado agregadas con
    /// el método <see cref="State.AddTransition"/>.
    /// </summary>
    public IReadOnlyList<Transition> Transitions
    {
        get { return this.transitions.AsReadOnly(); }
    }

    /// <summary>
    /// Agrega una nueva transición de salida a este estado.
    /// </summary>
    /// <remarks>
    /// En caso de que se haya agregado una transición para el evento
    /// <see cref="Event.Else"/>, la nueva transición se agrega
    /// inmediatamente antes, para que la transición Event.Else sea siempre la
    /// última.
    /// </remarks>>
    /// <param name="triggerEvent">El evento que dispara la transición.</param>
    /// <param name="nextState">El próximo estado cuando se dispara esta
    /// transición.</param>
    /// <param name="onTransition">La acción que se ejecuta cuando se dispara
    /// esta transición.</param>
    /// <returns>La transición agregada.</returns>
    public Transition AddTransition(
        Event triggerEvent,
        State nextState,
        Action onTransition = null)
    {
        bool HasTransitions()
        {
            return this.transitions.Count > 0;
        }

        bool LastTransitionIsElse()
        {
            return this.transitions.Last().TriggerEvent == Event.Else;
        }

        Transition transition = new Transition(
            triggerEvent,
            nextState,
            onTransition);

        // La última transición tiene que ser "Else"
        if (HasTransitions() && LastTransitionIsElse())
        {
            this.transitions.Insert(this.transitions.Count - 1, transition);
        }
        else
        {
            this.transitions.Add(transition);
        }

        return transition;
    }

    /// <summary>
    /// Procesa un evento si dispara alguna de las transiciones de este estado.
    /// Ver <see cref="Transition.ProcessEvent"/>.
    /// </summary>
    /// <param name="event">El evento a procesar.</param>
    /// <returns>Retorna el estado siguiente de la transición disparada por el
    /// evento procesado; retorna null si este evento no dispara una
    /// transición.</returns>
    public State ProcessEvent(Event @event)
    {
        State nextState = null;

        foreach (Transition edge in this.transitions)
        {
            nextState = edge.ProcessEvent(@event);
            if (nextState != null)
            {
                return nextState;
            }
        }

        return null;
    }
}
