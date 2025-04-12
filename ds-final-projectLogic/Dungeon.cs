using System.Numerics;

namespace ds_final_projectLogic;

public class Dungeon
{
    public Dictionary<int, List<Edge>> adjacencyList = new();
    public int startRoom = 0;
    public int endRoom = 100;

    public Dungeon(int totalNumberOfRooms = 15, int setSolutionLength = 10, int numberOfConnections = 50)
    {
        AddRoom(0);
        AddRoom(100);
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
                AddEdge(0, new Edge(randomRooms[i], true));
            }
            else
            {
                AddEdge(randomRooms[i - 1], new Edge(randomRooms[i]));
            }
        }
        AddEdge(randomRooms[setSolutionLength - 1], new Edge(100));
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
            Console.WriteLine($"Connections");
            foreach (var edge in adjacencyList[room])
            {
                Console.WriteLine($"To Room: {edge.ToRoom}  Challenge: {edge.Challenge.DifficultyLevel} {edge.Challenge.DifficultyType}");
            }
            Console.WriteLine($"");
        }
    }
    private void displayRoom(int room, List<int> roomsVisited)
    {
        Console.WriteLine($"Current Room: {room}");
        Console.WriteLine($"Rooms Visited: {string.Join(", ", roomsVisited)}");
        Console.WriteLine($"Options");
        List<int> availableRooms = adjacencyList[room].Select(r => r.ToRoom).ToList();
        availableRooms.Sort();
        for (int i = 1; i <= availableRooms.Count; i++)
        {
            Console.WriteLine($"{i}: Room {availableRooms[i - 1]}");
        }
        Console.Write("Please select the room you wish to enter: ");
    }
    public void VisitRoom(Hero hero, int room)
    {
        int currentRoom = hero.RoomsVisited.Last();
        Challenge challenge = adjacencyList[currentRoom].Find(e => e.ToRoom == room).Challenge;
        if (challenge != null)
        {

            ChallengesBST.Delete(challenge.ID);

            if (challenge.DifficultyType == ChallengeType.Strength && hero.Strength < challenge.DifficultyLevel)
            {
                hero.Health -= challenge.DifficultyLevel - hero.Strength;
            }
            if (challenge.DifficultyType == ChallengeType.Intelligence && hero.Intelligence < challenge.DifficultyLevel)
            {
                hero.Health -= challenge.DifficultyLevel - hero.Intelligence;
            }
            if (challenge.DifficultyType == ChallengeType.Agility && hero.Agility < challenge.DifficultyLevel)
            {
                hero.Health -= challenge.DifficultyLevel - hero.Agility;
            }
        }

        if (hero.Health <= 0)
        {
            return;
        }

        //success
        Console.Clear();
        Console.WriteLine($"Hero Health: {hero.Health}");
        displayRoom(room, hero.RoomsVisited);
        hero.RoomsVisited.Add(room);

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
