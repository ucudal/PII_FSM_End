namespace Ucu.Poo.Fsm.Tests;

[TestFixture]
public class StateMachineTests
{
    [Test]
    public void AddState_ShouldAddStateToMachine()
    {
        // Arrange
        StateMachine stateMachine = new StateMachine();

        // Act
        State state = stateMachine.AddState("State1");

        // Assert
        Assert.That(stateMachine.States, Has.Exactly(1).EqualTo(state));
    }

    [Test]
    public void AddState_ShouldSetInitialState()
    {
        // Arrange
        StateMachine stateMachine = new StateMachine();

        // Act
        State state = stateMachine.AddState("State1");

        // Assert
        Assert.That(stateMachine.CurrentState, Is.EqualTo(state));
    }

    [Test]
    public void AddState_ShouldNotChangeInitialStateWhenAddingMoreStates()
    {
        // Arrange
        StateMachine stateMachine = new StateMachine();
        State initialState = stateMachine.AddState("State1");

        // Act
        stateMachine.AddState("State2");

        // Assert
        Assert.That(stateMachine.CurrentState, Is.EqualTo(initialState));
    }

    [Test]
    public void ProcessEvent_ShouldReturnFalseWhenNoCurrentState()
    {
        // Arrange
        StateMachine stateMachine = new StateMachine();
        Event testEvent = new Event("TestEvent");

        // Act
        bool result = stateMachine.ProcessEvent(testEvent);

        // Assert
        Assert.That(result, Is.False);
    }

    [Test]
    public void ProcessEvent_ShouldChangeStateOnValidTransition()
    {
        // Arrange
        StateMachine stateMachine = new StateMachine();
        State state1 = stateMachine.AddState("State1");
        State state2 = stateMachine.AddState("State2");
        Event testEvent = new Event("TestEvent");

        state1.AddTransition(testEvent, state2);

        // Act
        bool result = stateMachine.ProcessEvent(testEvent);

        // Assert
        Assert.That(result, Is.True);
        Assert.That(stateMachine.CurrentState, Is.EqualTo(state2));
    }

    [Test]
    public void ProcessEvent_ShouldReturnFalseOnNoMatchingTransition()
    {
        // Arrange
        StateMachine stateMachine = new StateMachine();
        State state1 = stateMachine.AddState("State1");
        Event testEvent = new Event("TestEvent");

        // Act
        bool result = stateMachine.ProcessEvent(testEvent);

        // Assert
        Assert.That(result, Is.False);
        Assert.That(stateMachine.CurrentState, Is.EqualTo(state1));
    }

    [Test]
    public void ProcessEventSequence_ShouldProcessAllEvents()
    {
        // Arrange
        StateMachine stateMachine = new StateMachine();
        State state1 = stateMachine.AddState("State1");
        State state2 = stateMachine.AddState("State2");
        State state3 = stateMachine.AddState("State3");
        Event event1 = new Event("Event1");
        Event event2 = new Event("Event2");

        state1.AddTransition(event1, state2);
        state2.AddTransition(event2, state3);

        // Act
        bool result = stateMachine.ProcessEvent(new[] { event1, event2 });

        // Assert
        Assert.That(result, Is.True);
        Assert.That(stateMachine.CurrentState, Is.EqualTo(state3));
    }

    [Test]
    public void ProcessEventSequence_ShouldReturnFalseOnFirstFailedEvent()
    {
        // Arrange
        StateMachine stateMachine = new StateMachine();
        State state1 = stateMachine.AddState("State1");
        State state2 = stateMachine.AddState("State2");
        Event event1 = new Event("Event1");
        Event event3 = new Event("Event3");

        state1.AddTransition(event1, state2);
        // No transition from state2 to handle event3

        // Act
        bool result = stateMachine.ProcessEvent(new[] { event1, event3 });

        // Assert
        Assert.That(result, Is.False);
        Assert.That(stateMachine.CurrentState, Is.EqualTo(state2));
    }

    [Test]
    public void ProcessEvent_ShouldHandleElseTransition()
    {
        // Arrange
        StateMachine stateMachine = new StateMachine();
        State state1 = stateMachine.AddState("State1");
        State state2 = stateMachine.AddState("State2");
        Event nonMatchingEvent = new Event("NonMatchingEvent");

        state1.AddTransition(Event.Else, state2);

        // Act
        bool result = stateMachine.ProcessEvent(nonMatchingEvent);

        // Assert
        Assert.That(result, Is.True);
        Assert.That(stateMachine.CurrentState, Is.EqualTo(state2));
    }
}
