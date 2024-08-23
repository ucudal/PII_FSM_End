//------------------------------------------------------------------------------
// <copyright file="Transition.cs" company="Universidad Católica del Uruguay">
//     Copyright (c) Programación II. Derechos reservados.
// </copyright>
//------------------------------------------------------------------------------

namespace Ucu.Poo.Fsm;

/// <summary>
/// Esta clase representa una transición a un estado en una máquina de estado
/// finito.
/// </summary>
public class Transition
{
    /// <summary>
    /// Inicializa una nueva instancia de la clase <see cref="Transition"/>.
    /// </summary>
    /// <param name="triggerEvent">El evento que dispara la transición.</param>
    /// <param name="nextState">El estado al que se pasa cuando se dispara la
    /// transición.</param>
    /// <param name="onTransition">La acción que se ejecuta cuando se dispara
    /// la transición.</param>
    public Transition(Event triggerEvent, State nextState, Action onTransition = null)
    {
        this.TriggerEvent = triggerEvent;
        this.NextState = nextState;
        this.OnTransition = onTransition;
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
    /// Obtiene la acción que se ejecuta cuando se dispara la transición.
    /// </summary>
    public Action OnTransition { get; }

    /// <summary>
    /// Procesa un evento determinando si se dispara o no la transición. En caso
    /// de que se dispare la transición, ejecuta la acción
    /// <see cref="Transition.OnTransition"/> si es diferente de <c>null</c> y
    /// retorna el estado siguiente <see cref="Transition.NextState"/>.
    /// </summary>
    /// <remarks>
    /// El evento <see cref="Event.Else"/> dispara todas las transiciones.
    /// </remarks>
    /// <param name="event">El evento a procesar.</param>
    /// <returns>Retorna el estado siguiente NextState si el evento procesado
    /// dispara esta transición; retorna null en caso contrario.</returns>
    public State ProcessEvent(Event @event)
    {
        if (this.TriggerEvent != @event
            && this.TriggerEvent != Event.Else)
        {
            return null;
        }

        this.OnTransition?.Invoke();
        return this.NextState;
    }
}
