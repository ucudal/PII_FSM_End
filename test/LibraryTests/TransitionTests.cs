using NUnit.Framework;
using Ucu.Poo.Fsm;
using System;

namespace Ucu.Poo.Fsm.Tests;

[TestFixture]
public class TransitionTests
{
    [Test]
    public void Constructor_ShouldInitializeProperties()
    {
        // Arrange
        Event triggerEvent = new Event("TestEvent");
        State nextState = new State("NextState");
        Action onTransition = () => { };

        // Act
        Transition transition = new Transition(triggerEvent, nextState, onTransition);

        // Assert
        Assert.That(transition.TriggerEvent, Is.EqualTo(triggerEvent));
        Assert.That(transition.NextState, Is.EqualTo(nextState));
        Assert.That(transition.OnTransition, Is.EqualTo(onTransition));
    }

    [Test]
    public void ProcessEvent_ShouldReturnNextState_WhenTriggerEventMatches()
    {
        // Arrange
        Event triggerEvent = new Event("TestEvent");
        State nextState = new State("NextState");
        Transition transition = new Transition(triggerEvent, nextState);

        // Act
        State result = transition.ProcessEvent(triggerEvent);

        // Assert
        Assert.That(result, Is.EqualTo(nextState));
    }

    [Test]
    public void ProcessEvent_ShouldReturnNull_WhenTriggerEventDoesNotMatch()
    {
        // Arrange
        Event triggerEvent = new Event("TestEvent");
        State nextState = new State("NextState");
        Transition transition = new Transition(triggerEvent, nextState);

        Event otherEvent = new Event("OtherEvent");

        // Act
        State result = transition.ProcessEvent(otherEvent);

        // Assert
        Assert.That(result, Is.Null);
    }

    [Test]
    public void ProcessEvent_ShouldInvokeOnTransitionAction_WhenTriggered()
    {
        // Arrange
        bool actionInvoked = false;
        Action onTransition = () => { actionInvoked = true; };
        Event triggerEvent = new Event("TestEvent");
        State nextState = new State("NextState");

        Transition transition = new Transition(triggerEvent, nextState, onTransition);

        // Act
        transition.ProcessEvent(triggerEvent);

        // Assert
        Assert.That(actionInvoked, Is.True);
    }

    [Test]
    public void ProcessEvent_ShouldReturnNextState_WhenEventIsElse()
    {
        // Arrange
        State nextState = new State("NextState");
        Transition transition = new Transition(Event.Else, nextState);

        // Act
        State result = transition.ProcessEvent(Event.Else);

        // Assert
        Assert.That(result, Is.EqualTo(nextState));
    }

    [Test]
    public void AddTransition_ShouldAddElseEventLast_WhenItIsTheFirstTransition()
    {
        // Arrange
        State state = new State("TestState");
        
        // Act
        Transition elseTransition = state.AddTransition(Event.Else, new State("ElseState"));

        // Assert
        Assert.That(state.Transitions, Has.Exactly(1).EqualTo(elseTransition));
        Assert.That(state.Transitions[0].TriggerEvent, Is.EqualTo(Event.Else));
    }

    [Test]
    public void AddTransition_ShouldAddNonElseEventBeforeElseEvent()
    {
        // Arrange
        State state = new State("TestState");
        Transition elseTransition = state.AddTransition(Event.Else, new State("ElseState"));

        // Act
        Transition nonElseTransition = state.AddTransition(new Event("TestEvent"), new State("TestState"));

        // Assert
        Assert.That(state.Transitions.Count, Is.EqualTo(2));
        Assert.That(state.Transitions[0], Is.EqualTo(nonElseTransition));
        Assert.That(state.Transitions[1], Is.EqualTo(elseTransition));
    }

    [Test]
    public void AddTransition_ShouldKeepElseEventLast_WhenAddedMultipleTransitions()
    {
        // Arrange
        State state = new State("TestState");
        Transition elseTransition = state.AddTransition(Event.Else, new State("ElseState"));
        Transition firstTransition = state.AddTransition(new Event("FirstEvent"), new State("FirstState"));
        
        // Act
        Transition secondTransition = state.AddTransition(new Event("SecondEvent"), new State("SecondState"));

        // Assert
        Assert.That(state.Transitions.Count, Is.EqualTo(3));
        Assert.That(state.Transitions[0], Is.EqualTo(firstTransition));
        Assert.That(state.Transitions[1], Is.EqualTo(secondTransition));
        Assert.That(state.Transitions[2], Is.EqualTo(elseTransition));
    }

    [Test]
    public void AddTransition_ShouldReturnCorrectTransition_WhenAdding()
    {
        // Arrange
        State state = new State("TestState");
        Event triggerEvent = new Event("TestEvent");
        State nextState = new State("NextState");

        // Act
        Transition transition = state.AddTransition(triggerEvent, nextState);

        // Assert
        Assert.That(transition.TriggerEvent, Is.EqualTo(triggerEvent));
        Assert.That(transition.NextState, Is.EqualTo(nextState));
    }
}
