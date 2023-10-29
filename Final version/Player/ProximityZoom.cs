using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProximityZoom : MonoBehaviour
{
    private AndroidJavaObject proximitySensor;
    private float zoomSpeed = 0.1f; // Adjust this value to control the zoom speed.
    private float minZoom = 1.0f;  // Minimum orthographic size.
    private float maxZoom = 10.0f; // Maximum orthographic size.
    private Camera mainCamera;
    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;

        AndroidJavaObject sensorManager = new AndroidJavaObject("android.hardware.SensorManager");
        proximitySensor = sensorManager.Call<AndroidJavaObject>("getDefaultSensor", 8); // Sensor.TYPE_PROXIMITY
    }

    // Update is called once per frame
    void Update()
    {
        if (proximitySensor != null)
        {
            float distance = proximitySensor.Call<float>("getMaximumRange");
            float proximity = proximitySensor.Call<float>("getMaximumRange");

            // Map proximity values to the zoom level within your desired range.
            float newZoom = Mathf.Lerp(minZoom, maxZoom, proximity / distance);

            // Smoothly interpolate the current zoom level to the new one.
            mainCamera.orthographicSize = Mathf.Lerp(mainCamera.orthographicSize, newZoom, zoomSpeed * Time.deltaTime);
        }
    }
}
