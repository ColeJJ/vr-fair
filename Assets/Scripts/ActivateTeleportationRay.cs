using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.InputSystem;

public class ActivateTeleportationRay : MonoBehaviour
{

    public GameObject rightTeleportation;
    public InputActionProperty rightActivate;
    public InputActionProperty rightCancel;
    public XRRayInteractor leftRay;

    // Update is called once per frame
    void Update()
    {
        // hint: we dont need to do this here cause we have teleportation on right and ray interaction on left
        // bool isLeftRayHovering = leftRay.TryGetHitInfo(out Vector3 leftPos, out Vector3 leftNormal, out int leftNumber, out bool leftValid);
        rightTeleportation.SetActive(rightCancel.action.ReadValue<float>() == 0 && rightActivate.action.ReadValue<float>() > 0.1f);       
    }
}
