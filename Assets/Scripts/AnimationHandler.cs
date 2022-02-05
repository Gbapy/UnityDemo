using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationHandler : DemoBase
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(AnimationProcessor());
    }

    private IEnumerator AnimationProcessor()
    {
        while(true)
        {
            yield return new WaitForSeconds(ANIM_DELAY);

            if (m_UiAnimations.Count == 0) continue;

            StartCoroutine(m_UiAnimations[0].Process(ANIM_DELAY));

            if(m_UiAnimations[0].m_HasToWait == true)
            {
                while (m_UiAnimations[0].isComplete == false)
                {
                    yield return new WaitForSeconds(ANIM_DELAY);
                }
            }

            m_UiAnimations.RemoveAt(0);
        }
    }
}
