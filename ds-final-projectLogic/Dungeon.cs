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
                Console.WriteLine($"To Room: {edge.ToRoom}  Challenge: {(edge.Challenge != null ? edge.Challenge.DifficultyLevel : "No Challenge")} {(edge.Challenge != null ? edge.Challenge.DifficultyType : "No Challenge")}");
            }
            Console.WriteLine($"");
        }
    }
    private void displayRoom(int room, List<int> roomsVisited)
    {
        Console.WriteLine();
        Console.WriteLine($"Current Room: {room}");
        Console.WriteLine($"Rooms Visited: {string.Join(", ", roomsVisited)}");
        Console.WriteLine();
        Console.WriteLine($"Options");
        Console.WriteLine($"# | Room Number | Required Stat | Required Level |");
        List<Edge> availableRooms = adjacencyList[room];
        availableRooms.Sort((a, b) => a.ToRoom - b.ToRoom);
        for (int i = 0; i < availableRooms.Count; i++)
        {
                Console.WriteLine($"{i + 1,1} | {availableRooms[i].ToRoom,11} | {(availableRooms[i].Challenge != null ? availableRooms[i].Challenge.DifficultyType : "Path Taken"),13} | {(availableRooms[i].Challenge != null ? availableRooms[i].Challenge.DifficultyLevel : "Path Taken"),14} |");
        }
        Console.Write("Please select the room you wish to enter: ");
    }
    public void VisitRoom(Hero hero, int room)
    {
        int currentRoom = hero.RoomsVisited.Last();
        Challenge challenge = adjacencyList[currentRoom].Find(e => e.ToRoom == room).Challenge;
        if (challenge != null)
        {
            int strengthLevel = hero.Strength;
            int agilityLevel = hero.Agility;
            int intelligenceLevel = hero.Intelligence;
            if (hero.Inventory.Count() > 0 && hero.Inventory.Peek().BoostType == challenge.DifficultyType)
            {
                var boost = hero.Inventory.Dequeue();
                strengthLevel += boost.BoostType == ChallengeType.Strength ? boost.BoostValue : 0;
                agilityLevel += boost.BoostType == ChallengeType.Agility ? boost.BoostValue : 0;
                intelligenceLevel += boost.BoostType == ChallengeType.Intelligence ? boost.BoostValue : 0;
            }

            ChallengesBST.Delete(challenge.ID);

            if (challenge.DifficultyType == ChallengeType.Strength && strengthLevel < challenge.DifficultyLevel)
            {
                hero.Health -= challenge.DifficultyLevel - strengthLevel;
            }
            if (challenge.DifficultyType == ChallengeType.Intelligence && intelligenceLevel < challenge.DifficultyLevel)
            {
                hero.Health -= challenge.DifficultyLevel - intelligenceLevel;
            }
            if (challenge.DifficultyType == ChallengeType.Agility && agilityLevel < challenge.DifficultyLevel)
            {
                hero.Health -= challenge.DifficultyLevel - agilityLevel;
            }
        }

        if (hero.Health <= 0)
        {
            return;
        }

        //success
        Console.Clear();
        // hero Stats
        Console.WriteLine($"Hero Stats:");
        Console.WriteLine($"Health: {hero.Health}");

        int boostedStrength = hero.Strength + (hero.Inventory.Count() > 0 && hero.Inventory.Peek().BoostType == ChallengeType.Strength ? hero.Inventory.Peek().BoostValue : 0);
        Console.WriteLine($"Strength: {boostedStrength}");

        int boostedAgility = hero.Agility + (hero.Inventory.Count() > 0 && hero.Inventory.Peek().BoostType == ChallengeType.Agility ? hero.Inventory.Peek().BoostValue : 0);
        Console.WriteLine($"Agility: {boostedAgility}");

        int boostedIntelligence = hero.Intelligence + (hero.Inventory.Count() > 0 && hero.Inventory.Peek().BoostType == ChallengeType.Intelligence ? hero.Inventory.Peek().BoostValue : 0);
        Console.WriteLine($"Intelligence: {boostedIntelligence}");
        Console.WriteLine();

        // Loot Items
        Item? loot = getLoot();
        if (loot != null)
        {
            Console.WriteLine($"You found {loot.Type}!");
            if (hero.Inventory.Count == 5)
            {
                hero.Inventory.Dequeue();
            }
            hero.Inventory.Enqueue(loot);
            Console.WriteLine($"It provides +{loot.BoostValue} {loot.BoostType}");
        }
        else
        {
            Console.WriteLine("No loot found");
        }

        if (hero.Inventory.Count > 0)
        {
            Console.WriteLine();
            Console.WriteLine($"# of Items in Inventory: {hero.Inventory.Count()}");
            Console.WriteLine($"Current Item: {hero.Inventory.Peek().Type}");
            Console.WriteLine($"Boost: +{hero.Inventory.Peek().BoostValue} {hero.Inventory.Peek().BoostType}");
        }

        displayRoom(room, hero.RoomsVisited);
        hero.RoomsVisited.Add(room);

    }

    private Item? getLoot()
    {
        int randomNumber = Random.Shared.Next(0, 100);
        if (randomNumber < 30)
        {
            List<Item> lootItems = new List<Item>()
            {
               new Item("Boots of the Windrunner", Random.Shared.Next(0,10), ChallengeType.Agility),
               new Item("Cloak of Shadows", Random.Shared.Next(0,10), ChallengeType.Agility),
               new Item("Feather Step Anklets", Random.Shared.Next(0,10), ChallengeType.Agility),
               new Item("Ring of the Quick Fang", Random.Shared.Next(0,10), ChallengeType.Agility),
               new Item("Hunter's Hood", Random.Shared.Next(0,10), ChallengeType.Agility),
               new Item("Gauntlets of the Titan", Random.Shared.Next(0,10), ChallengeType.Strength),
               new Item("Ogre Bone Pauldrons", Random.Shared.Next(0,10), ChallengeType.Strength),
               new Item("Belt of the Mountain King", Random.Shared.Next(0,10), ChallengeType.Strength),
               new Item("Craghammer Warboots", Random.Shared.Next(0,10), ChallengeType.Strength),
               new Item("WristWraps of Wrath", Random.Shared.Next(0,10), ChallengeType.Strength),
               new Item("Circlet of the Mind's Eye", Random.Shared.Next(0,10), ChallengeType.Intelligence),
               new Item("Robes of the Deep Scholar", Random.Shared.Next(0,10), ChallengeType.Intelligence),
               new Item("Ring of Arcane Insight", Random.Shared.Next(0,10), ChallengeType.Intelligence),
               new Item("Pendant of Forgotten Lore", Random.Shared.Next(0,10), ChallengeType.Intelligence),
               new Item("Codex of Crystal Thought", Random.Shared.Next(0,10), ChallengeType.Intelligence),
            };
            return lootItems[Random.Shared.Next(lootItems.Count() - 1)];
        }
        else
        {
            return null;
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
