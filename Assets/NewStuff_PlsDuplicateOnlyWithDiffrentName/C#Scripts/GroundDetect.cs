using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundDetect : MonoBehaviour
{
    public PlayerMovement pm;
    public RayToMouse rtm;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter2D(Collider2D other) 
    {
        switch (other.gameObject.tag)
        {
            case "Movable":
            rtm.activated = false; break;
            case "Ground":
            pm.OnGround = true; break;
            default:
            break;
        }
    }
    void OnTriggerExit2D(Collider2D other) 
    {
        switch (other.gameObject.tag)
        {
            case "Ground":
            pm.OnGround = false; break;
            default:
            break;
        }
    }
}
