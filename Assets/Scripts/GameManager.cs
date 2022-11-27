using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public TargetStateHandler[] targetStateHandlers;
    public ScoreboardManager scoreboardManager;

    public int targetThreshold;
    public float gameTime;
    private bool timerIsRunning = true;
    
    void Start()
    {
        UpdateTargets(targetStateHandlers, TargetState.Down);
        InvokeRepeating("SpawnRandomTargetsIfNeeded", 2f, 2f);
    }

    void Update()
    {
        if (timerIsRunning) {
            if (gameTime > 0) {
                gameTime -= Time.deltaTime;
            } else {
                gameTime = 0;
                CancelInvoke();
                UpdateTargets(targetStateHandlers, TargetState.Down);
                timerIsRunning = false;
            }
            scoreboardManager.UpdateTime(gameTime);
        }
    }

    private void SpawnRandomTargetsIfNeeded() {
        if(targetStateHandlers.Where(n => n.state == TargetState.Up).Any()) { return; }
        StartCoroutine(SpawnRandomTargets(targetThreshold, 1f));
    }

    private IEnumerator SpawnRandomTargets(int threshold, float delay) {
        yield return new WaitForSeconds(delay);
        TargetStateHandler[] targetsToSpawn = SelectRandomListItems(targetStateHandlers, threshold);
        UpdateTargets(targetsToSpawn, TargetState.Up);
    }

    private TargetStateHandler[] SelectRandomListItems(TargetStateHandler[] targetStateHandlers, int threshold) {
        var mutableList = new List<TargetStateHandler>(targetStateHandlers);
        int removeCount = mutableList.Count - threshold;
        for(int i = 0; i < removeCount; i++) {
            int index = Random.Range(0, mutableList.Count);
            mutableList.RemoveAt(index);
        }

        return mutableList.ToArray();
    }

    private void UpdateTargets(TargetStateHandler[] stateHandlers, TargetState state) {
        foreach(TargetStateHandler stateHandler in stateHandlers) {
            stateHandler.UpdateTargetState(state);
        }
    }
}
