using NUnit.Framework;
using Ucu.Poo.Fsm;

namespace Ucu.Poo.Fsm.Tests;

[TestFixture]
public class PlayerTests
{
    [Test]
    public void PlayerEvents_ConcreteEvents_InheritFromEvent()
    {
        Assert.That(new Play(), Is.TypeOf<Play>().And.InstanceOf<Event>());
        Assert.That(new Pause(), Is.TypeOf<Pause>().And.InstanceOf<Event>());
        Assert.That(new Stop(), Is.TypeOf<Stop>().And.InstanceOf<Event>());
    }

    [Test]
    public void PlayerStates_ConcreteStates_InheritFromState()
    {
        Assert.That(new Stopped(), Is.TypeOf<Stopped>().And.InstanceOf<State>());
        Assert.That(new Playing(), Is.TypeOf<Playing>().And.InstanceOf<State>());
        Assert.That(new Paused(), Is.TypeOf<Paused>().And.InstanceOf<State>());
    }

    [Test]
    public void MusicPlayer_InitialState_StartsStoppedWithThreeStates()
    {
        MusicPlayer player = new MusicPlayer();

        Assert.That(player.States, Has.Count.EqualTo(3));
        Assert.That(player.CurrentState, Is.TypeOf<Stopped>());
    }

    [Test]
    public void MusicPlayer_ValidEventSequence_EndsStopped()
    {
        MusicPlayer player = new MusicPlayer();

        bool processed = player.ProcessEvents(new Event[]
        {
            new Play(),
            new Pause(),
            new Play(),
            new Stop(),
        });

        Assert.That(processed, Is.True);
        Assert.That(player.CurrentState, Is.TypeOf<Stopped>());
    }

    [Test]
    public void MusicPlayer_PauseWhileStopped_ReturnsFalse()
    {
        MusicPlayer player = new MusicPlayer();

        bool processed = player.ProcessEvent(new Pause());

        Assert.That(processed, Is.False);
        Assert.That(player.CurrentState, Is.TypeOf<Stopped>());
    }

    [Test]
    public void PlayerStates_LifecycleMethods_WriteLifecycleMessages()
    {
        using StringWriter output = new StringWriter();
        TextWriter originalOutput = Console.Out;
        Console.SetOut(output);

        try
        {
            new Stopped().OnEnter();
            new Stopped().OnExit();
            new Playing().OnEnter();
            new Playing().OnExit();
            new Paused().OnEnter();
            new Paused().OnExit();
        }
        finally
        {
            Console.SetOut(originalOutput);
        }

        Assert.That(output.ToString(), Does.Contain("Entrando a detenido..."));
        Assert.That(output.ToString(), Does.Contain("Saliendo de detenido..."));
        Assert.That(output.ToString(), Does.Contain("Comenzando a reproducir..."));
        Assert.That(output.ToString(), Does.Contain("Deteniendo la reproducción..."));
        Assert.That(output.ToString(), Does.Contain("Entrando en pausa..."));
        Assert.That(output.ToString(), Does.Contain("Saliendo de pausa..."));
    }
}