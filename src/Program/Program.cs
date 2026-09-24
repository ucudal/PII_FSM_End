using Ucu.Poo.Fsm;

public class Program
{
    public static void Main()
    {
        StateMachine player = new MusicPlayer();
        Event[] buttons = new Event[] { new Play(), new Pause(), new Play(), new Stop() };
        player.ProcessEvents(buttons);
    }
}

