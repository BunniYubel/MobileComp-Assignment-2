using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class DeleteBullet : MonoBehaviour
{
    public float TimeToLive = 5f;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, TimeToLive);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
