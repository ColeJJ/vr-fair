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
    public TargetRowManager[] targetRowManagers;
    public ScoreboardManager scoreboardManager;
    public TargetRowConfiguration[] targetRowConfigurations;
    public float spawnDelay;
    public float gameTime;

    private bool timerIsRunning = true;
    private bool targetsSpawning = false;

    void Start()
    {
        targetRowConfigurations = new TargetRowConfiguration[] {
            new TargetRowConfiguration {
                targetSpawnCount = 4,
                colorDisplayCount = 1,
                heavyTargetCount = 1,
                heavyTargetSpawnCount = 1
            },
            new TargetRowConfiguration {
                targetSpawnCount = 4,
                colorDisplayCount = 1,
                heavyTargetCount = 1,
                heavyTargetSpawnCount = 1
            }
        };

        this.scoreboardManager.ResetScore();
        for(int i = 0; i < targetRowManagers.Length; i++) {
            targetRowManagers[i].Setup(targetRowConfigurations[i]);
        }
    }

    void Update()
    {
        if (timerIsRunning) {
            if (gameTime > 0) {
                gameTime -= Time.deltaTime;
                SpawnRandomTargetsIfNeeded();
            } else {
                gameTime = 0;
                UpdateTargetStates(TargetState.Down);
                timerIsRunning = false;
            }
            scoreboardManager.UpdateTime(gameTime);
        }
    }

    private void SpawnRandomTargetsIfNeeded() {
        if(targetRowManagers.Where(n => n.HasActiveTargets()).Any() || targetsSpawning) { return; }
        StartCoroutine(SpawnRandomTargets());
    }

    private IEnumerator SpawnRandomTargets() {
        targetsSpawning = true;
        yield return new WaitForSeconds(spawnDelay);

        foreach(TargetRowManager targetRowManager in targetRowManagers) {
            targetRowManager.SpawnRandomTargets();
        }

        yield return new WaitForSeconds(1f);
        targetsSpawning = false;
    }

    private void UpdateTargetStates(TargetState state) {
        foreach(TargetRowManager targetRowManager in targetRowManagers) {
            targetRowManager.UpdateTargetStates(state);
        }
    }
}
