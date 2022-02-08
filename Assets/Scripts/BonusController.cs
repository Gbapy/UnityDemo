using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DemoCommon;

public class BonusController : DemoBase
{
    public BonusEnum bonusKind = BonusEnum.None;
    
    // Start is called before the first frame update
    void Start()
    {
        transform.SetParent(m_GameScene.transform);

        gameObject.tag = bonusKind == BonusEnum.BulletBoost ? "BulletBoost" : "CanonBoost";
    }

    // Update is called once per frame
    void Update()
    {
        if (m_PauseFlag == true) return;

        if (m_StartFlag == false)
        {
            DestroyImmediate(gameObject);
            return;
        }

        /* Makes a movement. */
        transform.Rotate(new Vector3(0, 1, 0), Time.deltaTime * 360.0f);

        transform.localScale = new Vector3(m_ScaleFactor, m_ScaleFactor, m_ScaleFactor);

        transform.localPosition -= new Vector3(0, Time.deltaTime * 2.0f * m_ScaleFactor, 0);

        /* Validates a position for self destruction. */
        if (transform.localPosition.y <= -6.0f * m_ScaleFactor) Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag != "Player") return;

        if (bonusKind == BonusEnum.BulletBoost) m_BulletCullDown += 5.0f;

        if (bonusKind == BonusEnum.CanonBoost) m_CanonCullDown += 5.0f;

        Destroy(gameObject);
    }
}
