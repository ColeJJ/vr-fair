using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

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
    public TargetState state = TargetState.Up;
    public int totalHitpoints = 1;
    public int scorePoints;

    public GameObject canvas;
    public Slider slider;

    // parts of the target
    public GameObject hitArea;
    public GameObject stem;
    public GameObject colorDisplay;

    public Renderer hitAreaRenderer;
    public Renderer stemRenderer;
    public Renderer colorDisplayRenderer;

    public AudioSource [] mTargetSounds;

    private HingeJoint targetJoint;
    private Rigidbody rb;
    private TMP_Text hitpointText;
    private ColorType colorType = ColorType.None;
    private TargetType targetType = TargetType.Normal;
    private int hitpoints;
    private int scorePointsMultiplier = 1;

    private AudioSource mAudioSrc;

    void Start()
    {
        mTargetSounds = GetComponents<AudioSource>();

        targetJoint = GetComponent<HingeJoint>();
        rb = GetComponent<Rigidbody>();

        hitpoints = totalHitpoints;

        hitArea = this.gameObject.transform.GetChild(0).gameObject;
        stem = this.gameObject.transform.GetChild(1).gameObject;
        colorDisplay = this.gameObject.transform.GetChild(2).gameObject;

        hitAreaRenderer = hitArea.GetComponent<Renderer>();
        stemRenderer = stem.GetComponent<Renderer>();
        colorDisplayRenderer = colorDisplay.GetComponent<Renderer>();
        colorDisplay.SetActive(false);

        canvas = this.gameObject.transform.GetChild(3).gameObject;
        slider = canvas.transform.GetChild(0).GetComponent<Slider>();
        canvas.SetActive(false);
    }

    void Update()
    {
        if(transform.localRotation.eulerAngles.z < 0.1) {
            targetJoint.useSpring = false;
            rb.isKinematic = hitpoints > 1;
            state = TargetState.Up;
        } else if(transform.localRotation.eulerAngles.z > 89.9) {
            targetJoint.useMotor = false;
            state = TargetState.Down;
        }

        if (canvas.activeSelf) {
            slider.value = hitpoints > 0 ? hitpoints : 0;
            // hitpointText.text = hitpoints > 0 ? hitpoints.ToString() : "";
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag != "Bullet" || state != TargetState.Up) { return; }
        mAudioSrc.Play();

        BulletManager bulletManager = collision.gameObject.GetComponent<BulletManager>();
        if(!MatchColorType(bulletManager.colorType)) { return; }

        hitpoints -= 1;

        if(hitpoints == 0) {
            state = TargetState.Hit;
            scoreboardManager.UpdateScore(scorePoints * scorePointsMultiplier);
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
                   colorDisplayRenderer.material = greenMaterial;
                   colorDisplay.SetActive(true);
                   break;
               case ColorType.Red:
                   Material redMaterial = Resources.Load("Material/Color Type Red", typeof(Material)) as Material;
                   colorDisplayRenderer.material = redMaterial;
                   colorDisplay.SetActive(true);
                   break;
           }
    }

    public void SetTargetType(TargetType type) {
        this.targetType = type;
        switch(type) {
            case TargetType.Normal:
                mAudioSrc = mTargetSounds[1];

                totalHitpoints = 1;
                scorePointsMultiplier = 1;

                canvas.SetActive(false);

                Material standardMaterial = Resources.Load("Material/Target Standard Material", typeof(Material)) as Material;
                hitAreaRenderer.material = standardMaterial;
                stemRenderer.material = standardMaterial;
                
                break;
            case TargetType.Heavy:
                mAudioSrc = mTargetSounds[0];

                totalHitpoints = 5;
                scorePointsMultiplier = 5;

                canvas.SetActive(true);
                slider.value = totalHitpoints;

                Material heavyMaterial = Resources.Load("Material/Target Heavy Material", typeof(Material)) as Material;
                hitAreaRenderer.material = heavyMaterial;
                stemRenderer.material = heavyMaterial;
                
                break;
        }
    }

    public void Reset() {
        SetTargetType(TargetType.Normal);
        UpdateTargetColorType(ColorType.None);
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
