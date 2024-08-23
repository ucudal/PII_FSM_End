using NUnit.Framework;
using Ucu.Poo.Fsm;

namespace Ucu.Poo.Fsm.Tests;

[TestFixture]
public class EventTests
{
    [Test]
    public void Constructor_ShouldInitializeNameProperty()
    {
        // Arrange
        string eventName = "TestEvent";

        // Act
        Event eventInstance = new Event(eventName);

        // Assert
        Assert.That(eventName, Is.EqualTo(eventInstance.Name));
    }

    [Test]
    public void ElseProperty_ShouldReturnSpecialElseEvent()
    {
        // Act
        Event elseEvent = Event.Else;

        // Assert
        Assert.That(elseEvent, Is.Not.Null);
        Assert.That(elseEvent.Name, Is.EqualTo(Event.ELSEEVENTNAME));
    }

    [Test]
    public void ElseProperty_ShouldAlwaysReturnSameInstance()
    {
        // Act
        Event firstElseEvent = Event.Else;
        Event secondElseEvent = Event.Else;

        // Assert
        Assert.That(firstElseEvent, Is.EqualTo(secondElseEvent));
    }
}