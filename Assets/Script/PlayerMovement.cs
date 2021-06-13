using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    public CharacterController controller;
    public Animator animator;
    public bool OnGround;
    public float speed=200f;
    public float Xmove=0f;
    public float jumpForce;
    public bool jumpCooldown;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        jumpCooldown = true;
    }
    void Update()
    {
        Xmove=Input.GetAxisRaw("Horizontal");
        Debug.Log(Xmove);
        animator.SetFloat("Velocity",Mathf.Abs(Xmove));
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        Movement();
        controller.Move(Xmove,false,false);
    }
    void Movement()
    {
        
        rb.velocity = new Vector2(speed * Input.GetAxisRaw("Horizontal") * Time.fixedDeltaTime,rb.velocity.y);
        if (OnGround && jumpCooldown)
        {
            rb.AddForce(new Vector2(0,Input.GetAxisRaw("Jump") * jumpForce), ForceMode2D.Impulse);
        }
    }
    IEnumerator CoolDown()
    {
        jumpCooldown = false;
        yield return new WaitForSeconds(0.1f);
        jumpCooldown = true;
    }
}
