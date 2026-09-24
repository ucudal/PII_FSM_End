//------------------------------------------------------------------------------
// <copyright file="Transition.cs" company="Universidad Católica del Uruguay">
//     Copyright (c) Programación II. Derechos reservados.
// </copyright>
//------------------------------------------------------------------------------

namespace Ucu.Poo.Fsm;

/// <summary>
/// Esta clase representa una transición a un estado en una máquina de estados
/// finitos <see cref="StateMachine"/>.
/// </summary>
public class Transition
{
    /// <summary>
    /// Inicializa una nueva instancia de la clase <see cref="Transition"/>.
    /// </summary>
    /// <param name="triggerEvent">El evento que dispara la transición.</param>
    /// <param name="nextState">El estado al que se pasa cuando se dispara la
    /// transición.</param>
    public Transition(Event triggerEvent, State nextState)
    {
        this.TriggerEvent = triggerEvent;
        this.NextState = nextState;
    }

    /// <summary>
    /// Obtiene el evento que dispara la transición.
    /// </summary>
    public Event TriggerEvent { get; }

    /// <summary>
    /// Obtiene el estado al que se pasa cuando se dispara la transición.
    /// </summary>
    public State NextState { get; }

    /// <summary>
    /// Determina si esta transición se dispara con el evento que se recibe como
    /// argumento.
    /// </summary>
    /// <param name="inputEvent">El evento a procesar.</param>
    /// <returns>Retorna <c>true</c> si el evento dispara esta transición;
    /// retorna <c>false</c> en caso contrario.</returns>
    public bool IsTriggeredBy(Event inputEvent)
    {
        return this.TriggerEvent.GetType() == inputEvent.GetType();
    }
}
