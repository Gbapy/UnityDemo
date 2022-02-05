using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DemoCommon;

public class DemoBase : MonoBehaviour
{
    protected const float ANIM_DELAY = 0.01f;

    protected static float m_EnemySpeed = 1.0f;
    protected static int m_EnemyAmount = 20;
    protected static float m_MySpeed = 0.0f;

    protected static List<UiAnimation> m_UiAnimations = new List<UiAnimation>();

    protected GameObject m_CurrentUI = null;

    protected void ButtonClickAnimation(GameObject obj, GameObject target, string functionName, Object param)
    {
        m_UiAnimations.Add(new TwinkleAnimation(obj, 0, 0, true, true, true, 0.9f, 1.1f, 3.6f, target, functionName, param));
    }

    private void SwitchToWindow(GameObject uiObj)
    {
        if (m_CurrentUI == null || uiObj == null)
        {
            Debug.Log("Invalid inputs: SwitchToWindow(" + m_CurrentUI.ToString() + ", " + uiObj.ToString() + ")");
            return;
        }
        m_UiAnimations.Add(new ZoomAnimation(m_CurrentUI, 0, 0, true, false, true, 1.0f, 0.0f));
        m_UiAnimations.Add(new ZoomAnimation(uiObj, 0, 0, true, true, true, 0.0f, 1.0f));

        m_CurrentUI = uiObj;
    }
}
