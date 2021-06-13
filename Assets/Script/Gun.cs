using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public TestChanManager chainManager;

    public GameObject bulletPrefab;
    public Transform shootPoint;
    public float ShootForce = 0.2f;

    public Transform GunObj;

    Camera cam;
    Quaternion rotateDegree = new Quaternion();
    Vector3 MousePos = new Vector3();

    private void Awake()
    {
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {       
        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }
        if (Input.GetMouseButtonDown(1))
        {

        }


        // rot
        MousePos = Input.mousePosition;
        MousePos = cam.ScreenToWorldPoint(MousePos);

        rotateDegree = Quaternion.LookRotation(MousePos - transform.position, transform.transform.forward);
        GunObj.rotation = rotateDegree;
    }

    void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, shootPoint.position, transform.rotation);
        Rigidbody2D rig = bullet.GetComponent<Rigidbody2D>();
        
        rig.AddForce(shootPoint.up * ShootForce, ForceMode2D.Impulse);
        chainManager.UpdateChainPos(bullet.transform);
    }
}
