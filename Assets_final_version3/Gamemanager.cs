using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Gamemanager : MonoBehaviour
{
    public InputField nameInputField;
    public static Gamemanager Instance;
    public LeaderboardManager leaderboard ;
    // Start is called before the first frame update
    public List<GameObject> objs;
    public  static List<int> score_list = new List<int> { 0, 0, 0, 0, 0};
    public  List<Text> texts;
    public Text m_text_score;
    public Text m_text_max;
    public Button m_button_restart;
    public Button m_button_exit;

    public  int m_score = 0;
    public static int m_max = 0;

    public AudioClip m_musicClip;
    public AudioSource m_AudioSource;

    bool cmp(int x, int y)
    {
        return x > y;
    }


    void Start()
    {
        Instance = this;

        m_AudioSource.clip = m_musicClip;
        m_AudioSource.Play();
        m_AudioSource.loop = true;

        m_text_score.text = m_score.ToString();
        m_text_max.text = m_max.ToString();
    }

    public void UpdateMaxScore()
    {
        m_max = leaderboard.highestScore;
        m_text_max.text = m_max.ToString();
    }



    // Update is called once per frame
    void Update()
    {
        
    }
    public void UpdateScore()
    {
        //score_list.Sort();
        //score_list.Reverse();
        //for (int i = 0; i < 5; i++)
        //{
        //    texts[i].text = score_list[i].ToString();
        //}
        UpdateMaxScore();
        leaderboard.playerName = nameInputField.text;
        leaderboard.playerScore = m_score;
        leaderboard.OnRegisterButtonClicked();
        //leaderboard.UpdateScore(m_score);
   
    }
    public void AddScore(int point)
    {
        m_score+=point;
        m_text_score.text = m_score.ToString();
        if(m_score >= m_max)
        {
            m_max = m_score;
        }
        m_text_max.text = m_max.ToString();
    }
    public void OpenObj(int i)
    {
        objs[i].SetActive(true); 
    }
    public void CloseObj(int i)
    {
        objs[i].SetActive(false);
    }
 
    public void ExitGame()
    {
        Application.Quit();
    }
  
    public void RestartGame()
    {
      SceneManager.LoadScene(0);
    }
    public void JumpScene(int i)
    {
      SceneManager.LoadScene(i);
    }

}
