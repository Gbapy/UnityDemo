using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : DemoBase
{
    public int damage = 1;

    private float scaleFactor = 1.0f;
    // Start is called before the first frame update
    void Start()
    {
        gameObject.tag = "Bullet";

        if (m_BulletCullDown > 0)
        {
            /* If Bullet-Boost effect has been activated. */
            damage = 3;
            scaleFactor = 1.5f;
        }

        transform.SetParent(m_GameScene.transform);
    }

    // Update is called once per frame
    void Update()
    {
        if (m_PauseFlag == true) return;

        if(m_StartFlag == false)
        {
            DestroyImmediate(gameObject);
            return;
        }

        /* Moves the bullet. */
        transform.localPosition += transform.up * m_SelectedLayout.bulletSpeed * m_ScaleFactor * Time.deltaTime;
        transform.localScale = new Vector3(m_ScaleFactor * 0.3f * scaleFactor, m_ScaleFactor * 0.3f * scaleFactor, m_ScaleFactor * 0.3f * scaleFactor);

        /* Validates the position for the self-destruction. */
        if (transform.localPosition.x < -5.0f * m_ScaleFactor || transform.localPosition.x > 5.0f * m_ScaleFactor)
        {
            Destroy(this.gameObject);
        }

        if(transform.localPosition.y > 5.0f * m_ScaleFactor)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Enemy")
        {
            /* If collides with enemy, self-destruction. */
            Destroy(gameObject);
        }
    }
}
