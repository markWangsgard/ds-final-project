using ds_final_projectLogic;

Console.WriteLine("Setting Up...");

Dungeon dungeon1 = new Dungeon();
dungeon1.DisplayDungeon();
//Console.Clear();

/*
Hero player = new Hero();
int CurrentRoom = 0;
List<int> roomsVisited = new List<int>();
while (player.Health > 0 || CurrentRoom != 1)
{
    if (CurrentRoom == 1)
    {
        break;
    }
    dungeon1.displayRoom(CurrentRoom, roomsVisited);
    roomsVisited.Add(CurrentRoom);
    int selection = int.Parse(Console.ReadKey(true).KeyChar.ToString());
    CurrentRoom = dungeon1.adjacencyList[CurrentRoom][selection-1].ToRoom;
}
Console.Clear();
Console.WriteLine("You Won!");*/