using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TargetSpawnManager : MonoBehaviour
{

    public TargetStateHandler[] targetStateHandlers;
    void Start()
    {

    }

    void Update()
    {
        if(!targetStateHandlers.Where(n => n.state == TargetState.Up).Any()) {
            TargetStateHandler[] targetsToSpawn = SelectRandomListItems(targetStateHandlers, 5);
            foreach(TargetStateHandler stateHandler in targetsToSpawn) {
                stateHandler.UpdateTargetState(TargetState.Up);
            }
        }
    }

    TargetStateHandler[] SelectRandomListItems(TargetStateHandler[] targetStateHandlers, int threshold) {
        var mutableList = new List<TargetStateHandler>(targetStateHandlers);
        int removeCount = mutableList.Count - threshold;
        for(int i = 0; i < removeCount; i++) {
            int index = Random.Range(0, mutableList.Count);
            mutableList.RemoveAt(index);
        }

        return mutableList.ToArray();
    }
}
