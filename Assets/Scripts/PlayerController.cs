using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : DemoBase
{
    public GameObject bulletPrefab = null;

    private bool isTriggered = false;

    protected static float bulletSpeedCulldown = 0.0f;
    protected static float canonCullDown = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        transform.localPosition = new Vector3(0, -4.5f * m_ScaleFactor, ENEMY_DEPTH);
        transform.localScale = new Vector3(m_ScaleFactor, m_ScaleFactor, m_ScaleFactor);

        StartCoroutine(SpawnBullet());
        StartCoroutine(BulletCullDown());
        StartCoroutine(CanonCullDown());
    }

    // Update is called once per frame
    void Update()
    {
        if (m_PauseFlag == true) return;

        float x = transform.localPosition.x;
        float y = -4.5f * m_ScaleFactor;
        float z = transform.localPosition.z;
        float xDelta = Input.GetAxis("Horizontal");

        transform.localScale = new Vector3(m_ScaleFactor, m_ScaleFactor, m_ScaleFactor);

        x = transform.localPosition.x + xDelta * m_SelectedLayout.playerSpeed * m_ScaleFactor * Time.deltaTime;

        x = Mathf.Clamp(x, -4.5f * m_ScaleFactor, 4.5f * m_ScaleFactor);

        transform.localPosition = new Vector3(x, y, z);

        if (Input.GetAxis("Vertical") != 0) isTriggered = true; else isTriggered = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        switch(other.tag)
        {
            case "SpeedBoost":

                break;
            case "CanonBoost":

                break;
            default:

                break;
        }    
    }

    protected IEnumerator SpawnBullet()
    {
        while (true)
        {
            yield return new WaitForSeconds(m_SelectedLayout.bulletInterval);

            if (m_PauseFlag == true) continue;
            if (m_StartFlag == false) continue;
            if (isTriggered == false) continue;

            Instantiate(bulletPrefab, transform.position, Quaternion.identity, m_GameScene.transform);

            //if (canonCullDown > 0.0f)
            {
                Instantiate(bulletPrefab, transform.position, Quaternion.AngleAxis(45.0f, new Vector3(0, 0, 1)), m_GameScene.transform);

                Instantiate(bulletPrefab, transform.position, Quaternion.AngleAxis(-45.0f, new Vector3(0, 0, 1)), m_GameScene.transform);
            }
        }
    }


    private IEnumerator BulletCullDown()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.04f);

            if (m_StartFlag == false) continue;
            if (m_PauseFlag == true) continue;

            if (bulletSpeedCulldown > 0)
            {
                m_SelectedLayout.bulletSpeed = 30.0f;
                bulletSpeedCulldown -= 0.04f;
            }
            else
            {
                m_SelectedLayout.bulletSpeed = 15.0f;
            }

            if (bulletSpeedCulldown < 0.0f) bulletSpeedCulldown = 0.0f;
        }
    }

    private static IEnumerator CanonCullDown()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.04f);

            if (m_StartFlag == false) continue;
            if (m_PauseFlag == true) continue;

            if (canonCullDown > 0.0f) canonCullDown -= 0.04f;

            if (canonCullDown < 0.0f) canonCullDown = 0.0f;
        }
    }
}
