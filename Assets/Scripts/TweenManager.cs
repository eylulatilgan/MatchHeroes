using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Prime31.ZestKit;

public class TweenManager : MonoBehaviour {
    [SerializeField]
    private RectTransform creditsPanel;
    [SerializeField]
    private RectTransform settingsPanel;
    [SerializeField]
    private RectTransform startButton;
    [SerializeField]
    private RectTransform settingsButton;
    [SerializeField]
    private RectTransform creditsButton;
    [SerializeField]
    private RectTransform headerText;
    [SerializeField]
    private Transform mainCamera;
    [SerializeField]
    private AnimationCurve slowingCurve;
    [SerializeField]
    private AnimationCurve jumpyCurve;
    [SerializeField]
    private RectTransform scoreText;
    [SerializeField]
    private RectTransform gameOverPanel;
  

    private TweenParty visibleMenuParty;
    private TweenParty invisibleMenuParty;

    void Awake()
    {
        visibleMenuParty = new TweenParty(1.5f);        
        invisibleMenuParty = new TweenParty(1.5f);
    }

    void Start()
    {
        InitVisibleMenuParty();
        visibleMenuParty.start();
    }

    private void InitInvisibleMenuParty()
    {
        invisibleMenuParty.addTween(startButton.ZKanchoredPositionTo(new Vector2(0f, 414.8f)));
        invisibleMenuParty.addTween(settingsButton.ZKanchoredPositionTo(new Vector2(0f, -350.6f)));
        invisibleMenuParty.addTween(creditsButton.ZKanchoredPositionTo(new Vector2(-50f, 50f)));
        invisibleMenuParty.addTween(headerText.ZKanchoredPositionTo(new Vector2(0f, 123f)));

        invisibleMenuParty.setDelay(0.1f).setDuration(1.5f);
        invisibleMenuParty.setAnimationCurve(slowingCurve);
        invisibleMenuParty.setIsRelative();
    }

    private void InitVisibleMenuParty()
    {
        visibleMenuParty.addTween(startButton.ZKanchoredPositionTo(new Vector2(0f, 0f)));
        visibleMenuParty.addTween(settingsButton.ZKanchoredPositionTo(new Vector2(0f, -90f)));
        visibleMenuParty.addTween(creditsButton.ZKanchoredPositionTo(new Vector2(50f, 50f)));
        visibleMenuParty.addTween(headerText.ZKanchoredPositionTo(new Vector2(0f, -130f)));      

        visibleMenuParty.setDelay(0.1f).setDuration(1.5f);
        visibleMenuParty.setAnimationCurve(slowingCurve);
        visibleMenuParty.setIsRelative();
    }

    public void CloseSettingPanel()
    {
        InitVisibleMenuParty();
        settingsPanel.ZKanchoredPositionTo(new Vector2(412f, 0f), 1f).setDelay(0.3f).setAnimationCurve(slowingCurve).setIsRelative().start();
        visibleMenuParty.start();
    }

    public void OpenSettingsPanel()
    {
        InitInvisibleMenuParty();
        settingsPanel.ZKanchoredPositionTo(new Vector2(0f, 0f), 1f).setDelay(0.3f).setAnimationCurve(slowingCurve).setIsRelative().start();
        invisibleMenuParty.start();
    }

    public void OpenCreditsPanel()
    {
        InitInvisibleMenuParty();
        creditsPanel.ZKanchoredPositionTo(new Vector2(0f, 0f), 1f).setDelay(0.3f).setAnimationCurve(slowingCurve).setIsRelative().start();
        invisibleMenuParty.start();
    }

    public void CloseCreditsPanel()
    {
        InitVisibleMenuParty();
        creditsPanel.ZKanchoredPositionTo(new Vector2(-412f, 0f), 1f).setDelay(0.3f).setAnimationCurve(slowingCurve).setIsRelative().start();
        visibleMenuParty.start();

    }

    public void StartTheGame()
    {
        InitInvisibleMenuParty();
        scoreText.ZKanchoredPositionTo(new Vector2(0f, -50f)).setDelay(2).setDuration(1).start();
        invisibleMenuParty.start();
        TriggerEvents();
    }

    public void TriggerEvents()
    {
        GameEvents.TriggerHandSpawn();
        GameEvents.TriggerInitBoard();
        mainCamera.ZKpositionTo(new Vector3(0f, 11f,-1f), 1f).setAnimationCurve(slowingCurve).start();
    }

    void OnEnable()
    {
        GameEvents.OnGameOver += GameOver;
    }

    void OnDisable()
    {
        GameEvents.OnGameOver -= GameOver;
    }

    public void GameOver()
    {
        gameOverPanel.ZKpositionTo(new Vector2(0f, 0f), 1f).setAnimationCurve(slowingCurve).start();
    }

    public void ReturnToTheMainMenu()
    {
        //gameOverPanel.ZKpositionTo(new Vector2(0f, 500f), 1f).setAnimationCurve(slowingCurve).start();
        GameEvents.TriggerResetGameState();
        mainCamera.ZKpositionTo(new Vector3(0f, 0f, -1f), 1f).setAnimationCurve(slowingCurve).start();
        scoreText.ZKanchoredPositionTo(new Vector2(0f, 50f)).setDuration(1).start();
        InitVisibleMenuParty();
        visibleMenuParty.start();
    }
}
