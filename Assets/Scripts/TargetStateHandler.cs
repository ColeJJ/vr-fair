using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TargetState {
    Up,
    Hit,
    Down
}

public class TargetStateHandler : MonoBehaviour
{
    public TargetState state = TargetState.Up;
    public ScoreboardManager scoreboardManager;

    private HingeJoint targetJoint;

    void Start()
    {
        targetJoint = GetComponent<HingeJoint>();
    }

    void Update()
    {
        if(transform.localRotation.eulerAngles.x < 0.1) {
            targetJoint.useSpring = false;
            state = TargetState.Up;
        } else if(transform.localRotation.eulerAngles.x > 89.9) {
            targetJoint.useMotor = false;
            state = TargetState.Down;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(state == TargetState.Up) {
            state = TargetState.Hit;
            scoreboardManager.UpdateScore(1);
        }
    }

    public void UpdateTargetState(TargetState state) { 
        if(state == TargetState.Up) {
            targetJoint.useSpring = true;
        } else if (state == TargetState.Down) {
            targetJoint.useMotor = true;
        }
    }

}
