using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ASceneTrans : MonoBehaviour
{
    //����һ��������
    public Slider LoadingSlider;
    //���������ı���������������ı����ϣ�
    public Text LoadText;
    //�����ַ���������д����������������
    public string SceneName;
    private float TargetVaule;
    private AsyncOperation async = null;

    void Start()
    {
        LoadText = GetComponent<Text>();
        LoadingSlider = FindObjectOfType<Slider>();
        StartCoroutine(AsyncLoading());
    }

    IEnumerator AsyncLoading()
    {
        //�첽���س���
        async = SceneManager.LoadSceneAsync(SceneName);
        //��ֹ����������Զ��л�
        async.allowSceneActivation = false;
        while (!async.isDone)
        {
            if (async.progress < 0.9f)
            {
                TargetVaule = async.progress;
            }
            else
            {
                TargetVaule = 1.0f;
            }
            LoadingSlider.value = TargetVaule;

            LoadText.text = (int)(LoadingSlider.value * 100) + "%";

            if (TargetVaule >= 0.9)
            {
                async.allowSceneActivation = true;
            }

            yield return null;

        }
    }
}