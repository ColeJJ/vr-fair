using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TargetSpawnManager : MonoBehaviour
{

    public TargetStateHandler[] targetStateHandlers;
    void Start()
    {
        foreach(TargetStateHandler stateHandler in targetStateHandlers) {
            stateHandler.UpdateTargetState(TargetState.Down);
        }
        InvokeRepeating("SpawnRandomTargetsIfNeeded", 2f, 2f);
    }

    void Update()
    {
        
    }

    private void SpawnRandomTargetsIfNeeded() {
        print("Spawn invoked");
        if(targetStateHandlers.Where(n => n.state == TargetState.Up).Any()) { return; }

        print("Spawn executed");
        StartCoroutine(SpawnRandomTargets(5, 1f));
    }

    private IEnumerator SpawnRandomTargets(int threshold, float delay) {
        yield return new WaitForSeconds(delay);
        TargetStateHandler[] targetsToSpawn = SelectRandomListItems(targetStateHandlers, threshold);
        foreach(TargetStateHandler stateHandler in targetsToSpawn) {
            stateHandler.UpdateTargetState(TargetState.Up);
        }
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
}
