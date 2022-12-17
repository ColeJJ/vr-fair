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
    public float spawnDelay;
    public float gameTime;

    public bool timerIsRunning = false;
    private bool targetsSpawning = false;
    private float internalTime;

    void Start()
    {
        GameLevel testLevel = new GameLevel {
            name = "Level 1",
            targetRowConfigurations = new TargetRowConfiguration[] {
                new TargetRowConfiguration {
                    targetSpawnCount = 2,
                    colorDisplayCount = 0,
                    heavyTargetCount = 2,
                    heavyTargetSpawnCount = 1
                },
                new TargetRowConfiguration {
                    targetSpawnCount = 2,
                    colorDisplayCount = 0,
                    heavyTargetCount = 0,
                    heavyTargetSpawnCount = 0
                },
                new TargetRowConfiguration {
                    targetSpawnCount = 1,
                    colorDisplayCount = 0,
                    heavyTargetCount = 0,
                    heavyTargetSpawnCount = 0
                },
                new TargetRowConfiguration {
                    targetSpawnCount = 1,
                    colorDisplayCount = 0,
                    heavyTargetCount = 0,
                    heavyTargetSpawnCount = 0
                }
            }
        };
        StartGame(testLevel);
    }

    void Update()
    {
        if (timerIsRunning) {
            if (internalTime > 0) {
                internalTime -= Time.deltaTime;
                SpawnRandomTargetsIfNeeded();
            } else {
                internalTime = 0;
                timerIsRunning = false;
                UpdateTargetStates(TargetState.Down);
            }
            scoreboardManager.UpdateTime(internalTime);
        }
    }

    public void StartGame(GameLevel level) { 
        ResetTargets();       
        SetTargetConfigurations(level.targetRowConfigurations);
        scoreboardManager.ResetScore();
        internalTime = gameTime;
        timerIsRunning = true;
    }

    public void CancelGame() { 
        internalTime = 0;
        scoreboardManager.ResetScore();
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

    private void SetTargetConfigurations(TargetRowConfiguration[] configurations) {
        for(int i = 0; i < targetRowManagers.Length; i++) {
            targetRowManagers[i].Setup(configurations[i]);
        }
    }

    private void ResetTargets() {
        foreach(TargetRowManager targetRowManager in targetRowManagers) {
            targetRowManager.Reset();
        }
    }
}
