namespace ds_final_projectLogic;

public record Item
{
    public string Type { get; set; }
    public int BoostValue { get; set; }
    public ChallengeType BoostType { get; set; }

    public Item(string type, int boostValue, ChallengeType boostType)
    {
        this.Type = type;
        this.BoostValue = boostValue;
        this.BoostType = boostType;
    }
}