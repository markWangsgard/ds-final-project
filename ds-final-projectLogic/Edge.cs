namespace ds_final_projectLogic;

public record Edge
{
    public int ToRoom;
    public Challenge Challenge { get; set; } = new Challenge();

    public Edge(int toRoom)
    {
        this.ToRoom = toRoom;
    }
}