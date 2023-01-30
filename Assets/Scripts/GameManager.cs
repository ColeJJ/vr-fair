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
    public int countdownStart;
    public int countdownStep;

    public bool timerIsRunning = false;
    private bool targetsSpawning = false;
    private bool firstSpawn = true;
    private float internalTime;
    private int internalCountdown;

    private Coroutine spawnCoroutine;

    private AudioSource [] timeSounds;

    void Start()
    {
        timeSounds = GameObject.Find("TimeSounds").GetComponents<AudioSource>();
    }

    void Update()
    {
        if (timerIsRunning) {
            internalTime -= Time.deltaTime;

            if (internalTime > 0) {
                SpawnRandomTargetsIfNeeded();

                if(internalTime < (float)internalCountdown) {
                    timeSounds[0].Play();
                    internalCountdown -= countdownStep;
                }
            } else {
                internalTime = 0;
                timerIsRunning = false;
                UpdateTargetStates(TargetState.Down);
                timeSounds[1].Play();
            }
            scoreboardManager.UpdateTime(internalTime);
        }
    }

    public void StartGame(GameLevel level) { 
        ResetTargets();       
        SetTargetConfigurations(level.targetRowConfigurations);
        scoreboardManager.ResetScore();
        internalTime = gameTime;
        internalCountdown = countdownStart;
        timerIsRunning = true;
        firstSpawn = true;
        timeSounds[3].Play();
    }

    public void CancelGame() { 
        internalTime = 0;
        targetsSpawning = false;
        StopCoroutine(spawnCoroutine);
        scoreboardManager.ResetScore();
    }

    private void SpawnRandomTargetsIfNeeded() {
        if(targetRowManagers.Where(n => n.HasActiveTargets()).Any() || targetsSpawning) { return; }

        if(!firstSpawn) {
            timeSounds[4].Play();
        }
        spawnCoroutine = StartCoroutine(SpawnRandomTargets());
        firstSpawn = false;
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
