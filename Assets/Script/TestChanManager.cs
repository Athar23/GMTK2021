using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestChanManager : MonoBehaviour
{
    public Detent detent;
    public Transform shootPoint;

    Transform leftTarget = null;
    Transform rightTarget = null;
    [HideInInspector]
    public Vector3 left = new Vector3();
    [HideInInspector]
    public Vector3 right = new Vector3();

    int counter = 0;
    void Update()
    {
        switch (counter)
        {
            case 0:
                return;
            case 1:
                if (rightTarget != null)
                {
                    detent.DrawLength(leftTarget.position, rightTarget.position);
                    right = rightTarget.position;
                }                  
                else
                    detent.DrawLength(leftTarget.position, right);
                break;
            case 2:
                if (leftTarget != null)
                {
                    detent.DrawLength(leftTarget.position, right);
                    left = leftTarget.position;
                }
                else
                {
                    detent.DrawLength(left, right);
                }
                    
                break;
            default:
                break;
        }
        
    }

    public void ClearChain()
    {
        detent.Clear();
    }

    public void UpdateChainPos(Transform curBullet)
    {
        counter += 1;
        if (counter > 2)
        {
            counter = 0;
        }
        switch (counter)
        {
            case 0:
                ClearChain();
                return;
            case 1:
                leftTarget = shootPoint;
                rightTarget = curBullet;
                break;
            case 2:
                leftTarget = curBullet;
                break;
            default:
                break;
        }
    } 
}
