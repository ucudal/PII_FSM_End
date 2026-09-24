using NUnit.Framework;
using Ucu.Poo.Fsm;

namespace Ucu.Poo.Fsm.Tests;

[TestFixture]
public class StateMachineTests
{
    [Test]
    public void AddState_NewState_AddsTheState()
    {
        StateMachine stateMachine = new StateMachine();
        State state = new TestState();

        stateMachine.AddState(state);

        Assert.That(stateMachine.States, Does.Contain(state));
    }

    [Test]
    public void AddState_DuplicateState_DoesNothing()
    {
        StateMachine stateMachine = new StateMachine();
        State firstState = new TestState();
        State secondState = new TestState();

        stateMachine.AddState(firstState);
        stateMachine.AddState(secondState);

        Assert.That(stateMachine.States, Does.Not.Contain(secondState));
    }

    [Test]
    public void AddState_FirstState_SetsCurrentState()
    {
        StateMachine stateMachine = new StateMachine();
        State firstState = new TestState();
        State secondState = new AnotherState();

        stateMachine.AddState(firstState);
        stateMachine.AddState(secondState);

        Assert.That(stateMachine.States, Has.Count.EqualTo(2));
        Assert.That(stateMachine.CurrentState, Is.SameAs(firstState));
    }

    [Test]
    public void ProcessEvent_NoStates_ReturnsFalse()
    {
        StateMachine stateMachine = new StateMachine();

        bool processed = stateMachine.ProcessEvent(new Play());

        Assert.That(processed, Is.False);
    }

    [Test]
    public void ProcessEvent_ValidTransition_ChangesStateAndInvokesLifecycleMethods()
    {
        StateMachine stateMachine = new StateMachine();
        TestState firstState = new TestState();
        AnotherState secondState = new AnotherState();
        firstState.AddTransition(new Play(), secondState);
        stateMachine.AddState(firstState);
        stateMachine.AddState(secondState);

        bool processed = stateMachine.ProcessEvent(new Play());

        Assert.That(processed, Is.True);
        Assert.That(stateMachine.CurrentState, Is.SameAs(secondState));
        Assert.That(firstState.ExitCount, Is.EqualTo(1));
        Assert.That(secondState.EnterCount, Is.EqualTo(1));
    }

    [Test]
    public void ProcessEvent_NoMatchingTransition_ReturnsFalseAndKeepsState()
    {
        StateMachine stateMachine = new StateMachine();
        TestState state = new TestState();
        stateMachine.AddState(state);

        bool processed = stateMachine.ProcessEvent(new Pause());

        Assert.That(processed, Is.False);
        Assert.That(stateMachine.CurrentState, Is.SameAs(state));
    }

    [Test]
    public void ProcessEvents_AllEventsProcessSuccessfully_EndsInFinalState()
    {
        StateMachine stateMachine = new StateMachine();
        TestState firstState = new TestState();
        TestState secondState = new TestState();
        TestState thirdState = new TestState();
        firstState.AddTransition(new Play(), secondState);
        secondState.AddTransition(new Pause(), thirdState);
        stateMachine.AddState(firstState);
        stateMachine.AddState(secondState);
        stateMachine.AddState(thirdState);

        bool processed = stateMachine.ProcessEvents(new Event[] { new Play(), new Pause() });

        Assert.That(processed, Is.True);
        Assert.That(stateMachine.CurrentState, Is.SameAs(thirdState));
    }

    [Test]
    public void ProcessEvents_UnprocessedEvent_ReturnsFalseAtFirstFailure()
    {
        StateMachine stateMachine = new StateMachine();
        TestState firstState = new TestState();
        TestState secondState = new TestState();
        firstState.AddTransition(new Play(), secondState);
        stateMachine.AddState(firstState);
        stateMachine.AddState(secondState);

        bool processed = stateMachine.ProcessEvents(new Event[] { new Play(), new Pause() });

        Assert.That(processed, Is.False);
        Assert.That(stateMachine.CurrentState, Is.SameAs(secondState));
    }

    private sealed class TestState : State
    {
        public int EnterCount { get; private set; }

        public int ExitCount { get; private set; }

        public override void OnEnter()
        {
            this.EnterCount++;
        }

        public override void OnExit()
        {
            this.ExitCount++;
        }
    }

    private class AnotherState : State
    {
        public int EnterCount { get; private set; }

        public int ExitCount { get; private set; }

        public override void OnEnter()
        {
            this.EnterCount++;
        }

        public override void OnExit()
        {
            this.ExitCount++;
        }
    }
}

