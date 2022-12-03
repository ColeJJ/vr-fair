using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireTestBullet : MonoBehaviour
{
    public GameObject bullet;
    public Transform spawnPoint;
    public float fireSpeed = 20;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // Test input for the Game Scene
        if (Input.GetMouseButtonDown(0)) {
            FireBullet();
        }
    }

    private void FireBullet()
    {
        GameObject spawnedBullet = Instantiate(bullet);
        spawnedBullet.transform.position = spawnPoint.position;
        spawnedBullet.GetComponent<Rigidbody>().velocity = spawnPoint.forward * fireSpeed;
        Destroy(spawnedBullet, 2);
    }
}
