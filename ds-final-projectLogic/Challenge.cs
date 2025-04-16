namespace ds_final_projectLogic;

public class Challenge
{
    public int ID { get; set; }
    public int DifficultyLevel { get; set; }
    public ChallengeType DifficultyType { get; set; }

    public Challenge(int id, bool first = false)
    {
        ID = id;
        DifficultyType = (ChallengeType)Random.Shared.Next(3);
        DifficultyLevel = Random.Shared.Next(6, 10);
    }
}
