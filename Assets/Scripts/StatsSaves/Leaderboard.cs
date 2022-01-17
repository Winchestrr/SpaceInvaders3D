using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Leaderboard : MonoBehaviour
{
    public dreamloLeaderBoard dreamlo;
    public TextMeshProUGUI leaderboardText;

    public string scores;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.M))
        {
            GetLeaderboardScores();
        }
    }

    public void GetLeaderboardScores()
    {
        leaderboardText.text = "";

        List<dreamloLeaderBoard.Score> scoreList = dreamlo.ToListHighToLow();

        if (scoreList == null) return;
        else
        {
            int maxToDisplay = 20;
            int count = 0;

            foreach (dreamloLeaderBoard.Score currentScore in scoreList)
            {
                count++;

                leaderboardText.text +=
                    ((count) + ". " +
                    currentScore.playerName +
                    ": " + currentScore.score +
                    ", " + currentScore.shortText + "\n");

                if (count >= maxToDisplay) break;
            }
        }
    }
}
