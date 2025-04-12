using System.Collections;

namespace ds_final_projectLogic;

public record Hero
{
    public int Strength { get; set; }
    public int Agility { get; set; }
    public int Intelligence { get; set; }
    public int Health { get; set; } = 2;
    public Queue<Item> Inventory { get; set; } = new Queue<Item>();

    public List<int> RoomsVisited { get; set; } = new List<int>() { 0 };

    public Hero(int strength = 3, int agility = 3, int intelligence = 3)
    {
        Strength = strength;
        Agility = agility;
        Intelligence = intelligence;
    }
}
