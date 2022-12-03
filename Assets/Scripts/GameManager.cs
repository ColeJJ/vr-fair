using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

struct SplitResult<T> {
    T[] splittedElements;
    T[] remainderElements;
}

public class GameManager : MonoBehaviour
{
    public TargetManager[] targetManagers;
    public ScoreboardManager scoreboardManager;

    public int spawnCount;
    public float spawnDelay;
    public int heavyTargetCount;
    public float gameTime;

    private bool timerIsRunning = true;
    private bool targetsSpawning = false;

    void Start()
    {
        TargetManager[] targetManagersHeavy = SplitOffRandomTargets(targetManagers, heavyTargetCount);
        foreach(TargetManager targetManager in targetManagersHeavy) { 
            targetManager.SetTargetType(TargetType.Heavy);
        }

        UpdateTargetStates(targetManagers, TargetState.Down);
    }

    void Update()
    {
        if (timerIsRunning) {
            if (gameTime > 0) {
                gameTime -= Time.deltaTime;
                SpawnRandomTargetsIfNeeded();
            } else {
                gameTime = 0;
                UpdateTargetStates(targetManagers, TargetState.Down);
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
        var targetsToSpawn = SplitOffRandomTargets(targetManagers, count);
        UpdateTargetStates(targetsToSpawn, TargetState.Up);
        yield return new WaitForSeconds(1f);
        targetsSpawning = false;
    }

    private TargetManager[] SplitOffRandomTargets(TargetManager[] targetManagers, int count) {
        var mutableList = new List<TargetManager>(targetManagers);
        var splittedList = new List<TargetManager>();
        for(int i = 0; i < count; i++) {
            int index = Random.Range(0, mutableList.Count);
            splittedList.Add(mutableList[index]);
            mutableList.RemoveAt(index);
        }

        return splittedList.ToArray();
    }

    private void UpdateTargetStates(TargetManager[] targetManagers, TargetState state) {
        foreach(TargetManager targetManager in targetManagers) {
            targetManager.UpdateTargetState(state);
        }
    }
}
