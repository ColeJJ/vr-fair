using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

[System.Serializable]
public class HapticTrigger
{

    [Range(0, 1)]
    public float hapticIntensity;
    public float hapticDuration;

    public void TriggerHaptic(BaseInteractionEventArgs eventArgs) {
        if(eventArgs.interactorObject is XRBaseControllerInteractor controllerInteractor && hapticIntensity > 0) {
            controllerInteractor.xrController.SendHapticImpulse(hapticIntensity, hapticDuration);
        }
    }
}
