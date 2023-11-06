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
        while (timer <= 1f) // ���ü���ʱ��Ϊ10��
        {
            timer += Time.deltaTime;
            float progress = Mathf.Clamp01(timer / 2f); // ������ؽ��ȣ�0��1֮�䣩

            LoadingSlider.value = progress;
            yield return null;
        }

        // ��������ɺ��첽���س���
        async = SceneManager.LoadSceneAsync(SceneName);
        async.allowSceneActivation = false;

        while (!async.isDone)
        {
            if (async.progress >= 0.9f) // �����ؽ��ȴ��ڵ���0.9ʱ�����������ʱ
            {
                TargetVaule = 1.0f;
                LoadingSlider.value = TargetVaule;
                async.allowSceneActivation = true; // �����л����³���
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