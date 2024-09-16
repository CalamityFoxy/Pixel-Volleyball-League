using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementPlayer2Script : MonoBehaviour
{
    
    
    private Rigidbody2D rb2;
    private bool IsJumping2 = false;
    [SerializeField] private float fuerzaSalto = 7f;
    private bool TouchingWall2 = false;
    
    void Start()
    {
        rb2 = GetComponent<Rigidbody2D>();
    }

    void Update()
    {



        if (TouchingWall2==false)
        {
            if (Input.GetKey(KeyCode.A))
            {
                rb2.velocity = new Vector2(-5, rb2.velocity.y);
            }

            

        }
        if (Input.GetKey(KeyCode.D))
        {
            rb2.velocity = new Vector2(5, rb2.velocity.y);
        }

        if (Input.GetKeyDown(KeyCode.W) && !IsJumping2)  
        {
            rb2.AddForce(Vector2.up * fuerzaSalto, ForceMode2D.Impulse);
            IsJumping2 = true; 
        }
        
        

        if(TouchingWall2==true)
        {
            rb2.velocity = new Vector2(rb2.velocity.x, Mathf.Clamp(rb2.velocity.y, -0.75f, float.MaxValue));

        }
            
    }

    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            IsJumping2 = false; 
        }

        if(collision.gameObject.CompareTag("Wall"))
        {
            TouchingWall2 = true;

            
        }
       
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            TouchingWall2 = false;


        }
    }


}
