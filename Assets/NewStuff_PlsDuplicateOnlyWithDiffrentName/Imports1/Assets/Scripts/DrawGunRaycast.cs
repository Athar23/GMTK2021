using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawGunRaycast : MonoBehaviour
{
    LineRenderer lineRenderer;
    public Transform shootPos;
    public float length = 5.0f;

    private void Awake()
    {
        lineRenderer = this.GetComponent<LineRenderer>();
    }

    private void Update()
    {
        lineRenderer.SetPosition(0, shootPos.position);
        lineRenderer.SetPosition(1, shootPos.position + shootPos.up * length);
    }
}
