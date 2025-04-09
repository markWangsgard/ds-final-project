namespace ds_final_projectLogic;

public class ChallengesBST
{
    public BSTNode RootNode;
}
public record BSTNode
{
    public BSTNode Left { get; set; }
    public Challenge Data { get; set; }
    public BSTNode Right { get; set; }

}