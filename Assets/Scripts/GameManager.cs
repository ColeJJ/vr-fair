using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public TargetManager[] targetManagers;
    public ScoreboardManager scoreboardManager;

    public int spawnCount;
    public float spawnDelay;
    public float gameTime;
    private bool timerIsRunning = true;
    private bool targetsSpawning = false;

    void Start()
    {
        UpdateTargets(targetManagers, TargetState.Down);
    }

    void Update()
    {
        if (timerIsRunning) {
            if (gameTime > 0) {
                gameTime -= Time.deltaTime;
                SpawnRandomTargetsIfNeeded();
            } else {
                gameTime = 0;
                UpdateTargets(targetManagers, TargetState.Down);
                timerIsRunning = false;
            }
            scoreboardManager.UpdateTime(gameTime);
        }
    }

    private void SpawnRandomTargetsIfNeeded() {
        if(targetManagers.Where(n => n.state == TargetState.Up).Any() || targetsSpawning) { return; }
        StartCoroutine(SpawnRandomTargets(spawnCount, spawnDelay));
    }

    private IEnumerator SpawnRandomTargets(int count, float delay) {
        targetsSpawning = true;
        yield return new WaitForSeconds(delay);
        TargetManager[] targetsToSpawn = SelectRandomListItems(targetManagers, count);
        UpdateTargets(targetsToSpawn, TargetState.Up);
        yield return new WaitForSeconds(1f);
        targetsSpawning = false;
    }

    private TargetManager[] SelectRandomListItems(TargetManager[] targetManagers, int count) {
        var mutableList = new List<TargetManager>(targetManagers);
        int removeCount = mutableList.Count - count;
        for(int i = 0; i < removeCount; i++) {
            int index = Random.Range(0, mutableList.Count);
            mutableList.RemoveAt(index);
        }

        return mutableList.ToArray();
    }

    private void UpdateTargets(TargetManager[] targetManagers, TargetState state) {
        foreach(TargetManager targetManager in targetManagers) {
            targetManager.UpdateTargetState(state);
        }
    }
}
