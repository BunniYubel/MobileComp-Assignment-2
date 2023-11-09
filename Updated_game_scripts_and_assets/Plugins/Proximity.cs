using UnityEngine;

public class ProximitySensor : MonoBehaviour
{
    private Camera mainCamera; 

    void Start()
    {
        mainCamera = Camera.main;
    }

    void Update()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            using (AndroidJavaObject activity = new AndroidJavaClass("com.unity3d.player.UnityPlayer").GetStatic<AndroidJavaObject>("currentActivity"))
            using (AndroidJavaObject proximitySensor = new AndroidJavaObject("com.example.plugin.ProximitySensor", activity))
            {
                float proximityValue = proximitySensor.Call<float>("getProximityValue");
                AdjustCameraFOV(proximityValue);
            }
        }
    }

    void AdjustCameraFOV(float proximityValue)
    {
        if (mainCamera != null)
        {
            float newFOV = Mathf.Lerp(90, 60, proximityValue / 10);
            mainCamera.fieldOfView = newFOV;
        }
    }
}
