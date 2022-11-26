using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TargetState {
    Up,
    Down
}

public class TargetStateHandler : MonoBehaviour
{
    public TargetState state = TargetState.Down;

    void Start()
    {
        UpdateTargetTransformation(this.state);
    }

    void Update()
    {
        
    }

    public void UpdateTargetState(TargetState state) {
        if(state == this.state) { return; }

        this.state = state;
        UpdateTargetTransformation(state);
    }

    private void UpdateTargetTransformation(TargetState state) {
        if(state == TargetState.Up) {
            transform.Rotate(-90, 0, 0);
        } else {
            transform.Rotate(90, 0, 0);
        }
    }
}
