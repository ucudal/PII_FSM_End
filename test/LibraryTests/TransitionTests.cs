using NUnit.Framework;

namespace Ucu.Poo.Fsm.Tests
{
    [TestFixture]
    public class TransitionTests
    {
        [Test]
        public void Constructor_ValidArguments_StoresTriggerEventAndNextState()
        {
            Play play = new Play();
            Playing nextState = new Playing();

            Transition transition = new Transition(play, nextState);

            Assert.That(transition.TriggerSymbol, Is.SameAs(play));
            Assert.That(transition.NextState, Is.SameAs(nextState));
        }

        [Test]
        public void IsTriggeredBy_SameEventType_ReturnsTrue()
        {
            Transition transition = new Transition(new Play(), new Playing());

            Assert.That(transition.IsTriggeredBy(new Play()), Is.True);
        }

        [Test]
        public void IsTriggeredBy_DifferentEventType_ReturnsFalse()
        {
            Transition transition = new Transition(new Play(), new Playing());

            Assert.That(transition.IsTriggeredBy(new Stop()), Is.False);
        }
    }
}