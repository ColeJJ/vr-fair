using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.InputSystem;

public class ActivateTeleportationRay : MonoBehaviour
{

    public GameObject rightTeleportation;
    public GameObject leftInteractionRay;
    public InputActionProperty rightActivate;
    public InputActionProperty rightCancel;
    public InputActionProperty leftCancel;
    public InputActionProperty leftActive;

    // Update is called once per frame
    void Update()
    {
        rightTeleportation.SetActive(rightCancel.action.ReadValue<float>() == 0 && rightActivate.action.ReadValue<float>() > 0.1f);       
        leftInteractionRay.SetActive(leftCancel.action.ReadValue<float>() == 0 && leftActive.action.ReadValue<float>() > 0.1f);       
    }
}
