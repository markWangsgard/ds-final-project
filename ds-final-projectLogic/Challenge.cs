namespace ds_final_projectLogic;

public class Challenge
{
    public int ID { get; set; }
    public int DifficultyLevel { get; set; }
    public ChallengeType DifficultyType { get; set; }

    public Challenge(int id)
    {
        ID = id;
        DifficultyType = (ChallengeType)Random.Shared.Next(3);
        DifficultyLevel = Random.Shared.Next(6, 10);
        //if ((int)DifficultyType == 0)
        //{
        //    DifficultyLevel = Random.Shared.Next(6, 10);
        //}
        //else if ((int)DifficultyType == 1)
        //{
        //    DifficultyLevel = Random.Shared.Next(7, 10);
        //}
        //else
        //{
        //    DifficultyLevel = Random.Shared.Next(8, 10);
        //}
    }
}
