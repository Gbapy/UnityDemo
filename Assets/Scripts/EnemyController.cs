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

    /* Bonus prefabs. */
    public GameObject[] bonusPrefab = null;

    /* If yes, it has been kiiled, no more reaction needed. */
    private bool isKilled = false;

    /* A current health, can be reduced when being hit by bullets. */
    private int currentHealth = 0;

    /* A integer indicating if this enemy gives a bonus or not. */
    private int bonus = -1;

    // Start is called before the first frame update
    void Start()
    {
        /* An enemy launching position. */
        float xPos = Random.Range(-4.5f, 4.5f);
        int rand = Random.Range(0, 300);

        transform.localPosition = new Vector3(xPos * m_ScaleFactor, 5.0f * m_ScaleFactor, ENEMY_DEPTH);

        /* A tag that is used when collision with bullets. */
        gameObject.tag = "Enemy";

        currentHealth = health;

        if (rand > 250 && rand < 275) bonus = 0;
        if (rand >= 275 && rand < 300) bonus = 1;
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

        currentHealth -= other.gameObject.GetComponent<BulletController>().damage;

        if(currentHealth <= 0)
        {
            float scale = transform.localScale.x;

            isKilled = true;

            /* Kill animation. */
            m_Animations.Add(new TwinkleAnimation(gameObject, 0, 0, true, true, false, true, 0.7f * scale, 1.3f * scale, 10.8f));

            /* Adjusts a score according to the height of the enemy. */
            int toAdd = (int)((transform.localPosition.y + 4.5f * m_ScaleFactor) * (float)health * 2.0f / (9.0f * m_ScaleFactor) + health);

            /* Increases the score of the player. */
            AddScore(toAdd);

            if(bonus != -1)
            {
                /* Spwans a bonus. */
                GameObject.Instantiate(bonusPrefab[bonus], transform.position, Quaternion.identity);
            }
        }
    }
}
