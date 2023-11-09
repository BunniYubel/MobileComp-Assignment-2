using UnityEngine;

public class MicrophoneInput : MonoBehaviour
{
    public float baseBulletSpeed = 1f;
    public float maxBulletSpeed = 20f;
    public float microphoneSensitivity = 0.03f;

    private string micDeviceName;
    private AudioClip microphoneInput;
    private bool isMicInitialized = false;

    void Start()
    {
        if (Microphone.devices.Length > 0)
        {
            micDeviceName = Microphone.devices[0];
            microphoneInput = Microphone.Start(micDeviceName, true, 1, 44100);
        }
        else
        {
            Debug.LogError("No microphone devices found");
        }
    }


    void Update()
    {
        if (microphoneInput != null)
        {
            float[] samples = new float[128];
            microphoneInput.GetData(samples, 0);
            float rmsValue = CalculateRMS(samples);

            // 打印原始 RMS 值用于调试
            //Debug.Log("RMS Value: " + rmsValue);

            float bulletSpeed = Mathf.Clamp(baseBulletSpeed + rmsValue * microphoneSensitivity, baseBulletSpeed, maxBulletSpeed);

            //打印计算出的子弹速度
            //Debug.Log("Bullet Speed: " + bulletSpeed);

            // 移除对 NewBulletScript.Instance 的引用
            // NewBulletScript.Instance.SetBulletSpeed(bulletSpeed);
        }
    }

    public float GetCurrentBulletSpeed()
    {
        float[] samples = new float[128];
        microphoneInput.GetData(samples, 0);
        float rmsValue = CalculateRMS(samples);
        return Mathf.Clamp(baseBulletSpeed + rmsValue * microphoneSensitivity, baseBulletSpeed, maxBulletSpeed);
    }


    float CalculateRMS(float[] samples)
    {
        float sum = 0f;
        for (int i = 0; i < samples.Length; i++)
        {
            sum += samples[i] * samples[i];
        }
        return Mathf.Sqrt(sum / samples.Length) * 10000;
    }
}
