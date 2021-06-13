using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMotion : MonoBehaviour
{
    public float moveSpeed = 5.0f;

    Rigidbody2D rig;
    Camera cam;

    Vector2 moveDir = new Vector2();
    Quaternion rotateDegree = new Quaternion();

    Vector2 MousePos = new Vector3();

    private void Awake()
    {
        rig = this.GetComponent<Rigidbody2D>();
        cam = Camera.main;
    }

    private void Update()
    {
        moveDir.x = Input.GetAxis("Horizontal");
        moveDir.y = Input.GetAxis("Vertical");

        MousePos = Input.mousePosition;
        MousePos = cam.ScreenToWorldPoint(MousePos);

        rotateDegree = Quaternion.LookRotation(MousePos - rig.position, -rig.transform.forward);
    }


    private void FixedUpdate()
    {
        // move
        rig.MovePosition(rig.position + moveDir * moveSpeed * Time.fixedDeltaTime);
        // rot
        rig.MoveRotation(rotateDegree);      
    }
}
