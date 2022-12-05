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
    public GameObject canvas;
    public GameObject colorDisplay;
    public TargetState state = TargetState.Up;
    public int totalHitpoints = 1;
    public int scorePoints = 1;

    private HingeJoint targetJoint;
    private Rigidbody rb;
    private TMP_Text hitpointText;
    private ColorType colorType = ColorType.None;
    private int hitpoints;

    void Start()
    {
        targetJoint = GetComponent<HingeJoint>();
        rb = GetComponent<Rigidbody>();
        hitpointText = canvas.transform.GetChild(0).GetComponent<TMP_Text>();

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

        if(canvas.activeSelf) {
            hitpointText.text = hitpoints > 0 ? hitpoints.ToString() : "";
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag != "Bullet" || state != TargetState.Up) { return; }

        BulletManager bulletManager = collision.gameObject.GetComponent<BulletManager>();
        if(bulletManager.collisionCount > 1 || !MatchColorType(bulletManager.colorType)) { return; }

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

    public void UpdateTargetColorType(ColorType colorType) {
        this.colorType = colorType;
        switch(colorType) {
            case ColorType.None:
                colorDisplay.SetActive(false);
                break;
            case ColorType.Green:
                Material greenMaterial = Resources.Load("Material/Color Type Green", typeof(Material)) as Material;
                colorDisplay.GetComponent<Renderer>().material = greenMaterial;
                colorDisplay.SetActive(true);
                break;
            case ColorType.Red:
                Material redMaterial = Resources.Load("Material/Color Type Red", typeof(Material)) as Material;
                colorDisplay.GetComponent<Renderer>().material = redMaterial;
                colorDisplay.SetActive(true);
                break;
        }
    }

    public void SetTargetType(TargetType type) {
        switch(type) {
            case TargetType.Normal:
                totalHitpoints = 1;
                scorePoints = 1;
                canvas.SetActive(false);
                Material standardMaterial = Resources.Load("Material/Target Standard Material", typeof(Material)) as Material;
                SetMaterial(standardMaterial);
                break;
            case TargetType.Heavy:
                totalHitpoints = 5;
                scorePoints = 5;
                canvas.SetActive(true);
                Material heavyMaterial = Resources.Load("Material/Target Heavy Material", typeof(Material)) as Material;
                SetMaterial(heavyMaterial);
                break;
        }
    }

    private void SetMaterial(Material material) {
        foreach(Transform child in transform) {
            child.gameObject.GetComponent<Renderer>().material = material;
        }
    }

    private bool MatchColorType(ColorType colorType) {
        if(this.colorType == ColorType.None) { return true; }
        return colorType == this.colorType;
    }

}
