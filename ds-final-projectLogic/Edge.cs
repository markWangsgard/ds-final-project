namespace ds_final_projectLogic;

public record Edge
{
    public int ToRoom;
    private int ChallengeID;
    public Challenge Challenge
    {
        get
        {
            return ChallengesBST.GetChallenge(ChallengeID);
        }
    }


    public Edge(int toRoom, bool first = false)
    {
        this.ToRoom = toRoom;

        if (first)
        {
            ChallengeID = -1;
        }
        else
        {
            ChallengeID = Random.Shared.Next(100);
            while (ChallengesBST.nodeExists(ChallengeID))
            {
                ChallengeID = Random.Shared.Next(100);
            }
            Challenge temp = new Challenge(ChallengeID, first);
            ChallengesBST.Insert(temp);
        }
    }
}