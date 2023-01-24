using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.InputSystem;

public class ActivateTeleportationRay : MonoBehaviour
{

    public GameObject teleportationRay;
    public InputActionProperty activateAction;
    
    // Update is called once per frame
    void Update()
    {
        teleportationRay.SetActive(activateAction.action.ReadValue<float>() >= 0.1f); 
    }
}
