using NUnit.Framework;
using Ucu.Poo.Fsm;
using System;

namespace Ucu.Poo.Fsm.Tests;

[TestFixture]
public class StateTests
{
    [Test]
    public void AddTransition_ShouldAddTransitionToEmptyState()
    {
        // Arrange
        State state = new State("InitialState");
        Event triggerEvent = new Event("TestEvent");
        State nextState = new State("NextState");

        // Act
        Transition transition = state.AddTransition(triggerEvent, nextState);

        // Assert
        Assert.That(state.Transitions, Has.Exactly(1).EqualTo(transition));
    }

    [Test]
    public void AddTransition_ShouldAddElseTransitionLast()
    {
        // Arrange
        State state = new State("InitialState");
        Transition firstTransition = state.AddTransition(new Event("TestEvent1"), new State("NextState1"));

        // Act
        Transition elseTransition = state.AddTransition(Event.Else, new State("ElseState"));

        // Assert
        Assert.That(state.Transitions.Count, Is.EqualTo(2));
        Assert.That(state.Transitions[1], Is.EqualTo(elseTransition));
    }

    [Test]
    public void AddTransition_ShouldInsertBeforeElseTransition()
    {
        // Arrange
        State state = new State("InitialState");
        Transition elseTransition = state.AddTransition(Event.Else, new State("ElseState"));
        
        // Act
        Transition middleTransition = state.AddTransition(new Event("TestEvent2"), new State("NextState2"));

        // Assert
        Assert.That(state.Transitions.Count, Is.EqualTo(2));
        Assert.That(state.Transitions[0], Is.EqualTo(middleTransition));
        Assert.That(state.Transitions[1], Is.EqualTo(elseTransition));
    }

    [Test]
    public void ProcessEvent_ShouldReturnNextState_OnMatchingTransition()
    {
        // Arrange
        State initialState = new State("InitialState");
        Event triggerEvent = new Event("TestEvent");
        State nextState = new State("NextState");

        initialState.AddTransition(triggerEvent, nextState);

        // Act
        State resultState = initialState.ProcessEvent(triggerEvent);

        // Assert
        Assert.That(resultState, Is.EqualTo(nextState));
    }

    [Test]
    public void ProcessEvent_ShouldInvokeAction_OnTransition()
    {
        // Arrange
        State initialState = new State("InitialState");
        bool actionInvoked = false;
        Action transitionAction = () => actionInvoked = true;
        Event triggerEvent = new Event("TestEvent");
        State nextState = new State("NextState");

        initialState.AddTransition(triggerEvent, nextState, transitionAction);

        // Act
        initialState.ProcessEvent(triggerEvent);

        // Assert
        Assert.That(actionInvoked, Is.True);
    }

    [Test]
    public void ProcessEvent_ShouldReturnNull_OnNonMatchingEvent()
    {
        // Arrange
        State initialState = new State("InitialState");
        Event triggerEvent = new Event("TestEvent");
        Event nonMatchingEvent = new Event("NonMatchingEvent");
        State nextState = new State("NextState");

        initialState.AddTransition(triggerEvent, nextState);

        // Act
        State resultState = initialState.ProcessEvent(nonMatchingEvent);

        // Assert
        Assert.That(resultState, Is.Null);
    }

    [Test]
    public void ProcessEvent_ShouldTriggerElseTransition_WhenNoOtherMatch()
    {
        // Arrange
        State initialState = new State("InitialState");
        Event nonMatchingEvent = new Event("NonMatchingEvent");
        State nextState = new State("NextState");

        initialState.AddTransition(Event.Else, nextState);

        // Act
        State resultState = initialState.ProcessEvent(nonMatchingEvent);

        // Assert
        Assert.That(resultState, Is.EqualTo(nextState));
    }

    [Test]
    public void ProcessEvent_ShouldReturnNull_WhenNoTransitions()
    {
        // Arrange
        State initialState = new State("InitialState");
        Event someEvent = new Event("SomeEvent");

        // Act
        State resultState = initialState.ProcessEvent(someEvent);

        // Assert
        Assert.That(resultState, Is.Null);
    }
}
