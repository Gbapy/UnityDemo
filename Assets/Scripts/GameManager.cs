using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DemoCommon;

public class GameManager : DemoBase
{
    [Header("Game Scene")]
    public GameObject sceneBase = null;
    public GameObject background = null;
    public GameObject frame = null;

    public TextMesh scoreText = null;

    [Header("Enemy Properties")]
    public GameObject[] enemyPrefabs = null;

    [Header("Layouts Properties")]
    [SerializeField]
    public List<GameLayout> gameLayouts = new List<GameLayout>();

    private int widthBackup = 0;
    private int heightBackup = 0;

    // Start is called before the first frame update
    void Start()
    {
        m_StartFlag = false;

        m_GameScene = sceneBase;

        m_GameLayouts = gameLayouts;

        m_Background = background;

        m_ScoreText = scoreText;

        StartCoroutine(SpawnEnemy());
    }

    private void Update()
    {
        if (Screen.width == widthBackup && Screen.height == heightBackup) return;

        widthBackup = Screen.width;
        heightBackup = Screen.height;

        AdjustGameSceneLayout();
    }

    IEnumerator SpawnEnemy()
    {
        while(true)
        {
            yield return new WaitForSeconds(m_SelectedLayout.enemyInterval);

            if (m_PauseFlag == true) continue;
            if (m_StartFlag == false) continue;

            int idx = Random.Range(-1, 3);

            idx = Mathf.Clamp(idx, 0, 2);

            GameObject obj = GameObject.Instantiate(enemyPrefabs[idx], m_GameScene.transform);
        }
    }

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
