using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum ColorType {
    None,
    Red,
    Green
}

public class GameManager : MonoBehaviour
{
    public TargetManager[] targetManagers;
    public ScoreboardManager scoreboardManager;
    public float spawnDelay;
    public int targetSpawnCount;
    public int colorDisplayCount;
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
        this.scoreboardManager.ResetScore();

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
        StartCoroutine(SpawnRandomTargets());
    }

    private IEnumerator SpawnRandomTargets() {
        targetsSpawning = true;
        yield return new WaitForSeconds(spawnDelay);

        var (heavyTargetsToSpawn, _) = SplitOffRandomTargets(targetManagersHeavy, heavyTargetSpawnCount);
        var (normalTargetsToSpawn, _) = SplitOffRandomTargets(targetManagersNormal, targetSpawnCount);
        UpdateTargetColorTypes(normalTargetsToSpawn, colorDisplayCount);

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

    private void UpdateTargetColorTypes(TargetManager[] targetManagers, int displayCount) {
        var (coloredTargets, nonColoredTargets) = SplitOffRandomTargets(targetManagers, displayCount);

        foreach(TargetManager targetManager in coloredTargets) {
            ColorType colorType = Random.Range(0, 2) == 1 ? ColorType.Green : ColorType.Red;
            targetManager.UpdateTargetColorType(colorType);
        }

        foreach(TargetManager targetManager in nonColoredTargets) {
            targetManager.UpdateTargetColorType(ColorType.None);
        } 
    }

    private void UpdateTargetStates(TargetManager[] targetManagers, TargetState state) {
        foreach(TargetManager targetManager in targetManagers) {
            targetManager.UpdateTargetState(state);
        }
    }
}
