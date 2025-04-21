using System.Collections;

namespace ds_final_projectLogic;

public record Hero
{
    public int Strength { get; set; }
    public int Agility { get; set; }
    public int Intelligence { get; set; }
    public int Health { get; set; } = 20;
    public Stack<Item> Inventory { get; set; } = new Stack<Item>();

    public Stack<int> RoomsVisited { get; set; } = new Stack<int>();

    public Hero(int strength = 7, int agility = 5, int intelligence = 6)
    {
        Strength = strength;
        Agility = agility;
        Intelligence = intelligence;
        RoomsVisited.Push(0);
        Inventory.Push(new Item("Boots of the Windrunner", 5, ChallengeType.Agility));
        Inventory.Push(new Item("Pendant of Forgotten Lore", 5, ChallengeType.Intelligence));
    }
}
