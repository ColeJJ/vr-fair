using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TargetState {
    Up,
    Hit,
    Down
}

public class TargetManager : MonoBehaviour
{
    public TargetState state = TargetState.Up;
    public ScoreboardManager scoreboardManager;
    public int hitThreshold = 1;
    public int points = 1;

    private HingeJoint targetJoint;
    private Rigidbody rb;

    void Start()
    {
        targetJoint = GetComponent<HingeJoint>();
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if(transform.localRotation.eulerAngles.x < 0.1) {
            targetJoint.useSpring = false;
            rb.isKinematic = hitThreshold > 1;
            state = TargetState.Up;
        } else if(transform.localRotation.eulerAngles.x > 89.9) {
            targetJoint.useMotor = false;
            state = TargetState.Down;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag != "Bullet" || state != TargetState.Up) { return; }

        BulletManager bulletManager = collision.gameObject.GetComponent<BulletManager>();
        if(bulletManager.collisionCount > 1) { return; }

        hitThreshold -= 1;
        if(hitThreshold == 0) {
            state = TargetState.Hit;
            scoreboardManager.UpdateScore(points);
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
