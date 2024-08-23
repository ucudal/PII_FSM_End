using Ucu.Poo.Fsm;

Console.WriteLine("Hello, World!");

Event eventD = new Event("D");
Event eventA = new Event("a");
Event eventN = new Event("n");
Event eventI = new Event("i");
Event eventE = new Event("e");
Event eventL = new Event("l");
Event eventR = new Event("R");

StateMachine machine = new StateMachine();

State cero = machine.AddState("0");
State one = machine.AddState("1");
State two = machine.AddState("2");
State three = machine.AddState("3");
State four = machine.AddState("4");
State five = machine.AddState("5");

int count = 0;

Action<string> Show = (s) => Console.WriteLine(s);

cero.AddTransition(eventD, one, () => {
    count++;
    Show("D:0->1"); 
});
cero.AddTransition(Event.Else, cero, () => { 
    count = 0;
    Show("else:0->0");
});
cero.AddTransition(eventR, one, () =>
{
    count++;
    Show("R:0->1");
});

one.AddTransition(eventA, two, () =>
{
    count++;
    Show("A:1->2");
});
one.AddTransition(Event.Else, cero, () => { 
    count = 0;
    Show("Else:1->0");
});
two.AddTransition(eventN, three, () =>
{
    count++;
    Show("N:2->3");
});
two.AddTransition(Event.Else, cero, () => { 
    count = 0;
    Show("Else:2->0");
});
three.AddTransition(eventI, four, () =>
{
    count++;
    Show("I:3->4");
});
three.AddTransition(Event.Else, cero, () => { 
    count = 0;
    Show("Else:3->0");
});
four.AddTransition(eventE, five, () =>
{
    count++;
    Show("E:4->5");
});
four.AddTransition(Event.Else, cero, () => { 
    count = 0;
    Show("Else:4->0");
});
five.AddTransition(eventL, cero, () =>
{
    count++;
    Show("L:5->0");
    Console.WriteLine($"Conteo: {count}");
    count = 0;
});
five.AddTransition(Event.Else, cero, () =>
{
    count = 0;
    Show("Else:4->0");
});

machine.ProcessEvent(new Event[] { eventD, eventA, eventN, eventI, eventE, eventL });
machine.CurrentState = cero;
machine.ProcessEvent(new Event[] { eventR, eventA, eventN, eventI, eventE, eventL });