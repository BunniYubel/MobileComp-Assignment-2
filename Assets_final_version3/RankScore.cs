using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 


public class RankScore : MonoBehaviour
{
    public LeaderboardManager leaderboardManager;
    public int maxscore = 0;
    public int nowscore = 0;
    public Text maxscoreText;
    public Text nowscoreText;
    public Text nowtimeText;

    void Start()
    {
        leaderboardManager = GetComponent<LeaderboardManager>();
        if (leaderboardManager == null)
        {
            Debug.LogError("LeaderboardManager component not found on the GameObject.");
            return;
        }
        StartCoroutine(GetAndSetMaxScore());
    }


    IEnumerator GetAndSetMaxScore()
    {
        // Wait until the leaderboard data is fetched from the server.
        yield return StartCoroutine(leaderboardManager.BeginGetLeaderboardData());

        // Now that data is fetched, we can safely get the highest score.
        maxscore = leaderboardManager.GetHighestScore();
        maxscoreText.text = maxscore.ToString();
    }



    public void UpdateScore()
    {
        nowscore += 1;
        if (nowscore > maxscore)
        {
            maxscore = nowscore;
        }

        // 更新UI
        maxscoreText.text = maxscore.ToString();
        nowscoreText.text = nowscore.ToString();
        nowtimeText.text = Time.timeSinceLevelLoad.ToString("F2");

        // 如果leaderboard不是null，更新分数到服务器
        if (leaderboardManager != null)
        {
            leaderboardManager.UpdateScore(maxscore);
        }
    }
}
