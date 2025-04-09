namespace ds_final_projectLogic;

public class Dungeon
{
    public Dictionary<int, List<Edge>> adjacencyList = new ();
    public int startRoom = 0;
    public int endRoom = 1;
    public int currentRoom { get; set; }

    public bool RoomExists(int room)
    {
        return adjacencyList.ContainsKey(room);
    }
    public bool EdgeExists(int fromRoom, int toRoom)
    {
        return adjacencyList[fromRoom].Any((edge) => edge.ToRoom == toRoom);
    }

    public void AddRoom(int room)
    {
        if (adjacencyList.ContainsKey(room))
        {
            Console.WriteLine("Room Already Added");
            return;
        }

        adjacencyList.Add(room, new List<Edge>());
    }
    public void AddEdge(int fromRoom, Edge edge)
    {
        if (!RoomExists(fromRoom) || !RoomExists(edge.ToRoom))
        {
            Console.WriteLine("One or both rooms do not exist");
            return;
        }
        if (EdgeExists(fromRoom, edge.ToRoom))
        {
            Console.WriteLine($"Edge from {fromRoom} to {edge.ToRoom} already exits");
            return;
        }

        adjacencyList[fromRoom].Add(edge);
        Console.WriteLine($"Edge from {fromRoom} to {edge.ToRoom} added");
    }
}
