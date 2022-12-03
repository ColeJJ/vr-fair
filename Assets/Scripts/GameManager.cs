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
    public int heavyTargetSpawnCount;
    public float gameTime;

    private TargetManager[] targetManagersNormal;
    private TargetManager[] targetManagersHeavy;
    private bool timerIsRunning = true;
    private bool targetsSpawning = false;

    void Start()
    {
        var (targetManagersHeavy, targetManagersNormal) = SplitOffRandomTargets(targetManagers, heavyTargetCount);
        foreach(TargetManager targetManager in targetManagersHeavy) { 
            targetManager.SetTargetType(TargetType.Heavy);
        }
        this.targetManagersNormal = targetManagersNormal;
        this.targetManagersHeavy = targetManagersHeavy;

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

        var (heavyTargetsToSpawn, _) = SplitOffRandomTargets(targetManagersHeavy, heavyTargetSpawnCount);
        var (normalTargetsToSpawn, _) = SplitOffRandomTargets(targetManagersNormal, spawnCount - heavyTargetSpawnCount);
        var targetsToSpawn = heavyTargetsToSpawn.Concat(normalTargetsToSpawn).ToArray();
        UpdateTargetStates(targetsToSpawn, TargetState.Up);

        yield return new WaitForSeconds(1f);
        targetsSpawning = false;
    }

    private (TargetManager[], TargetManager[]) SplitOffRandomTargets(TargetManager[] targetManagers, int count) {
        var remainderList = new List<TargetManager>(targetManagers);
        var splittedList = new List<TargetManager>();
        for(int i = 0; i < count; i++) {
            int index = Random.Range(0, remainderList.Count);
            splittedList.Add(remainderList[index]);
            remainderList.RemoveAt(index);
        }

        return (splittedList.ToArray(), remainderList.ToArray());
    }

    private void UpdateTargetStates(TargetManager[] targetManagers, TargetState state) {
        foreach(TargetManager targetManager in targetManagers) {
            targetManager.UpdateTargetState(state);
        }
    }
}
