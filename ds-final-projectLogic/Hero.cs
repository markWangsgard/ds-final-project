using System.Collections;

namespace ds_final_projectLogic;

public record Hero
{
    public int Strength { get; set; }
    public int Agility { get; set; }
    public int Intelligence { get; set; }
    public int Health { get; set; } = 20;
    public Queue<Item> Inventory { get; set; } = new Queue<Item>();
}
