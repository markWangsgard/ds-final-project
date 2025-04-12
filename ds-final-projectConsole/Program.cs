using ds_final_projectLogic;

Console.WriteLine("Setting Up...");

Dungeon dungeon1 = new Dungeon();
dungeon1.DisplayDungeon();
//Console.Clear();


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