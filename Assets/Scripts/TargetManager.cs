using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public enum TargetState {
    Up,
    Hit,
    Down
}

public enum TargetType {
    Normal,
    Heavy
}

public class TargetManager : MonoBehaviour
{
    public ScoreboardManager scoreboardManager;
    public TMP_Text hitpointText;
    public Material standardMaterial;
    public Material heavyMaterial;
    public TargetState state = TargetState.Hit;
    public int totalHitpoints = 1;
    public int scorePoints = 1;

    private HingeJoint targetJoint;
    private Rigidbody rb;
    private int hitpoints;

    void Start()
    {
        targetJoint = GetComponent<HingeJoint>();
        rb = GetComponent<Rigidbody>();
        hitpoints = totalHitpoints;
    }

    void Update()
    {
        if(transform.localRotation.eulerAngles.x < 0.1) {
            targetJoint.useSpring = false;
            rb.isKinematic = hitpoints > 1;
            state = TargetState.Up;
        } else if(transform.localRotation.eulerAngles.x > 89.9) {
            targetJoint.useMotor = false;
            state = TargetState.Down;
        }

        hitpointText.text = hitpoints > 0 ? hitpoints.ToString() : "";
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag != "Bullet" || state != TargetState.Up) { return; }

        BulletManager bulletManager = collision.gameObject.GetComponent<BulletManager>();
        if(bulletManager.collisionCount > 1) { return; }

        hitpoints -= 1;

        if(hitpoints == 0) {
            state = TargetState.Hit;
            scoreboardManager.UpdateScore(scorePoints);
        }
    }

    public void UpdateTargetState(TargetState state) { 
        switch(state) {
            case TargetState.Up:
                hitpoints = totalHitpoints;
                targetJoint.useSpring = true;
                break;
            case TargetState.Down:
                hitpoints = 0;
                targetJoint.useMotor = true;
                break;
            case TargetState.Hit:
                print("Hit state not supported for programmatic updates");
                break;
        }
    }

    public void SetTargetType(TargetType type) {
        switch(type) {
            case TargetType.Normal:
                totalHitpoints = 1;
                scorePoints = 1;
                SetMaterial(standardMaterial);
                break;
            case TargetType.Heavy:
                totalHitpoints = 5;
                scorePoints = 5;
                SetMaterial(heavyMaterial);
                break;
        }
    }

    private void SetMaterial(Material material) {
        foreach(Transform child in transform) {
            child.gameObject.GetComponent<Renderer>().material = material;
        }
    }

}
