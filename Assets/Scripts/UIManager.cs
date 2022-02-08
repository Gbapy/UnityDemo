using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DemoCommon;

/// <summary>
/// A class for UI, derived from DemoBase.
/// </summary>
public class UIManager : DemoBase
{
    public float delayOfSplash = 3.0f;          // Delay for a splash window.

    [Header("Windows")]
    public GameObject splashWindow = null;      // A slash window.
    public GameObject selectorWindow = null;    // A selector window.
    public GameObject creditWindow = null;      // A credit window.
    public GameObject endWindow = null;         // A final window that shows the score.

    /* UI buttons on selector window. */
    [Header("Selector Window")]
    public GameObject creditButton = null;
    public GameObject optionButton1 = null;
    public GameObject optionButton2 = null;

    /* Buttons on credit window. */
    [Header("Credit Window")]
    public GameObject backButton = null;

    /* Buttons on the final window. */
    [Header("Final Window")]
    public Text finalScore = null;
    public GameObject retryButton = null;
    public GameObject titleButton = null;

    /* A curtain object that hide gameplay objects from UI. */
    public GameObject UiCurtain = null;

    /* A selected level, used when retrying. */
    private int selectedLevel = 0;
    // Start is called before the first frame update
    void Start()
    {
        /* Makes all UI windows inactive. */
        splashWindow.SetActive(false);
        selectorWindow.SetActive(false);
        creditWindow.SetActive(false);
        endWindow.SetActive(false);

        /* Sets some UI as global variables. */
        m_UiBase = gameObject;
        m_UiCurtain = UiCurtain;

        /* Shows the splash window thoough animation. */
        StartCoroutine(AnimationProcessor());
        StartCoroutine(ShowSplash(delayOfSplash));
    }

    /// <summary>
    /// An event handler of the button '1' on the selector window.
    /// </summary>
    public void OnOptionButton1Clicked()
    {
        ButtonClickAnimation(optionButton1, gameObject, "HideCurrentWindow");
        selectedLevel = 0;
        InitGameScene(selectedLevel);
    }

    /// <summary>
    /// An event handler of the button '2' on the selector window.
    /// </summary>
    public void OnOptionButton2Clicked()
    {
        ButtonClickAnimation(optionButton2, gameObject, "HideCurrentWindow");
        selectedLevel = 1;
        InitGameScene(selectedLevel);
    }

    /// <summary>
    /// An event handler of the "Credit' button on the selector window.
    /// </summary>
    public void OnCreditButtonClicked()
    {
        ButtonClickAnimation(creditButton, gameObject, "SwitchToWindow", creditWindow);
    }

    /// <summary>
    /// An event handler of the 'Back' button on the credit window.
    /// </summary>
    public void OnBackButtonClicked()
    {
        ButtonClickAnimation(backButton, gameObject, "SwitchToWindow", selectorWindow);
    }
    
    /// <summary>
    /// An event handler of the 'Retry' button on the score window.
    /// </summary>
    public void OnRetryButtonClicked()
    {
        ButtonClickAnimation(retryButton, gameObject, "HideCurrentWindow");
        InitGameScene(selectedLevel);
    }

    /// <summary>
    /// An event handle for the 'Title' button on the score window.
    /// </summary>
    public void OnTitleButtonClicked()
    {
        ButtonClickAnimation(titleButton, gameObject, "SwitchToWindow", selectorWindow);
    }

    /// <summary>
    /// Shows a splash screen during the delayForSplash.
    /// </summary>
    /// <param name="toLast"> A period that shows the spash screen. </param>
    protected IEnumerator ShowSplash(float delayForSplash)
    {
        ShowWindow(splashWindow);

        yield return new WaitForSeconds(delayForSplash);

        HideWindow(splashWindow);
        ShowWindow(selectorWindow);
    }

    /* Shows a final window after gameplay has been completed, or failed. */
    private void ShowFinalWindow()
    {
        finalScore.text = "Score: " + m_ScoreText.text;

        ShowWindow(endWindow);
    }
}
