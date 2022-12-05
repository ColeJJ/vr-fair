using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreboardManager : MonoBehaviour
{

    public int score = 0;
    public TMP_Text timeText;
    public TMP_Text scoreText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ResetScore() {
        score = 0;
        scoreText.text = $"Score: {score}";
    }

    public void UpdateScore(int addScore) {
        score += addScore;
        scoreText.text = $"Score: {score}";
    }

    public void UpdateTime(float time) {
        float minutes = Mathf.FloorToInt(time / 60);
        float seconds = Mathf.FloorToInt(time % 60);
        timeText.text = string.Format("Time: {0:00}:{1:00}", minutes, seconds);
    }
}
