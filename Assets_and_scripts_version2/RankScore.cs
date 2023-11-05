using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class RankScore : MonoBehaviour
{
    // Start is called before the first frame update
    LeaderboardManager leaderboard =new LeaderboardManager();
    //Player打掉敌人的时候，nowscore+1
    public int maxscore = 0;
    public int nowscore = 0;
    public Text maxscoreText;
    public Text nowscoreText;
    public Text nowtimeText;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    //更新分数
    public void UpdateScroe()
    {
        nowscore += 1;
        if (nowscore > maxscore)
        {
            maxscore = nowscore;
        }
        leaderboard.playerScore = nowscore;
        maxscoreText.text =  maxscore.ToString(); 
        nowscoreText.text =  nowscore.ToString();
        nowtimeText.text = Time.time.ToString();
    }

}
