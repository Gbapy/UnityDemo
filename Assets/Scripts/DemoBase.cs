using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DemoCommon;

/// <summary>
/// A base class containing all global data & process that are shared over the entire game scope.
/// </summary>
public class DemoBase : MonoBehaviour
{
    /* A delay for any animations. */
    protected const float ANIM_DELAY = 0.01f;

    /* Z value of the score text. */
    protected const float SCORE_DEPTH = 1.0f;

    /* Z value of the frame. */
    protected const float FRAME_DEPTH = 2.0f;

    /* Z value of the player and enemies. */
    protected const float ENEMY_DEPTH = 5.0f;

    /* A root object of the UI. */
    protected static GameObject m_UiBase = null;

    /* A curtain object that hides gameplay objects from UI. */
    protected static GameObject m_Curtain = null;

    /* A flag for pause state. */
    protected static bool m_PauseFlag = false;

    /* A flag representing if the gameplay has been started ot not. */
    protected static bool m_StartFlag = false;

    /* A selected game layout. */
    protected static GameLayout m_SelectedLayout = new GameLayout();

    /* A list of the launched aniamtions. */
    protected static List<UiAnimation> m_Animations = new List<UiAnimation>();

    /* A list of game layouts. */
    protected static List<GameLayout> m_GameLayouts = new List<GameLayout>();

    /* A root object containing all gameplay objects. */
    protected static GameObject m_GameScene = null;

    /* A background of the gameplay. */
    protected static GameObject m_Background = null;

    /* An textmesh object that shows score value. */
    protected static TextMesh m_ScoreText = null;

    /* A scale factor for the several screen sizes anf aspect ratios. */
    protected static float m_ScaleFactor = 1.0f;

    /* A current UI window. */
    private static GameObject m_CurrentUI = null;

    /* A score value. */
    private static int m_Score = 0;

    /// <summary>
    /// Launches an animation of the button clicking.
    /// </summary>
    /// <param name="obj"> An animated object. </param>
    /// <param name="target"> A target object that receives a message after this animation. </param>
    /// <param name="functionName"> A target function. </param>
    /// <param name="param"> A parameter for the target function. </param>
    protected void ButtonClickAnimation(GameObject toAnimate, GameObject target, string functionName = null, Object param = null)
    {
        m_Animations.Add(new TwinkleAnimation(toAnimate, 0, 0, true, true, true, false, 0.9f, 1.1f, 3.6f, target, functionName, param));
    }

    /// <summary>
    /// Switchs to a curtain UI window through animations.
    /// </summary>
    /// <param name="target"> A target window to switch to. </param>
    protected void SwitchToWindow(GameObject target)
    {
        HideCurrentWindow();        // Hides a current window.

        ShowWindow(target);         // Shows a target window.
    }

    /// <summary>
    /// Hides a current UI window through a zoom-out animation.
    /// </summary>
    protected void HideCurrentWindow()
    {
        HideWindow(m_CurrentUI);

        m_CurrentUI = null;
    }

    /// <summary>
    /// Shows a window through a zoom-in animation.
    /// </summary>
    /// <param name="uiObj"></param>
    protected void ShowWindow(GameObject uiObj)
    {
        if (uiObj != null) m_Animations.Add(new ZoomAnimation(uiObj, 0, 0, true, true, true, false, 0.0f, 1.0f));

        m_CurrentUI = uiObj;
    }

    /// <summary>
    /// Hides a window through a zoom-out animation.
    /// </summary>
    /// <param name="uiObj"></param>
    protected void HideWindow(GameObject uiObj)
    {
        if(uiObj != null) m_Animations.Add(new ZoomAnimation(uiObj, 0, 0, true, false, true, false, 1.0f, 0.0f));
    }

    /// <summary>
    /// Initialization of a game scene.
    /// </summary>
    /// <param name="layout"> An index of game layout. </param>
    protected void InitGameScene(int layout)
    {
        /* Hides the curtain to show gameplay objects. */
        m_Curtain.SetActive(false);

        m_SelectedLayout = m_GameLayouts[layout];

        m_Background.GetComponent<MeshRenderer>().material = m_SelectedLayout.layout;

        AddScore(-1);

        m_StartFlag = true;
        m_PauseFlag = false;
    }

    /// <summary>
    /// Adds and show the score.
    /// </summary>
    /// <param name="toAdd"></param>
    protected void AddScore(int toAdd)
    {
        if (toAdd < 0)
        {
            m_Score = 0;
        }
        else{
            m_Score += toAdd;
        }

        m_ScoreText.text = m_Score.ToString();
    }

    /// <summary>
    /// Processes all animations.
    /// </summary>
    /// <returns></returns>
    protected IEnumerator AnimationProcessor()
    {
        while (true)
        {
            yield return new WaitForSeconds(ANIM_DELAY);

            if (m_Animations.Count == 0) continue;

            StartCoroutine(m_Animations[0].Process(ANIM_DELAY));

            if (m_Animations[0].m_HasToWait == true)
            {
                while (m_Animations[0].isComplete == false)
                {
                    yield return new WaitForSeconds(ANIM_DELAY);
                }
            }

            m_Animations.RemoveAt(0);
        }
    }
}
