using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
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
    public Text finalScore = null;
    public GameObject retryButton = null;
    public GameObject titleButton = null;

    public GameObject curtain = null;

    private int selectedLevel = 0;
    // Start is called before the first frame update
    void Start()
    {
        splashWindow.SetActive(false);
        selectorWindow.SetActive(false);
        creditWindow.SetActive(false);
        endWindow.SetActive(false);

        m_UiBase = gameObject;
        m_Curtain = curtain;

        StartCoroutine(ShowSplash(delayOfSplash));
    }

    public void OnOptionButton1Clicked()
    {
        ButtonClickAnimation(optionButton1, gameObject, "HideCurrentWindow");
        selectedLevel = 0;
        InitGameScene(selectedLevel);
    }

    public void OnOptionButton2Clicked()
    {
        ButtonClickAnimation(optionButton2, gameObject, "HideCurrentWindow");
        selectedLevel = 1;
        InitGameScene(selectedLevel);
    }

    public void OnCreditButtonClicked()
    {
        ButtonClickAnimation(creditButton, gameObject, "SwitchToWindow", creditWindow);
    }

    public void OnBackButtonClicked()
    {
        ButtonClickAnimation(backButton, gameObject, "SwitchToWindow", selectorWindow);
    }
    
    public void OnRetryButtonClicked()
    {
        ButtonClickAnimation(retryButton, gameObject, "HideCurrentWindow");
        InitGameScene(selectedLevel);
    }

    public void OnTitleButtonClicked()
    {
        ButtonClickAnimation(titleButton, gameObject, "SwitchToWindow", selectorWindow);
    }

    /// <summary>
    /// Shows a splash screen.
    /// </summary>
    /// <param name="toLast"> A period that shows the spash screen. </param>
    protected IEnumerator ShowSplash(float delayForSplash)
    {
        ShowWindow(splashWindow);

        yield return new WaitForSeconds(delayForSplash);

        HideWindow(splashWindow);
        ShowWindow(selectorWindow);
    }

    private void ShowFinalWindow()
    {
        curtain.SetActive(true);

        finalScore.text = "Score: " + m_ScoreText.text;

        ShowWindow(endWindow);
    }
}
