using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public TargetStateHandler[] targetStateHandlers;
    public ScoreboardManager scoreboardManager;

    public int spawnCount;
    public float spawnDelay;
    public float gameTime;
    private bool timerIsRunning = true;
    private bool targetsSpawning = false;

    void Start()
    {
        UpdateTargets(targetStateHandlers, TargetState.Down);
    }

    void Update()
    {
        if (timerIsRunning) {
            if (gameTime > 0) {
                gameTime -= Time.deltaTime;
                SpawnRandomTargetsIfNeeded();
            } else {
                gameTime = 0;
                UpdateTargets(targetStateHandlers, TargetState.Down);
                timerIsRunning = false;
            }
            scoreboardManager.UpdateTime(gameTime);
        }
    }

    private void SpawnRandomTargetsIfNeeded() {
        if(targetStateHandlers.Where(n => n.state == TargetState.Up).Any() || targetsSpawning) { return; }
        StartCoroutine(SpawnRandomTargets(spawnCount, spawnDelay));
    }

    private IEnumerator SpawnRandomTargets(int count, float delay) {
        targetsSpawning = true;
        yield return new WaitForSeconds(delay);
        TargetStateHandler[] targetsToSpawn = SelectRandomListItems(targetStateHandlers, count);
        UpdateTargets(targetsToSpawn, TargetState.Up);
        yield return new WaitForSeconds(1f);
        targetsSpawning = false;
    }

    private TargetStateHandler[] SelectRandomListItems(TargetStateHandler[] targetStateHandlers, int count) {
        var mutableList = new List<TargetStateHandler>(targetStateHandlers);
        int removeCount = mutableList.Count - count;
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
