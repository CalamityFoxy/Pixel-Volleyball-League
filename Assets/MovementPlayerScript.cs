using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementPlayerScript : MonoBehaviour
{

    [SerializeField] private float fuerzaDeSalto = 7f;

    private BallHit ballHit;
    private Rigidbody2D rb1;
    private bool IsJumping1 = false;
    private bool TouchingWall1 = false;
    private bool TeclaParaArmar1 = false;
    public bool teclaParaArmar1 => TeclaParaArmar1;

    void Start()
    {
        rb1 = GetComponent<Rigidbody2D>();
        ballHit = FindObjectOfType<BallHit>();
    }

    void Update()
    {

        if(Input.GetKeyDown(KeyCode.Space) && ballHit.PelotaEnRadio())
        {
            
              TeclaParaArmar1 =true; 
        }
       
        if(TouchingWall1 ==false)
        {

            if (Input.GetKey(KeyCode.RightArrow))
            {
                rb1.velocity = new Vector2(5, rb1.velocity.y);
            }


        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            rb1.velocity = new Vector2(-5, rb1.velocity.y);
        }


        if (TouchingWall1 == true)
        {
            rb1.velocity = new Vector2(rb1.velocity.x, Mathf.Clamp(rb1.velocity.y, -0.75f, float.MaxValue));

        }



        if (Input.GetKeyDown(KeyCode.UpArrow) && !IsJumping1)
        {
            rb1.AddForce(Vector2.up * fuerzaDeSalto, ForceMode2D.Impulse);
            IsJumping1 = true;
        }

    }
    


    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.CompareTag("Ground"))
        {
            IsJumping1 = false;
        }
        if (collision.gameObject.CompareTag("Wall"))
        {
            TouchingWall1 = true;


        }

    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            TouchingWall1 = false;


        }
    }
    public void ResetTeclaParaArmar()
    {
        TeclaParaArmar1 = false;
    }

}
