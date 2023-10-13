using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public class PlayerMovement : MonoBehaviour
{
    //I recommend 7 for the move speed, and 1.2 for the force damping
    public Rigidbody2D rb;
    public float moveSpeed;
    public Vector2 PlayerInput;
    void Update()
    {
        PlayerInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;
    }
    void FixedUpdate()
    {
        Vector2 moveForce = PlayerInput * moveSpeed;
        rb.velocity = moveForce;
    }
 
}