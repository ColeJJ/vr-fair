using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TargetState {
    Up,
    Down
}

public class TargetStateHandler : MonoBehaviour
{

    private TargetState state = TargetState.Up;
    // public TargetState state {
    //     get { return state; }
    //     set {
    //         if(value != state) {
    //             state = value;
    //             ChangeTargetPosition(state);
    //         }
    //     }
    // }

    // Start is called before the first frame update
    void Start()
    {
        UpdateTargetTransformation(this.state);
    }

    // Update is called once per frame
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
            transform.Rotate(0, 0, 0);
        } else {
            transform.Rotate(90, 0, 0);
        }
    }
}
