namespace ds_final_projectLogic;

public record Edge
{
    public int ToRoom { get; set; }
    public Challenge Challenge { get; set; } = new Challenge();
}