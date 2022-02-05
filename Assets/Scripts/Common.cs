using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DemoCommon
{
    public abstract class UiAnimation
    {
        public bool isComplete = false;

        public bool m_HasToWait = true;

        protected GameObject m_uiObject;

        private bool m_PreShow = false;

        private bool m_PostShow = false;

        private float m_PreDelay = 0.0f;
        private float m_PostDelay = 0.0f;

        private GameObject m_target = null;

        private string m_FunctionName = null;

        private Object m_Param = null;
        public UiAnimation(GameObject obj, float preDelay = 0.0f, float postDelay = 0.0f,
            bool preShow = true, bool postShow = true, GameObject target = null, string funcName = null, Object param = null, bool hasToWait = true)
        {
            m_uiObject = obj;
            m_PreDelay = preDelay;
            m_PostDelay = postDelay;
            m_PreShow = preShow;
            m_PostShow = postShow;
            m_HasToWait = hasToWait;
            m_target = target;
            m_FunctionName = funcName;
            m_Param = param;
        }

        protected abstract void PlayAnimation(int frameIndex);

        protected abstract void SetFinalTransform();

        public IEnumerator Process(float animDelay)
        {
            yield return new WaitForSeconds(m_PreDelay);

            PreProcess();

            for (int i = 0; i < 100; i += 5)
            {
                PlayAnimation(i);
                yield return new WaitForSeconds(animDelay);
            }

            SetFinalTransform();

            yield return new WaitForSeconds(m_PostDelay);

            PostProces();
        }

        private void PreProcess()
        {
            if (m_PreShow)
                m_uiObject.SetActive(true);
            else
                m_uiObject.SetActive(false);
        }

        private void PostProces()
        {
            if (m_PostShow)
                m_uiObject.SetActive(true);
            else
                m_uiObject.SetActive(false);

            if (m_target != null && m_FunctionName != null)
            {
                m_target.SendMessage(m_FunctionName, m_Param);
            }

            isComplete = true;
        }
    }

    public class ZoomAnimation : UiAnimation
    {
        private float m_Min = 0.0f;
        private float m_Max = 1.0f;

        private float m_frameStep = 0.0f;

        public ZoomAnimation(GameObject obj, float preDelay = 0.0f, float postDelay = 0.0f,
            bool preShow = true, bool postShow = true, bool hasToWait = true, float min = 0.0f, float max = 1.0f,
            GameObject target = null, string funcName = null)
            : base(obj, preDelay, postDelay, preShow, postShow, target, funcName)
        {
            m_Min = min;
            m_Max = max;

            m_frameStep = (m_Max - m_Min) / 100.0f;
        }

        protected override void PlayAnimation(int frameIndex)
        {
            float s = m_Min + (float)frameIndex * m_frameStep;
            m_uiObject.transform.localScale = new Vector3(s, s, s);
        }

        protected override void SetFinalTransform()
        {
            m_uiObject.transform.localScale = new Vector3(m_Max, m_Max, m_Max);
        }
    }

    public class TwinkleAnimation : UiAnimation
    {
        private float m_Min = 0.0f;
        private float m_Max = 1.0f;

        private float m_frameStep = 0.0f;

        private float m_ScaleRange = 0.0f;
        public TwinkleAnimation(GameObject obj, float preDelay = 0.0f, float postDelay = 0.0f,
            bool preShow = true, bool postShow = true, bool hasToWait = true, float min = 0.0f, float max = 1.0f, float rate = 3.6f,
            GameObject target = null, string funcName = null, Object param = null)
            : base(obj, preDelay, postDelay, preShow, postShow, target, funcName, param)
        {
            m_Min = min;
            m_Max = max;

            m_ScaleRange = (m_Max - m_Min) * 0.5f;

            m_frameStep = rate;
        }
        
        protected override void PlayAnimation(int frameIndex)
        {
            float s = (m_Min + m_Max) * 0.5f + m_ScaleRange * Mathf.Cos((float)frameIndex * m_frameStep * Mathf.PI / 180.0f);
            m_uiObject.transform.localScale = new Vector3(s, s, s);
        }

        protected override void SetFinalTransform()
        {
            m_uiObject.transform.localScale = Vector3.one;
        }
    }
}
