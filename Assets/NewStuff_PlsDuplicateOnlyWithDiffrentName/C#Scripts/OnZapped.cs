using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnZapped : MonoBehaviour
{
    public bool hitted;
    private Rigidbody2D rb;
    public bool activatable;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void MoveIntoPosition(Vector2 position)
    {
        if (activatable)
        {
            rb.MovePosition(position);
        }
    }
    void OnMouseEnter() 
    {
        activatable = true;
    }
    void OnMouseExit() 
    {
        activatable = false;
    }
    //int r = 0;
    //Vector2 direction = new Vector2(position.x - rb.position.x,position.y - rb.position.y);
    //while (Physics2D.Raycast(rb.position,direction,direction.magnitude,lm) && r < 200)
    //{
    //    position -= direction.normalized * 0.1f;
    //    direction = new Vector2(position.x - rb.position.x,position.y - rb.position.y);
    //    r++;
    //    if (r == 120)
    //    {
    //        Debug.Log("Major Shift Change");
    //    }
    //}
}
