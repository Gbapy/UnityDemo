using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DemoCommon
{
    /// <summary>
    /// A base class for several animation.
    /// </summary>
    public abstract class UiAnimation
    {
        /* When this animation has been completed, true. */
        public bool isComplete = false;

        /* When it is true, needs to wait for this animation to be completed. */
        public bool m_HasToWait = true;

        /* When it's true, m_Object will be destroyed. */
        public bool m_hasToDestroy = false;

        /* The main object that has to animate. */
        protected GameObject m_Object;

        /* If it is true, then the object has to be shown before aniamtion. Otherwise, needs to be hidden. */
        private bool m_PreShow = false;

        /* If it is true, then the object has to be shown after aniamtion. */
        private bool m_PostShow = false;

        /* A delay before an animation. */
        private float m_PreDelay = 0.0f;

        /* A delay after an animation. */
        private float m_PostDelay = 0.0f;

        /* A target object that receives a massage after this animation has been completed. */
        private GameObject m_target = null;

        /* A target function that receives a massage after this animation has been completed. */
        private string m_FunctionName = null;

        /* A parameter that will pass in the target function. */
        private UnityEngine.Object m_Param = null;

        /* A contructor. */
        public UiAnimation(GameObject obj, float preDelay = 0.0f, float postDelay = 0.0f,
            bool preShow = true, bool postShow = true, GameObject target = null, 
            string funcName = null, UnityEngine.Object param = null, bool hasToWait = true, bool hasToDestroy = false)
        {
            m_Object = obj;
            m_PreDelay = preDelay;
            m_PostDelay = postDelay;
            m_PreShow = preShow;
            m_PostShow = postShow;
            m_HasToWait = hasToWait;
            m_hasToDestroy = hasToDestroy;
            m_target = target;
            m_FunctionName = funcName;
            m_Param = param;
        }

        /* An abstract function for customized animations of the derived class instances. */
        protected abstract void PlayAnimation(int frameIndex);

        /* An abstract function for customized final transforms of the derived class instances. */
        protected abstract void SetFinalTransform();

        /// <summary>
        /// A processing of animation.
        /// </summary>
        /// <param name="animDelay"></param>
        /// <returns></returns>
        public IEnumerator Process(float animDelay)
        {
            /* Waits for preDelay. */
            yield return new WaitForSeconds(m_PreDelay);

            /* Pre process. */
            PreProcess();

            /* Real animation. */
            for (int i = 0; i < 100; i += 5)
            {
                PlayAnimation(i);
                yield return new WaitForSeconds(animDelay);
            }

            SetFinalTransform();

            yield return new WaitForSeconds(m_PostDelay);

            /* Post process. */
            PostProces();
        }

        /// <summary>
        /// A process that has to be done before the animation.
        /// </summary>
        private void PreProcess()
        {
            if (m_PreShow)
                m_Object.SetActive(true);
            else
                m_Object.SetActive(false);
        }

        /// <summary>
        /// A process after the animtion.
        /// </summary>
        private void PostProces()
        {
            if (m_PostShow)
                m_Object.SetActive(true);
            else
                m_Object.SetActive(false);

            /* Sends a message with a parameter. */
            if (m_target != null && m_FunctionName != null)
            {
                m_target.SendMessage(m_FunctionName, m_Param);
            }

            /* If needed, destroy. */
            if (m_hasToDestroy == true) UnityEngine.GameObject.Destroy(m_Object);

            isComplete = true;
        }
    }

    /// <summary>
    /// A class for a zoom-in or zoon-out animation.
    /// </summary>
    public class ZoomAnimation : UiAnimation
    {
        private float m_Min = 0.0f;
        private float m_Max = 1.0f;

        private float m_frameStep = 0.0f;

        public ZoomAnimation(GameObject obj, float preDelay = 0.0f, float postDelay = 0.0f,
            bool preShow = true, bool postShow = true, bool hasToWait = true, bool hasToDestroy = false, float min = 0.0f, float max = 1.0f,
            GameObject target = null, string funcName = null, UnityEngine.Object param = null)
            : base(obj, preDelay, postDelay, preShow, postShow, target, funcName, param, hasToWait, hasToDestroy)
        {
            m_Min = min;
            m_Max = max;

            m_frameStep = (m_Max - m_Min) / 100.0f;
        }

        protected override void PlayAnimation(int frameIndex)
        {
            float s = m_Min + (float)frameIndex * m_frameStep;
            m_Object.transform.localScale = new Vector3(s, s, s);
        }

        protected override void SetFinalTransform()
        {
            m_Object.transform.localScale = new Vector3(m_Max, m_Max, m_Max);
        }
    }

    /// <summary>
    /// A class for a twinkling animation.
    /// </summary>
    public class TwinkleAnimation : UiAnimation
    {
        private float m_Min = 0.0f;
        private float m_Max = 1.0f;

        private float m_frameStep = 0.0f;

        private float m_ScaleRange = 0.0f;
        public TwinkleAnimation(GameObject obj, float preDelay = 0.0f, float postDelay = 0.0f,
            bool preShow = true, bool postShow = true, bool hasToWait = true, bool hasToDestroy = false, float min = 0.0f, float max = 1.0f, float rate = 3.6f,
            GameObject target = null, string funcName = null, UnityEngine.Object param = null)
            : base(obj, preDelay, postDelay, preShow, postShow, target, funcName, param, hasToWait, hasToDestroy)
        {
            m_Min = min;
            m_Max = max;

            m_ScaleRange = (m_Max - m_Min) * 0.5f;

            m_frameStep = rate;
        }
        
        protected override void PlayAnimation(int frameIndex)
        {
            float s = (m_Min + m_Max) * 0.5f + m_ScaleRange * Mathf.Cos((float)frameIndex * m_frameStep * Mathf.PI / 180.0f);
            m_Object.transform.localScale = new Vector3(s, s, s);
        }

        protected override void SetFinalTransform()
        {
            m_Object.transform.localScale = Vector3.one;
        }
    }

    /// <summary>
    /// A class for the layouts of the game play.
    /// </summary>
    [Serializable]
    public class GameLayout
    {
        public float playerSpeed = 7.0f;

        public float bulletSpeed = 15.0f;
        public float bulletInterval = 0.1f;

        public int enemyAmount = 200;
        public float enemySpeed = 5.0f;
        public float enemyInterval = 2.0f;

        public Material layout = null;
    }
}
