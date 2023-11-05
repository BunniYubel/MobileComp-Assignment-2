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
            Debug.Log("User registered successfully");
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
            Debug.Log("Score uploaded successfully");
            // 上传成功后，刷新排行榜数据
            StartCoroutine(GetLeaderboardData());
        }
    }

    IEnumerator GetLeaderboardData()
    {
        string url = "https://octopus-app-6yuia.ondigitalocean.app/user/getTopUsers";
        UnityWebRequest www = UnityWebRequest.Get(url);
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log("Error: " + www.error);
        }
        else
        {
            string jsonResponse = www.downloadHandler.text;
            LeaderboardData leaderboardData = JsonUtility.FromJson<LeaderboardData>(jsonResponse);
            UpdateLeaderboardUI(leaderboardData.top_users);
        }
    }

    void UpdateLeaderboardUI(UserData[] topUsers)
    {
        for (int i = 0; i < topUsers.Length; i++)
        {
            if (i < usernameTexts.Length && i < scoreTexts.Length)
            {
                usernameTexts[i].text = $"{i + 1}. {topUsers[i].Username}"; // 更新用户名Text
                scoreTexts[i].text = topUsers[i].Score.ToString(); // 更新分数Text
            }
            else
            {
                break; // 如果Text组件数量少于排行榜数据量，退出循环
            }
        }
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
