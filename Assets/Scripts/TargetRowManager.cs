using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TargetRowConfiguration {
    public int targetSpawnCount;
    public int colorDisplayCount;
    public int heavyTargetCount;
    public int heavyTargetSpawnCount;
}

public class TargetRowManager : MonoBehaviour
{
    public TargetManager[] targetManagers;
    
    private TargetRowConfiguration configuration;
    private TargetManager[] targetManagersNormal;
    private TargetManager[] targetManagersHeavy;

    public void Setup(TargetRowConfiguration configuration) {
        this.configuration = configuration;
        UpdateTargetStates(TargetState.Down);
        SetHeavyTargets();
    }

    public void UpdateTargetStates(TargetState state) {
        UpdateTargetStates(targetManagers, state);
    }

    public void Reset() {
        foreach(TargetManager targetManager in targetManagers) {
            targetManager.Reset();
        }

        configuration = null;
        targetManagersNormal = null;
        targetManagersHeavy = null;
    }

    public void SpawnRandomTargets() {
        var (heavyTargetsToSpawn, _) = SplitOffRandomTargets(targetManagersHeavy, configuration.heavyTargetSpawnCount);
        var (normalTargetsToSpawn, _) = SplitOffRandomTargets(targetManagersNormal, configuration.targetSpawnCount);
        UpdateTargetColorTypes(normalTargetsToSpawn, configuration.colorDisplayCount);

        var targetsToSpawn = heavyTargetsToSpawn.Concat(normalTargetsToSpawn).ToArray();
        UpdateTargetStates(targetsToSpawn, TargetState.Up);
    }

    public bool HasActiveTargets() {
        return targetManagers.Where(n => n.state == TargetState.Up).Any();
    }

    private void SetHeavyTargets() {
        var (targetManagersHeavy, targetManagersNormal) = SplitOffRandomTargets(targetManagers, configuration.heavyTargetCount);
        foreach(TargetManager targetManager in targetManagersHeavy) { 
            targetManager.SetTargetType(TargetType.Heavy);
        }
        this.targetManagersNormal = targetManagersNormal;
        this.targetManagersHeavy = targetManagersHeavy;
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
