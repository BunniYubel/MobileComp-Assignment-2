using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;

using UnityEngine;
using UnityEngine.UI;

public class KroniiMovement : MonoBehaviour
{
    public Slider m_slider_hp;
    public float speed; // Adjust the movement speed as needed.
    public int life = 100;
    private bool moveRight;

    void Start()
    {
        speed = 4f;
        moveRight = true;
    }
    // Update is called once per frame
    void Update()
    {
        if (transform.position.x > 8f)
        {
            moveRight = false;
        }
        else if (transform.position.x < -8f)
        {
            moveRight = true;
        }

        if (moveRight)
        {
            transform.position = new Vector2(transform.position.x + speed * Time.deltaTime, transform.position.y);
        }
        else
        {
            transform.position = new Vector2(transform.position.x - speed * Time.deltaTime, transform.position.y);
        }
    }
    

}
