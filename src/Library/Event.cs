//------------------------------------------------------------------------------
// <copyright file="Event.cs" company="Universidad Católica del Uruguay">
//     Copyright (c) Programación II. Derechos reservados.
// </copyright>
//------------------------------------------------------------------------------

namespace Ucu.Poo.Fsm;

/// <summary>
/// Esta clase representa un evento procesable por la máquina de estado.
/// </summary>
public class Event
{
    /// <summary>
    /// El nombre del evento <see cref="Event.Else"/>.
    /// </summary>
    public const string ELSEEVENTNAME = "Else";

    private static Event @else = new Event(ELSEEVENTNAME);

    /// <summary>
    /// Inicializa una nueva instancia de la clase <see cref="Event"/>.
    /// </summary>
    /// <param name="name">El nombre del evento.</param>
    public Event(string name)
    {
        this.Name = name;
    }

    /// <summary>
    /// Obtiene un evento especial que tiene la semántica de "todos los demás
    /// eventos". A un estado se agregan las transiciones para todos los eventos
    /// que provocan un cambio de estado, más una transición con este evento
    /// para representar todos los demás casos.
    /// </summary>
    public static Event Else
    {
        get { return @else; }
    }

    /// <summary>
    /// Obtiene el nombre del evento. El nombre del evento no es usado durante
    /// el procesamiento.
    /// </summary>
    public string Name { get; }
}