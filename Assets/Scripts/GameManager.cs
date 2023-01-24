using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public TargetRowManager[] targetRowManagers;
    public ScoreboardManager scoreboardManager;
    public float spawnDelay;
    public float gameTime;

    public bool timerIsRunning = false;
    private bool targetsSpawning = false;
    private float internalTime;

    private Coroutine spawnCoroutine;

    void Start()
    {

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
        targetsSpawning = false;
        StopCoroutine(spawnCoroutine);
        scoreboardManager.ResetScore();
    }

    private void SpawnRandomTargetsIfNeeded() {
        if(targetRowManagers.Where(n => n.HasActiveTargets()).Any() || targetsSpawning) { return; }
        spawnCoroutine = StartCoroutine(SpawnRandomTargets());
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
