using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TargetState {
    Up,
    Down
}

public class TargetStateHandler : MonoBehaviour
{
    public TargetState state = TargetState.Up;

    HingeJoint targetJoint;

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

    public void OnBulletCollision() {
        print("Hit!");
    }

    public void UpdateTargetState(TargetState state) { 
        if(state == TargetState.Up) {
            targetJoint.useSpring = true;
        } else {
            targetJoint.useMotor = true;
        }
    }

}
