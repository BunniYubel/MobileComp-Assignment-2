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

            // ��ӡԭʼ RMS ֵ���ڵ���
            //Debug.Log("RMS Value: " + rmsValue);

            float bulletSpeed = Mathf.Clamp(baseBulletSpeed + rmsValue * microphoneSensitivity, baseBulletSpeed, maxBulletSpeed);

            //��ӡ��������ӵ��ٶ�
            //Debug.Log("Bullet Speed: " + bulletSpeed);

            // �Ƴ��� NewBulletScript.Instance ������
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
