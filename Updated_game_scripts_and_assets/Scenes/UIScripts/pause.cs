using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pause : MonoBehaviour
{
    public GameObject Background;

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Pause()
    {
        Background.SetActive(true);
        Time.timeScale = 0;
    }

    public void Continue()
    {
        Background.SetActive(false);
        Time.timeScale = 1;
    }
}
