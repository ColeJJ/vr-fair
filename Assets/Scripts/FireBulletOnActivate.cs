using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class FireBulletOnActivate : MonoBehaviour
{

    public GameObject bullet;
    public Transform spawnPoint;
    public float fireSpeed = 20;
    private AudioSource mAudioSrc;

    public HapticTrigger activatedHapticTrigger;
    public HapticTrigger hoverEnteredHapticTrigger;

    // Start is called before the first frame update
    void Start()
    {
        XRGrabInteractable grabbable = GetComponent<XRGrabInteractable>();
        XRBaseInteractable interactable = GetComponent<XRBaseInteractable>();
        grabbable.activated.AddListener(FireBullet);
        grabbable.activated.AddListener(activatedHapticTrigger.TriggerHaptic);
        grabbable.hoverEntered.AddListener(hoverEnteredHapticTrigger.TriggerHaptic);
        // interactable.activated.AddListener(TriggerHaptic);
        mAudioSrc = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FireBullet(ActivateEventArgs arg)
    {
        mAudioSrc.Play();
        GameObject spawnedBullet = Instantiate(bullet);
        spawnedBullet.transform.position = spawnPoint.position;
        spawnedBullet.GetComponent<Rigidbody>().velocity = spawnPoint.forward * fireSpeed;
        Destroy(spawnedBullet, 5);
    }

}
