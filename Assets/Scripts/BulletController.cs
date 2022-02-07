using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : DemoBase
{
    // Start is called before the first frame update
    void Start()
    {
        gameObject.tag = "Bullet";

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

        transform.localPosition += transform.up * m_SelectedLayout.bulletSpeed * m_ScaleFactor * Time.deltaTime;
        transform.localScale = new Vector3(m_ScaleFactor * 0.3f, m_ScaleFactor * 0.3f, m_ScaleFactor * 0.3f);

        if (transform.localPosition.x < -5.0f * m_ScaleFactor || transform.localPosition.x > 5.0f * m_ScaleFactor)
        {
            Destroy(this.gameObject);
        }

        if(transform.localPosition.y > 5.0f * m_ScaleFactor)
        {
            Destroy(this.gameObject);
        }
    }
}
