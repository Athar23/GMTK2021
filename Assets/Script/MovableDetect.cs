using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovableDetect : MonoBehaviour
{
    public RayToMouse rtm;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerStay2D(Collider2D other) 
    {
        switch (other.gameObject.tag)
        {
            case "Movable":
            rtm.activated = false; break;
            default:
            break;
        }
    }
}
