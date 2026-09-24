using NUnit.Framework;
using Ucu.Poo.Fsm;

namespace Ucu.Poo.Fsm.Tests;

[TestFixture]
public class StateTests
{
    [Test]
    public void AddTransition_StateWithoutTransitions_AddsTransition()
    {
        TestState state = new TestState();
        TestState nextState = new TestState();
        Play play = new Play();

        state.AddTransition(play, nextState);

        Assert.That(state.Transitions, Has.Count.EqualTo(1));
        Assert.That(state.Transitions[0].TriggerEvent, Is.SameAs(play));
        Assert.That(state.Transitions[0].NextState, Is.SameAs(nextState));
    }

    [Test]
    public void GetNextState_MatchingEventType_ReturnsNextState()
    {
        TestState state = new TestState();
        TestState nextState = new TestState();
        state.AddTransition(new Play(), nextState);

        State result = state.GetNextState(new Play());

        Assert.That(result, Is.SameAs(nextState));
    }

    [Test]
    public void GetNextState_EventWithoutTransition_ReturnsNull()
    {
        TestState state = new TestState();
        state.AddTransition(new Play(), new TestState());

        State result = state.GetNextState(new Pause());

        Assert.That(result, Is.Null);
    }

    private sealed class TestState : State
    {
        public override void OnEnter()
        {
        }

        public override void OnExit()
        {
        }
    }
}

