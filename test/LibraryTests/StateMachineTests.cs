using NUnit.Framework;
using Ucu.Poo.Fsm;

namespace Ucu.Poo.Fsm.Tests
{
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

            StateMachine.AddStateResult firstResult = stateMachine.AddState(firstState);
            StateMachine.AddStateResult secondResult = stateMachine.AddState(secondState);

            Assert.That(firstResult, Is.EqualTo(StateMachine.AddStateResult.Success));
            Assert.That(secondResult, Is.EqualTo(StateMachine.AddStateResult.TypeAlreadyExists));
            Assert.That(stateMachine.States, Does.Not.Contain(secondState));
        }

        [Test]
        public void AddToAlphabet_DuplicateSymbolType_ReturnsTypeAlreadyExists()
        {
            StateMachine stateMachine = new StateMachine();
            InputSymbol firstSymbol = new TestSymbol();
            InputSymbol secondSymbol = new TestSymbol();

            StateMachine.AddToAlphabetResult firstResult = stateMachine.AddToAlphabet(firstSymbol);
            StateMachine.AddToAlphabetResult secondResult = stateMachine.AddToAlphabet(secondSymbol);

            Assert.That(firstResult, Is.EqualTo(StateMachine.AddToAlphabetResult.Success));
            Assert.That(secondResult, Is.EqualTo(StateMachine.AddToAlphabetResult.TypeAlreadyExists));
            Assert.That(stateMachine.Alphabet, Is.EquivalentTo(new[] { firstSymbol }));
        }

        [Test]
        public void AddState_TransitionSymbolNotInAlphabet_ReturnsErrorAndDoesNotAddState()
        {
            StateMachine stateMachine = new StateMachine();
            State state = new TestState();
            state.AddTransition(new TestSymbol(), new AnotherState());

            StateMachine.AddStateResult result = stateMachine.AddState(state);

            Assert.That(result, Is.EqualTo(StateMachine.AddStateResult.TransitionTriggerTypeNotInAlphabet));
            Assert.That(stateMachine.States, Is.Empty);
            Assert.That(stateMachine.CurrentState, Is.Null);
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

            bool processed = stateMachine.ProcessEvent(new TestSymbol());

            Assert.That(processed, Is.False);
        }

        [Test]
        public void ProcessEvent_ValidTransition_ChangesStateAndInvokesLifecycleMethods()
        {
            StateMachine stateMachine = new StateMachine();
            InputSymbol play = new TestSymbol();
            TestState firstState = new TestState();
            AnotherState secondState = new AnotherState();
            stateMachine.AddToAlphabet(play);
            firstState.AddTransition(play, secondState);
            stateMachine.AddState(firstState);
            stateMachine.AddState(secondState);

            bool processed = stateMachine.ProcessEvent(new TestSymbol());

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

            bool processed = stateMachine.ProcessEvent(new AnotherSymbol());

            Assert.That(processed, Is.False);
            Assert.That(stateMachine.CurrentState, Is.SameAs(state));
        }

        [Test]
        public void ProcessEvents_AllEventsProcessSuccessfully_EndsInFinalState()
        {
            StateMachine stateMachine = new StateMachine();
            InputSymbol play = new TestSymbol();
            InputSymbol pause = new AnotherSymbol();
            TestState firstState = new TestState();
            AnotherState secondState = new AnotherState();
            ThirdState thirdState = new ThirdState();
            stateMachine.AddToAlphabet(new InputSymbol[] { play, pause });
            firstState.AddTransition(play, secondState);
            secondState.AddTransition(pause, thirdState);
            stateMachine.AddState(firstState);
            stateMachine.AddState(secondState);
            stateMachine.AddState(thirdState);

            bool processed = stateMachine.ProcessEvents(new InputSymbol[] { new TestSymbol(), new AnotherSymbol() });

            Assert.That(processed, Is.True);
            Assert.That(stateMachine.CurrentState, Is.SameAs(thirdState));
        }

        [Test]
        public void ProcessEvents_UnprocessedEvent_ReturnsFalseAtFirstFailure()
        {
            StateMachine stateMachine = new StateMachine();
            InputSymbol play = new TestSymbol();
            TestState firstState = new TestState();
            AnotherState secondState = new AnotherState();
            stateMachine.AddToAlphabet(play);
            firstState.AddTransition(play, secondState);
            stateMachine.AddState(firstState);
            stateMachine.AddState(secondState);

            bool processed = stateMachine.ProcessEvents(new InputSymbol[] { new TestSymbol(), new AnotherSymbol() });

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

        private sealed class AnotherState : State
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

        private sealed class ThirdState : State
        {
            public override void OnEnter()
            {
            }

            public override void OnExit()
            {
            }
        }

        private sealed class TestSymbol : InputSymbol
        {
        }

        private sealed class AnotherSymbol : InputSymbol
        {
        }
    }
}
