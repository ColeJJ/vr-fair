using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class AnimateHandOnInput : MonoBehaviour
{
    public InputActionProperty pinchAnimationAction; 
    public InputActionProperty gripAnimationAction;
    public Animator handAnimator;
    bool gripToggle = false;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float triggerValue = pinchAnimationAction.action.ReadValue<float>();
        handAnimator.SetFloat("Trigger", triggerValue);

        if(gripAnimationAction.action.triggered) {
            gripToggle = !gripToggle;
            handAnimator.SetFloat("Grip", Convert.ToSingle(gripToggle));
        }

    }
}
