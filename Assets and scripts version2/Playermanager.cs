using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Playermanager : MonoBehaviour
{
    public int m_life = 100;
    public Slider m_slider_hp;
    public GameObject m_explosionFX;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag=="Bullet")
        {
            KroniiBulletScript kronibullet = collision.gameObject.GetComponent<KroniiBulletScript>();
            if(kronibullet != null )
            {
                m_life -= kronibullet.power;
                m_slider_hp.value -= kronibullet.power; 
                if(m_life <= 0)
                {
                    Instantiate(m_explosionFX, transform.position, Quaternion.identity);
                    for (int i = 0; i < 5; i++)
                    {
                        if (Gamemanager.score_list[i] == 0)
                        {
                            Gamemanager.score_list[i]=Gamemanager.Instance.m_score;
                            break;
                        }
                    }
             
                    Gamemanager.Instance.objs[2].SetActive(true);
                    Destroy(this.gameObject);
                }
            }
        }
    }
}
