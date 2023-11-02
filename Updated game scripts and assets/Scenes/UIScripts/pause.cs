using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pause : MonoBehaviour
{
    public GameObject Panel;

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Pause()
    {
        Panel.SetActive(true);
        Time.timeScale = 0;
    }

    public void Continue()
    {
        Panel.SetActive(false);
        Time.timeScale = 1;
    }
}
