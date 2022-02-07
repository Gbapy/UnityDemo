using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DemoCommon;

public class DemoBase : MonoBehaviour
{
    protected const float ANIM_DELAY = 0.01f;
    protected const float SCORE_DEPTH = 1.0f;
    protected const float FRAME_DEPTH = 2.0f;
    protected const float ENEMY_DEPTH = 5.0f;

    protected static GameObject m_UiBase = null;
    protected static GameObject m_Curtain = null;

    protected static bool m_PauseFlag = false;
    protected static bool m_StartFlag = false;

    protected static GameLayout m_SelectedLayout = new GameLayout();

    protected static List<UiAnimation> m_Animations = new List<UiAnimation>();
    protected static List<GameLayout> m_GameLayouts = new List<GameLayout>();

    protected static GameObject m_GameScene = null;
    protected static GameObject m_Background = null;

    protected static TextMesh m_ScoreText = null;

    protected static float m_ScaleFactor = 1.0f;

    private static GameObject m_CurrentUI = null;

    private static int m_Score = 0;

    protected void ButtonClickAnimation(GameObject obj, GameObject target, string functionName = null, Object param = null)
    {
        m_Animations.Add(new TwinkleAnimation(obj, 0, 0, true, true, true, false, 0.9f, 1.1f, 3.6f, target, functionName, param));
    }

    protected void SwitchToWindow(GameObject uiObj)
    {
        HideCurrentWindow();

        ShowWindow(uiObj);
    }

    protected void HideCurrentWindow()
    {
        HideWindow(m_CurrentUI);

        m_CurrentUI = null;
    }

    protected void ShowWindow(GameObject uiObj)
    {
        if (uiObj != null) m_Animations.Add(new ZoomAnimation(uiObj, 0, 0, true, true, true, false, 0.0f, 1.0f));

        m_CurrentUI = uiObj;
    }

    protected void HideWindow(GameObject uiObj)
    {
        if(uiObj != null) m_Animations.Add(new ZoomAnimation(uiObj, 0, 0, true, false, true, false, 1.0f, 0.0f));
    }

    protected void InitGameScene(int layout)
    {
        m_Curtain.SetActive(false);

        m_SelectedLayout = m_GameLayouts[layout];

        m_Background.GetComponent<MeshRenderer>().material = m_SelectedLayout.layout;

        AddScore(-1);

        m_StartFlag = true;
        m_PauseFlag = false;
    }
    
    protected void AddScore(int score)
    {
        if (score < 0)
        {
            m_Score = 0;
        }
        else{
            m_Score += score;
        }

        m_ScoreText.text = m_Score.ToString();
    }
}
