using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Collections;
public class LeaderboardManager : MonoBehaviour
{
    public Text[] usernameTexts; // 用于显示用户名的Text组件数组
    public Text[] scoreTexts; // 用于显示分数的Text组件数组

    public string playerName; // 玩家的用户名
    public int playerScore; // 玩家的分数

    public int highestScore { get; private set; }
    public IEnumerator BeginGetLeaderboardData()
    {
        yield return StartCoroutine(GetLeaderboardData());
    }



    void Start()
    {
        StartCoroutine(GetLeaderboardData());
    }

    public void OnRegisterButtonClicked()
    {
        if (!string.IsNullOrEmpty(playerName))
        {
            StartCoroutine(RegisterUser(playerName));
        }
        else
        {
            Debug.LogError("Username cannot be empty!");
        }
    }

    IEnumerator RegisterUser(string playerName)
    {
        string registerUrl = "https://octopus-app-6yuia.ondigitalocean.app/user/register";
        string jsonPayload = "{\"username\": \"" + playerName + "\"}";
        byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(jsonPayload);

        UnityWebRequest registerRequest = new UnityWebRequest(registerUrl, "POST");
        registerRequest.uploadHandler = new UploadHandlerRaw(bodyRaw);
        registerRequest.downloadHandler = new DownloadHandlerBuffer();
        registerRequest.SetRequestHeader("Content-Type", "application/json");

        yield return registerRequest.SendWebRequest();

        if (registerRequest.isNetworkError || registerRequest.isHttpError)
        {
            Debug.LogError("Error: " + registerRequest.error);
        }
        else
        {
            //Debug.Log("User registered successfully");
            // 注册成功后，可以在这里处理其他逻辑，例如更新分数等
            UpdateScore(playerScore);
        }
    }

    public void UpdateScore(int newScore)
    {
        playerScore = newScore;
        StartCoroutine(UploadScoreToServer(playerName, playerScore));
    }

    IEnumerator UploadScoreToServer(string username, int score)
    {
        string url = "https://octopus-app-6yuia.ondigitalocean.app/user/updateScore";
        // 构建包含用户名和分数的JSON数据
        string jsonPayload = "{\"username\": \"" + username + "\", \"score\": " + score + "}";
        byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(jsonPayload);

        UnityWebRequest www = new UnityWebRequest(url, "PATCH");
        www.uploadHandler = new UploadHandlerRaw(bodyRaw);
        www.downloadHandler = new DownloadHandlerBuffer();
        www.SetRequestHeader("Content-Type", "application/json");

        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.LogError("Error: " + www.error);
        }
        else
        {
            //Debug.Log("Score uploaded successfully");
            // 上传成功后，刷新排行榜数据
            StartCoroutine(GetLeaderboardData());
        }
    }

    public IEnumerator GetLeaderboardData()
    {
        string url = "https://octopus-app-6yuia.ondigitalocean.app/user/getTopUsers";
        UnityWebRequest www = UnityWebRequest.Get(url);
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            //Debug.Log("Error: " + www.error);
        }
        else
        {
            string jsonResponse = www.downloadHandler.text;
            //Debug.Log("Received leaderboard data: " + jsonResponse);
            LeaderboardData leaderboardData = JsonUtility.FromJson<LeaderboardData>(jsonResponse);
            UpdateLeaderboardUI(leaderboardData.top_users);
        }
    }

    void UpdateLeaderboardUI(UserData[] topUsers)
    {
        highestScore = 0;
        //Debug.Log("UpdateLeaderboardUI called.");

        for (int i = 0; i < topUsers.Length; i++)
        {
            if (i < usernameTexts.Length && i < scoreTexts.Length)
            {
                usernameTexts[i].text = $"{topUsers[i].Username}";
                scoreTexts[i].text = topUsers[i].Score.ToString();
                //Debug.Log($"Rank {i + 1}: {topUsers[i].Username} - Score: {topUsers[i].Score}"); // 打印排名和分数

                if (i == 0)
                {
                    highestScore = topUsers[i].Score;
                    //Debug.Log($"New highest score is {highestScore} by user {topUsers[i].Username}"); // 打印最高分数和用户
                }
            }
            else
            {
                //Debug.LogWarning("Not enough UI Text elements to display all users."); // 如果UI元素不够，打印警告
                break;
            }
        }
    }


    public int GetHighestScore()
    {
        return highestScore;
    }

    [System.Serializable]
    public class LeaderboardData
    {
        public UserData[] top_users;
    }

    [System.Serializable]
    public class UserData
    {
        public string Username;
        public int Score;
    }
   
}
