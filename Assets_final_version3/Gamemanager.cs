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
    leaderboard.playerName = nameInputField.text;
    leaderboard.playerScore = m_score;
    leaderboard.OnRegisterButtonClicked(); // 这里假设注册和登录是一步操作

    // 在这里调用新的方法来获取服务器上的最高分
    StartCoroutine(GetMaxScoreFromServer(leaderboard.playerName));
}

// 协程，从服务器获取最高分
IEnumerator GetMaxScoreFromServer(string username)
{
    string loginUrl = "https://octopus-app-6yuia.ondigitalocean.app/user/login";
    string jsonPayload = "{\"username\": \"" + username + "\"}";
    byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(jsonPayload);

    UnityWebRequest loginRequest = new UnityWebRequest(loginUrl, "POST");
    loginRequest.uploadHandler = (UploadHandler)new UploadHandlerRaw(bodyRaw);
    loginRequest.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
    loginRequest.SetRequestHeader("Content-Type", "application/json");

    yield return loginRequest.SendWebRequest();

    if (loginRequest.isNetworkError || loginRequest.isHttpError)
    {
        Debug.LogError("Error: " + loginRequest.error);
        // 可以在这里处理错误，例如提示用户
    }
    else
    {
        Debug.Log("User logged in successfully");
        LoginResponseData loginData = JsonUtility.FromJson<LoginResponseData>(loginRequest.downloadHandler.text);
        int serverMaxScore = loginData.score;

        // 比较本地最高分和服务器最高分
        m_max = Mathf.Max(m_max, serverMaxScore);
        m_text_max.text = m_max.ToString();

        // 如果服务器最高分高于本地最高分，可以选择上传新的最高分
        if (serverMaxScore > m_score)
        {
            leaderboard.UpdateScore(serverMaxScore);
        }
    }
}

[System.Serializable]
public class LoginResponseData
{
    public string message;
    public int score;
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
