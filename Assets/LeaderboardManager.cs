using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Collections;

public class LeaderboardManager : MonoBehaviour
{
    public Text[] usernameTexts; // ������ʾ�û�����Text�������
    public Text[] scoreTexts; // ������ʾ������Text�������

    public string playerName; // ��ҵ��û���
    public int playerScore; // ��ҵķ���

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
            // ע��ɹ��󣬿��������ﴦ�������߼���������·�����
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
        // ���������û����ͷ�����JSON����
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
            // �ϴ��ɹ���ˢ�����а�����
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
                usernameTexts[i].text = $"{i + 1}. {topUsers[i].Username}"; // �����û���Text
                scoreTexts[i].text = topUsers[i].Score.ToString(); // ���·���Text
            }
            else
            {
                break; // ���Text��������������а����������˳�ѭ��
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