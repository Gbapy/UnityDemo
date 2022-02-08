using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A class for player, derived from DemoBase.
/// </summary>
public class PlayerController : DemoBase
{
    public GameObject bulletPrefab = null;      // A bullet prefab.

    private bool isTriggered = false;           // true, when the trigger has been pulled.

#if UNITU_IOS || UNITY_ANDROID
    private Vector2 touchPos = Vector2.zero;
#endif
    // Start is called before the first frame update
    void Start()
    {
        transform.localPosition = new Vector3(0, -4.5f * m_ScaleFactor, ENEMY_DEPTH);
        transform.localScale = new Vector3(m_ScaleFactor, m_ScaleFactor, m_ScaleFactor);

        StartCoroutine(SpawnBullet());          // Starts spawing the bullets.
        StartCoroutine(BulletCullDown());       // Starts culling down of Bullet-Boost effect.
        StartCoroutine(CanonCullDown());        // Starts culling down of Canon-Boost effect.
    }

    // Update is called once per frame
    void Update()
    {
        if (m_PauseFlag == true) return;
        if (m_StartFlag == false) return;

        /* Updates the position and scale of the player. */

        float xInput = 0;
        float yInput = 0;

#if UNITY_IOS || UNITY_ANDROID
        foreach (Touch tc in Input.touches)
        {
            if (tc.position.x > Screen.width * 0.6f)
            {
                yInput = 1.0f;
                break;
            }
        }

        foreach (Touch tc in Input.touches) {
            if(tc.position.x < Screen.width * 0.4f) {
                if (tc.phase == TouchPhase.Moved)
                {
                    xInput = Mathf.Sign(tc.position.x - touchPos.x);
                    touchPos = tc.position;
                    break;
                }else if(tc.phase == TouchPhase.Began)
                {
                    touchPos = tc.position;
                }
            }
        }
#else
        xInput = Input.GetAxis("Horizontal");
        yInput = Input.GetAxis("Vertical");
#endif

        float x = transform.localPosition.x;
        float y = -4.5f * m_ScaleFactor;
        float z = transform.localPosition.z;

        transform.localScale = new Vector3(m_ScaleFactor, m_ScaleFactor, m_ScaleFactor);

        x = transform.localPosition.x + xInput * m_SelectedLayout.playerSpeed * m_ScaleFactor * Time.deltaTime;

        x = Mathf.Clamp(x, -4.5f * m_ScaleFactor, 4.5f * m_ScaleFactor);

        transform.localPosition = new Vector3(x, y, z);

        if (yInput != 0) isTriggered = true; else isTriggered = false;
    }

    /// <summary>
    /// Spawns bullets.
    /// </summary>
    /// <returns></returns>
    protected IEnumerator SpawnBullet()
    {
        while (true)
        {
            yield return new WaitForSeconds(m_SelectedLayout.bulletInterval);

            if (m_PauseFlag == true) continue;          // When a pause state, ignore.
            if (m_StartFlag == false) continue;         // When the game play hasn't started, ignore.
            if (isTriggered == false) continue;         // When the trigger hasn't been pulled, ignore.

            /* Spawning a main bullet. */
            Instantiate(bulletPrefab, transform.position, Quaternion.identity, m_GameScene.transform);

            if (m_CanonCullDown > 0.0f)
            {
                /* If the canon bonus has been activated, extra spawning. */
                Instantiate(bulletPrefab, transform.position, Quaternion.AngleAxis(45.0f, new Vector3(0, 0, 1)), m_GameScene.transform);

                Instantiate(bulletPrefab, transform.position, Quaternion.AngleAxis(-45.0f, new Vector3(0, 0, 1)), m_GameScene.transform);
            }
        }
    }

    /// <summary>
    /// Culling down of a Bullet-Boost effect.
    /// </summary>
    /// <returns></returns>
    private IEnumerator BulletCullDown()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.04f);

            if (m_StartFlag == false) continue;
            if (m_PauseFlag == true) continue;

            if (m_BulletCullDown > 0) m_BulletCullDown -= 0.04f;

            if (m_BulletCullDown < 0.0f) m_BulletCullDown = 0.0f;
        }
    }

    /// <summary>
    /// Cuuling down of a Canon-Boost effect.
    /// </summary>
    /// <returns></returns>
    private static IEnumerator CanonCullDown()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.04f);

            if (m_StartFlag == false) continue;
            if (m_PauseFlag == true) continue;

            if (m_CanonCullDown > 0.0f) m_CanonCullDown -= 0.04f;

            if (m_CanonCullDown < 0.0f) m_CanonCullDown = 0.0f;
        }
    }
}
