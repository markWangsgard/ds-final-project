namespace ds_final_projectLogic;

public class Dungeon
{
    public Dictionary<int, List<Edge>> adjacencyList = new();
    public int startRoom = 0;
    public int endRoom = 1;
    public int currentRoom { get; set; }

    public Dungeon()
    {
        AddRoom(0);
        AddRoom(1);
        List<int> randomRooms = new List<int>();
        for (int i = 0; i < 30; i++)
        {
            int roomToAdd = Random.Shared.Next(3, 100);
            while (randomRooms.Contains(roomToAdd))
            {
                roomToAdd = Random.Shared.Next(3,100);
            }
            randomRooms.Add(roomToAdd);
        }
        foreach (int room in randomRooms)
        {
            AddRoom(room);
        }
        for (int i = 0; i < 15; i++)
        {
            int roomToAdd = randomRooms[i];
            if (i-1 == -1)
            {
                AddEdge(0, new Edge(randomRooms[i]));
            }
            else
            {
                AddEdge(randomRooms[i-1], new Edge(randomRooms[i]));
            }
        }
        AddEdge(randomRooms[14], new Edge(1));
        for (int i = 0;i < 50;i++)
        {
            int random1 = Random.Shared.Next(0, randomRooms.Count());
            int random2 = Random.Shared.Next(0, randomRooms.Count());

            AddEdge(randomRooms[random1], new Edge(randomRooms[random2]));
        }
        

    }
    public void DisplayDungeon()
    {
        foreach (var room in adjacencyList.Keys)
        {
            //Console.WriteLine($"Room: {room} - Connections: {String.Join(", ", adjacencyList[room])}");
            List<int> edges = adjacencyList[room].Select(r => r.ToRoom).ToList();
            Console.WriteLine($"Room: {room} - Connections: {string.Join(", ", edges)}");
        }
    }

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
        Console.WriteLine($"{room} added");
    }
    public void AddEdge(int fromRoom, Edge edge)
    {
        if (!RoomExists(fromRoom) || !RoomExists(edge.ToRoom))
        {
            Console.WriteLine($"One or both rooms do not exist - {fromRoom} -> {edge.ToRoom}");
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
