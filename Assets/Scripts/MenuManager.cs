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

    public GameManager gameManager;
    public TMP_Text actionButtonText;
    public TMP_Dropdown levelSelection;

    private GameLevel[] levels;
    private int levelSelectionIndex = 0;
    private bool gameRunning = false;

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
        if(gameRunning) {
            if(gameManager.timerIsRunning) {
                actionButtonText.text = "Cancel";
            } else {
                actionButtonText.text = "Start";
                gameRunning = false;
            }
        }
    }

    public void LevelSelected() {
        levelSelectionIndex = levelSelection.value;
    }

    public void ActionButtonTapped() {
        if(!gameRunning) {
            gameManager.StartGame(levels[levelSelectionIndex]);
            gameRunning = true;
        } else {
            gameManager.CancelGame();
        }
    }
}
