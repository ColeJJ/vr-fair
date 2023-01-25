using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public struct GameLevel {
    public string name;
    public TargetRowConfiguration[] targetRowConfigurations;
}

public class MenuManager : MonoBehaviour
{
    private enum GameState {
        Idle,
        Starting,
        Running
    }

    public GameManager gameManager;
    public TMP_Text actionButtonText;
    public TMP_Dropdown levelSelection;
    public float startupTime;

    private GameLevel[] levels;
    private int levelSelectionIndex = 0;
    private bool startCounterRunning = false;
    private GameState gameState = GameState.Idle;
    private float internalStartupTime;

    // Start is called before the first frame update
    void Start()
    {
        levels = LevelProvider.levels;
        foreach(GameLevel level in levels) {
            levelSelection.options.Add(new TMP_Dropdown.OptionData() { text = level.name });
        }
        levelSelection.RefreshShownValue();
    }

    // Update is called once per frame
    void Update()
    {
        switch(gameState) {
            case GameState.Idle:
                break;
            case GameState.Starting:
                if(internalStartupTime > 0) {
                    internalStartupTime -= Time.deltaTime;
                } else {
                    internalStartupTime = 0;
                }
                actionButtonText.text = string.Format("{0:0.0}", internalStartupTime);
                break;
            case GameState.Running:
                if(gameManager.timerIsRunning) {
                    actionButtonText.text = "Cancel";
                } else {
                    actionButtonText.text = "Start";
                    gameState = GameState.Idle;
                }
                break;
        }
    }

    public void LevelSelected() {
        levelSelectionIndex = levelSelection.value;
    }

    public void ActionButtonTapped() {
        switch(gameState) {
            case GameState.Idle:
                StartCoroutine(StartGame());
                break;
            case GameState.Starting:
                break;
            case GameState.Running:
                gameManager.CancelGame();
                break;
        }
    }

    private IEnumerator StartGame() {
        internalStartupTime = startupTime;
        gameState = GameState.Starting;
        yield return new WaitForSeconds(startupTime);
        gameManager.StartGame(levels[levelSelectionIndex]);
        gameState = GameState.Running;
    }
}
