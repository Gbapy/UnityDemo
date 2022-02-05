using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DemoCommon;

public class UIManager : DemoBase
{
    public float delayOfSplash = 3.0f;

    [Header("Windows")]
    public GameObject splashWindow = null;
    public GameObject selectorWindow = null;
    public GameObject creditWindow = null;
    public GameObject endWindow = null;

    [Header("Selector Window")]
    public GameObject creditButton = null;
    public GameObject optionButton1 = null;
    public GameObject optionButton2 = null;

    [Header("Credit Window")]
    public GameObject backButton = null;

    [Header("Final Window")]
    public GameObject retryButton = null;
    public GameObject titleButton = null;

    // Start is called before the first frame update
    void Start()
    {
        splashWindow.SetActive(false);
        selectorWindow.SetActive(false);
        creditWindow.SetActive(false);
        endWindow.SetActive(false);

        ShowSplash(delayOfSplash);
    }

    public void OnOptionButton1Clicked()
    {
        ButtonClickAnimation(optionButton1, this.gameObject, "SwitchToWindow", creditWindow);
    }

    public void OnOptionButton2Clicked()
    {
        ButtonClickAnimation(optionButton2, this.gameObject, "SwitchToWindow", creditWindow);
    }

    public void OnCreditButtonClicked()
    {
        ButtonClickAnimation(creditButton, this.gameObject, "SwitchToWindow", creditWindow);
    }

    public void OnBackButtonClicked()
    {
        ButtonClickAnimation(backButton, this.gameObject, "SwitchToWindow", selectorWindow);
    }
    
    public void OnRetryButtonClicked()
    {

    }

    public void OnTitleButtonClicked()
    {
        ButtonClickAnimation(titleButton, this.gameObject, "SwitchToWindow", selectorWindow);
    }
    /// <summary>
    /// Shows a splash screen.
    /// </summary>
    /// <param name="toLast"> A period that shows the spash screen. </param>
    protected void ShowSplash(float delayForSplash)
    {
        m_UiAnimations.Add(new ZoomAnimation(splashWindow, 0, delayForSplash));
        m_UiAnimations.Add(new ZoomAnimation(splashWindow, 0, 0, true, false, true, 1.0f, 0.0f));
        m_UiAnimations.Add(new ZoomAnimation(selectorWindow, 0, 0));

        m_CurrentUI = selectorWindow;
    }
}
