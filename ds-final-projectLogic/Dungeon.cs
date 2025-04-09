namespace ds_final_projectLogic;

public class Dungeon
{
    public Dictionary<int, List<Edge>> adjacencyList = new();
    public int startRoom = 0;
    public int endRoom = 1;
    public int currentRoom { get; set; }

    public Dungeon(int totalNumberOfRooms = 15, int setSolutionLength = 10, int numberOfConnections = 50)
    {
        AddRoom(0);
        AddRoom(1);
        List<int> randomRooms = new List<int>();
        for (int i = 0; i < totalNumberOfRooms; i++)
        {
            int roomToAdd = Random.Shared.Next(2, 100);
            while (randomRooms.Contains(roomToAdd))
            {
                roomToAdd = Random.Shared.Next(2, 100);
            }
            randomRooms.Add(roomToAdd);
        }
        foreach (int room in randomRooms)
        {
            AddRoom(room);
        }
        for (int i = 0; i < setSolutionLength; i++)
        {
            int roomToAdd = randomRooms[i];
            if (i - 1 == -1)
            {
                AddEdge(0, new Edge(randomRooms[i]));
            }
            else
            {
                AddEdge(randomRooms[i - 1], new Edge(randomRooms[i]));
            }
        }
        AddEdge(randomRooms[setSolutionLength - 1], new Edge(1));
        for (int i = 0; i < numberOfConnections; i++)
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
            Console.WriteLine($"Room: {room} [Total: {edges.Count()}] - Connections: {string.Join(", ", edges)}");
        }
    }
    public void displayRoom(int room, List<int> roomsVisited)
    {
        Console.Clear();
        Console.WriteLine($"Current Room: {room}");
        Console.WriteLine($"Rooms Visited: {string.Join(", ", roomsVisited)}");
        Console.WriteLine($"Options");
        List<int> availableRooms = adjacencyList[room].Select(r => r.ToRoom).ToList();
        for (int i = 1; i <= availableRooms.Count; i++)
        {
            Console.WriteLine($"{i}: {availableRooms[i - 1]}");
        }
        Console.Write("Please select the room you wish to enter: ");
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
