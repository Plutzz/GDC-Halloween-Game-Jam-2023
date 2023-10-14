using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

public class PlayerMovement : Singleton<PlayerMovement>
{
    public Rigidbody2D rb;
    public float moveSpeed;
    public Vector2 PlayerInput;
    public GameObject Graphics;
    public Animator Anim;
    void Update()
    {
        PlayerInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;
    }
    void FixedUpdate()
    {
        Vector2 moveForce = PlayerInput * moveSpeed;
        rb.velocity = moveForce;

        Anim.SetFloat("XSpeed", Mathf.Abs(rb.velocity.x));
        Anim.SetFloat("YVelocity", rb.velocity.y);

        if(rb.velocity.x > 0)
        {
            Graphics.transform.localScale = new Vector3(1 ,1 ,1);
        }
        else if(rb.velocity.x < 0)
        {
            Graphics.transform.localScale = new Vector3(-1, 1, 1);
        }
    }
 
}