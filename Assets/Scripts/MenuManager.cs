using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MenuManager : MonoBehaviour
{

    public GameManager gameManager;
    public TMP_Text actionButtonText;
    public bool gameRunning = false;

    // Start is called before the first frame update
    void Start()
    {
        
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

    public void ActionButtonTapped() {
        if(!gameRunning) {
            gameManager.StartGame();
            gameRunning = true;
        } else {
            gameManager.CancelGame();
        }
    }
}
