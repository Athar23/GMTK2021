using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform shootPos;
    public float ShootForce = 0.2f;



    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, shootPos.position, transform.rotation);
        Rigidbody2D rig = bullet.GetComponent<Rigidbody2D>();

        rig.AddForce(shootPos.up * ShootForce,ForceMode2D.Impulse);
    }
}
