using ds_final_projectLogic;

Console.WriteLine("Setting Up...");

Dungeon dungeon1 = new Dungeon();
//dungeon1.DisplayDungeon();
System.Console.Clear();
string welcomeMessage = """
Welcome to the Snow College's Basement!

You are a hero on a quest to escape the Basement.
You will encounter various challenges along the way.

Along some paths, there is a chance of finding loot.
This loot may be useful in your adventure.
Each Item is a one use item. You can hold max 5 items in your inventory.

Your goal is to escape the Basement by reaching room 100.
The rooms are randomized, so the bigger the number doesn't mean closer to the end.

Good luck!
Press any key to start your adventure!
""";
Console.WriteLine(welcomeMessage);
Console.ReadKey();

Hero player = new Hero();
int CurrentRoom = dungeon1.adjacencyList[0][0].ToRoom;
do
{
    dungeon1.VisitRoom(player, CurrentRoom);
    if (player.Health <= 0)
    {
        break;
    }
    int selection;
    do
    {
        try
        {
            selection = int.Parse(Console.ReadKey(true).KeyChar.ToString());
        }
        catch
        {
            selection = 0;
        }
    }
    while (selection < 0 || selection > dungeon1.adjacencyList[CurrentRoom].Count);
    CurrentRoom = dungeon1.adjacencyList[CurrentRoom][selection - 1].ToRoom;
    Console.WriteLine();
}
while (player.Health > 0 && CurrentRoom != 100);


Console.Clear();
if (player.Health <= 0)
{
    Console.WriteLine("You Died!");
}
else
{
    Console.WriteLine("You Escaped!");
}