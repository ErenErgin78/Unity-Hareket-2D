using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class Hareket : MonoBehaviour
{
    private Rigidbody2D rb;
    public float speed;
    public float jumppower;
    public Vector2 boxsize;
    float Horizontal;
    float Vertical;
    public float distance;
    public LayerMask LayerMask;
    private bool rb_x;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        speed = 0.2f;
        jumppower = 60f;
        
        
        
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawCube(transform.position-transform.up*distance,boxsize);
       
    }
    

    private void Update()
    {
        GroundCheck();
        Horizontal = Input.GetAxis("Horizontal");
        Vertical = Input.GetAxis("Vertical");



        if (rb.velocity.x == 0) { rb_x = true; }
        else { rb_x = false; }

        if (!GroundCheck() && rb.velocity.y<=1) { rb.gravityScale = 5f; speed = 0.2f; } //düþerken
        else if (!GroundCheck() && rb.velocity.y > 0) { speed = 0.1f; } //zýplarken
        else if (!rb_x && GroundCheck()) { rb.gravityScale = 0f; speed = 0.3f; } //büyük zýplama 
        else if(rb_x && GroundCheck()) { rb.gravityScale = 0f; speed = 0.3f; }  //zýplamadan hareket
        
       
      
        if (Horizontal > 0.1f || Vertical < 0.1)
        {
            rb.AddForce(new Vector2(Horizontal * speed, 0f), ForceMode2D.Impulse);
        }


        if (Input.GetKey(KeyCode.Space)&&GroundCheck())
        {
            rb.AddForce(new Vector2(0f, jumppower), ForceMode2D.Impulse);
        }        
    }
    private bool GroundCheck() 
    {

        if (Physics2D.BoxCast(transform.position, boxsize, 0, -transform.up, distance, LayerMask))
        {return true;}
        else
        {return false;}
    }
}
