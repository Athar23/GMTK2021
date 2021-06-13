using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSimpleFollow : MonoBehaviour
{
    public Transform followTarget;
    [Tooltip("Move smoothly")]
    public float damping = 0.02f;

    Vector3 offset = new Vector3();

    private void Start()
    {
        offset = transform.position - followTarget.position;
    }

    private void FixedUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, followTarget.position + offset, damping);
    }
}
