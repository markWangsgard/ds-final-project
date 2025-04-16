using System.Collections;

namespace ds_final_projectLogic;

public record Hero
{
    public int Strength { get; set; }
    public int Agility { get; set; }
    public int Intelligence { get; set; }
    public int Health { get; set; } = 20;
    public Queue<Item> Inventory { get; set; } = new Queue<Item>();

    public List<int> RoomsVisited { get; set; } = new List<int>() { 0 };

    public Hero(int strength = 7, int agility = 5, int intelligence = 6)
    {
        Strength = strength;
        Agility = agility;
        Intelligence = intelligence;
        Inventory.Enqueue(new Item("Boots of the Windrunner", 5, ChallengeType.Agility));
        Inventory.Enqueue(new Item("Pendant of Forgotten Lore", 5, ChallengeType.Intelligence));
    }
}
