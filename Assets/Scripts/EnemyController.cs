using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DemoCommon;

/// <summary>
/// A class for enemies, derived from DemoBase.
/// </summary>
public class EnemyController : DemoBase
{
    /* A health of this enemy. */
    public int health = 0;

    /* If yes, it has been kiiled, no more reaction needed. */
    private bool isKilled = false;

    // Start is called before the first frame update
    void Start()
    {
        /* An enemy launching position. */
        float xPos = Random.Range(-4.5f, 4.5f);

        transform.localPosition = new Vector3(xPos * m_ScaleFactor, 5.0f * m_ScaleFactor, ENEMY_DEPTH);

        /* A tag that is used when collision with bullets. */
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

        /* Uptetes the position & scale. */
        transform.localPosition -= new Vector3(0, m_SelectedLayout.enemySpeed * m_ScaleFactor * Time.deltaTime, 0);
        this.transform.localScale = new Vector3(m_ScaleFactor, m_ScaleFactor, m_ScaleFactor);

        /* Validates the position to judge the ending of the game. */
        if (transform.localPosition.y <= -4.5f * m_ScaleFactor)
        {
            m_StartFlag = false;
            m_UiBase.SendMessage("ShowFinalWindow");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag != "Bullet") return;

        if (isKilled == true) return;

        float scale = transform.localScale.x;

        isKilled = true;

        m_Animations.Add(new TwinkleAnimation(gameObject, 0, 0, true, true, false, true, 0.7f * scale, 1.3f * scale, 3.6f));

        AddScore(health);
    }
}
