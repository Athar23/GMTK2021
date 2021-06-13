using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChainManagerSample : MonoBehaviour
{
    /// <summary>
    /// There are two Public Function in Detent
    /// 1.DrawLength(Vector2 l,Vector2 r)   // Generate chain within two Point
    /// 2.Clear()                           // Clear the chain
    /// </summary>

    public Transform left;
    public Transform right;
    public Detent detent;

    void Update()
    {
        detent.DrawLength(left.position, right.position);
    }
}
