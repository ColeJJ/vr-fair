using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class PistolManager : MonoBehaviour
{

    public ColorType colorType;
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
        grabbable.activated.AddListener(FireBullet);
        grabbable.activated.AddListener(activatedHapticTrigger.TriggerHaptic);
        grabbable.hoverEntered.AddListener(hoverEnteredHapticTrigger.TriggerHaptic);
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
        BulletManager bulletManager = spawnedBullet.GetComponent<BulletManager>();
        bulletManager.SetColorType(colorType);

        spawnedBullet.transform.position = spawnPoint.position;
        spawnedBullet.GetComponent<Rigidbody>().velocity = spawnPoint.forward * fireSpeed;
        Destroy(spawnedBullet, 5);
    }
}
