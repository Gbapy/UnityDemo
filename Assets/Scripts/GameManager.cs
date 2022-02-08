using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DemoCommon;

/// <summary>
/// A game manager class.
/// </summary>
public class GameManager : DemoBase
{
    [Header("Game Scene")]
    public GameObject sceneBase = null;         // A root gameobject containing all gameplay objects.
    public GameObject background = null;        // A background object.
    public GameObject frame = null;             // A frame. 

    public TextMesh scoreText = null;           // A score object.

    [Header("Enemy Properties")]
    public GameObject[] enemyPrefabs = null;    // An enemy prefabs.

    [Header("Layouts Properties")]
    [SerializeField]
    public List<GameLayout> gameLayouts = new List<GameLayout>();

    private int widthBackup = 0;
    private int heightBackup = 0;

    /* A total number of enemies spawned. */
    private int enemySpawned = 0;

    // Start is called before the first frame update
    void Start()
    {
        m_StartFlag = false;

        /* Sets some global variables. */
        m_GameScene = sceneBase;

        m_GameLayouts = gameLayouts;

        m_Background = background;

        m_ScoreText = scoreText;

        /* Starts spawning enemies. */
        StartCoroutine(SpawnEnemy());
    }

    private void Update()
    {
        if (Screen.width == widthBackup && Screen.height == heightBackup) return;

        /*If screen size or aspect ratio has changed, relocates screen items. */
        widthBackup = Screen.width;
        heightBackup = Screen.height;

        AdjustGameSceneLayout();
    }

    /// <summary>
    /// Spawns enemies. 
    /// </summary>
    /// <returns></returns>
    IEnumerator SpawnEnemy()
    {
        while(true)
        {
            yield return new WaitForSeconds(m_SelectedLayout.enemyInterval);

            if (m_PauseFlag == true) continue;
            if (m_StartFlag == false) continue;
            if (enemySpawned > m_SelectedLayout.enemyAmount) continue;

            int idx = Random.Range(-1, 3);

            idx = Mathf.Clamp(idx, 0, 2);

            GameObject obj = GameObject.Instantiate(enemyPrefabs[idx], m_GameScene.transform);

            enemySpawned++;

            if (enemySpawned >= m_SelectedLayout.enemyAmount)
            {
                enemySpawned = 0;
                m_StartFlag = false;
                m_UiBase.SendMessage("ShowFinalWindow");
            }
        }
    }

    /// <summary>
    /// Adjusts the screen layout.
    /// </summary>
    private void AdjustGameSceneLayout()
    {
        int width = Screen.width;
        int height = Screen.height;

        bool isPortrait = width < height ? true : false;

        if (isPortrait)
        {
            m_ScaleFactor = (float)width / (float)height;

            float margin = (float)height * 0.5f - (float)width * 0.45f;
            float posY = ((float)height * 0.5f + (float)width * 0.45f) * 0.5f;
            float sF = width * 0.5f / (float)height;

            scoreText.transform.parent.localPosition = new Vector3(0, posY * 10.0f / (float)height, SCORE_DEPTH);
            scoreText.transform.parent.localScale = new Vector3(sF, sF, sF);
        }
        else
        {
            m_ScaleFactor = 1.0f;

            float margin = (float)width * 0.5f - (float)height * 0.45f;
            float posX = ((float)width * 0.5f + (float)height * 0.45f) * 0.5f;
            float sF = margin * 0.8f / (float)height;

            scoreText.transform.parent.localPosition = new Vector3(posX * 10.0f / (float)height, 0.0f, SCORE_DEPTH);
            scoreText.transform.parent.localScale = new Vector3(sF, sF, sF);
        }

        frame.transform.localScale = new Vector3(m_ScaleFactor * 3.0f, FRAME_DEPTH, m_ScaleFactor * 3.0f);
    }
}
