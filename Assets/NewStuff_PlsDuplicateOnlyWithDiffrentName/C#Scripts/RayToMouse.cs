using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayToMouse : MonoBehaviour
{
    private LineRenderer lr;
    public bool activated;
    private Transform tr;
    private RaycastHit2D hit;
    public Transform rayPosition;
    // Start is called before the first frame update
    void Start()
    {
        lr = GetComponent<LineRenderer>();
        tr = GetComponent<Transform>();
        tr.parent = null;
    }

    // Update is called once per frame
    void Update()
    {
        tr.position = new Vector3(rayPosition.position.x,rayPosition.position.y,0f);
        if (Input.GetMouseButtonDown(0))
        {
            if (activated)
            {
                activated = false;
            }
            else
            {
                activated = true;
            }
        }
        CastRay();
    }
    void CastRay()
    {
        lr.SetPosition(0, tr.position);
        if (activated)
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            hit = Physics2D.Raycast(rayPosition.position, new Vector2(mousePos.x - tr.position.x,mousePos.y - tr.position.y), 200);
            if (hit.transform != null)
            {
                //It did hit something
                lr.SetPosition(1, new Vector3(hit.point.x, hit.point.y, 0f));
                switch (hit.transform.gameObject.tag)
                {
                    case "Movable":
                    hit.transform.gameObject.GetComponent<OnZapped>().MoveIntoPosition(new Vector2(mousePos.x, mousePos.y)); break;
                    default:
                    break;
                }
            }
            else
            {
                //it did not
                lr.SetPosition(1, new Vector3(mousePos.x, mousePos.y, 0f));
            }
        }
        else
        {
            lr.SetPosition(1, tr.position);
        }
    }
}
