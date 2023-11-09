using System.Collections;

using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoad : MonoBehaviour
{
    public Slider LoadingSlider;
    public Text LoadText;
    public string SceneName;
    private float TargetVaule;
    private AsyncOperation async = null;

    void Start()
    {
        LoadingSlider = FindObjectOfType<Slider>();
        StartCoroutine(AsyncLoading());
    }

    IEnumerator AsyncLoading()
    {
        float timer = 0f;
        while (timer <= 1f) // 设置加载时间为10秒
        {
            timer += Time.deltaTime;
            float progress = Mathf.Clamp01(timer / 2f); // 计算加载进度（0到1之间）

            LoadingSlider.value = progress;
            yield return null;
        }

        // 当加载完成后，异步加载场景
        async = SceneManager.LoadSceneAsync(SceneName);
        async.allowSceneActivation = false;

        while (!async.isDone)
        {
            if (async.progress >= 0.9f) // 当加载进度大于等于0.9时，即加载完成时
            {
                TargetVaule = 1.0f;
                LoadingSlider.value = TargetVaule;
                async.allowSceneActivation = true; // 允许切换到新场景
            }
            else
            {
                TargetVaule = async.progress;
                LoadingSlider.value = TargetVaule;
            }

            yield return null;
        }
    }
}