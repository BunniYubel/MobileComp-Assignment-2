using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gyro_Script : MonoBehaviour
{
    public Button pauseMenu;
    //[SerializeField] Vector3 rot;
    
    // Start is called before the first frame update
    
    //private bool menuButtonPressed = false;
    private float initialRotationZ;

    void Start()
    {
        // Initialize the gyroscope
        Input.gyro.enabled = true;
        initialRotationZ = Input.gyro.rotationRateUnbiased.z;
    }

    void Update()
    {
        // Check if the Y-axis rotation has changed by 90 degrees
        float currentRotationZ = Input.gyro.rotationRateUnbiased.z;
        
        
        //for testing on unity 
        //float currentRotationY = rot.y;
        
        float rotationChangeZ = Mathf.Abs(currentRotationZ - initialRotationZ);
        // rotationChangeY >= 90.0f && !menuButtonPressed
        if (rotationChangeZ >= 5.0f || rotationChangeZ <= -5.0f)
        {
            // Trigger the menu button action
            TriggerMenuButton();
            //menuButtonPressed = true;
        }

        //menuButtonPressed = false;
    }

    // You can replace this method with your actual menu button action
    void TriggerMenuButton()
    {
        Debug.Log("Menu Button Pressed!");
        pauseMenu.onClick.Invoke();
    }

    //void TaskOnClick()
    //{
        
    //}
}
