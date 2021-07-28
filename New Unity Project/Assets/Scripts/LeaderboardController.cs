using UnityEngine.UI;
using LootLocker.Requests;
using UnityEngine;

public class LeaderboardController : MonoBehaviour
{
    public InputField MemberID, PlayerScore;
    public int ID;
    int maxScores = 5;
    public Text[] Entries;


    private void Start()
    {
        LootLockerSDKManager.StartSession("Player", (response) =>
        {
            if (response.success)
            {
                Debug.Log("Success");
            }
            else
            {
                Debug.Log("Failed");
            }
        });
    }

    public void ShowScores()
    {
        LootLockerSDKManager.GetScoreList(ID, maxScores, (response) =>
        {


            if (response.success)
            {
                LootLocker.Requests.LootLockerLeaderboardMember[] scores = response.items;


                for (int i = 0; i < scores.Length; i++)
                {
                    Entries[i].text = (scores[i].rank + ".   " + scores[i].score);
                }

                if (scores.Length < maxScores)
                {
                    for (int i = scores.Length; i < maxScores; i++)
                    {
                        Entries[i].text = (i + 1).ToString() + ".  none";
                    }
                }
            }
            else
            {
                Debug.Log("Failed");
            }



        });
    }

    
    public void SubmitScore()
    {
        LootLockerSDKManager.SubmitScore(random.range.text, int.Parse(PlayerScore.text), ID, (response) =>
        {
            if (response.success)
            {
                Debug.Log("Success");
            }
            else
            {
                Debug.Log("Failed");
            }
            return;
        });
    }
}
