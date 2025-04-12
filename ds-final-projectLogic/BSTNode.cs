namespace ds_final_projectLogic;

public record BSTNode
{
    public BSTNode Left { get; set; }
    public Challenge Data { get; set; }
    public BSTNode Right { get; set; }
    public int Height = 1;

    public BSTNode(Challenge challenge)
    {
        Data = challenge;
    }

}