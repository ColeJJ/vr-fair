using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireTestBullet : MonoBehaviour
{
    public GameObject bullet;
    public Transform spawnPoint;
    public float fireSpeed = 20;
    private bool colorToggle = false;

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

        if (Input.GetMouseButtonDown(1)) {
            colorToggle = !colorToggle;
        }
    }

    private void FireBullet()
    {
        GameObject spawnedBullet = Instantiate(bullet);
        BulletManager bulletManager = spawnedBullet.GetComponent<BulletManager>();
        bulletManager.SetColorType(colorToggle ? ColorType.Red : ColorType.Green);

        spawnedBullet.transform.position = spawnPoint.position;
        spawnedBullet.GetComponent<Rigidbody>().velocity = spawnPoint.forward * fireSpeed;
        Destroy(spawnedBullet, 2);
    }
}
