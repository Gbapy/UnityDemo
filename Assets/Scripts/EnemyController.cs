using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DemoCommon;

public class EnemyController : DemoBase
{
    public int health = 0;

    private bool isKilled = false;

    // Start is called before the first frame update
    void Start()
    {
        float xPos = Random.Range(-4.5f, 4.5f);

        transform.localPosition = new Vector3(xPos * m_ScaleFactor, 5.0f * m_ScaleFactor, ENEMY_DEPTH);

        gameObject.tag = "Enemy";
    }

    // Update is called once per frame
    void Update()
    {
        if (m_PauseFlag == true) return;
        if (isKilled == true) return;

        if(m_StartFlag == false) 
        {
            DestroyImmediate(gameObject);
            return;
        }

        transform.localPosition -= new Vector3(0, m_SelectedLayout.enemySpeed * m_ScaleFactor * Time.deltaTime, 0);
        this.transform.localScale = new Vector3(m_ScaleFactor, m_ScaleFactor, m_ScaleFactor);

        if (transform.localPosition.y <= -4.5f * m_ScaleFactor)
        {
            m_StartFlag = false;
            m_UiBase.SendMessage("ShowFinalWindow");
        }
    }

    private void SelfDestroy()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag != "Bullet") return;

        if (isKilled == true) return;

        float scale = transform.localScale.x;

        isKilled = true;

        m_Animations.Add(new TwinkleAnimation(gameObject, 0, 0, true, true, false, true, 0.7f * scale, 1.3f * scale, 3.6f, gameObject, "SelfDestroy"));

        AddScore(health);
    }
}
