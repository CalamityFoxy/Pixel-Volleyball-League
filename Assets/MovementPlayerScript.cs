using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class MovementPlayerScript : MonoBehaviour
{

    private float fuerzaDeSalto = 12f;

    private BallHit ballHit;
    private Rigidbody2D rb1;
    private bool IsJumping1 = false;
    private bool TouchingWall1 = false;
    private bool TouchingNet1 = false;
    private bool TeclaParaArmar1 = false;
    private bool TeclaParaPegar1 = false;
    private bool TeclaParaRematar1 = false;
    private float tiempoDeEnfriamiento = 0.5f;
    private float tiempoUltimaActivacion = 0f;
    
    public bool teclaParaArmar1 => TeclaParaArmar1;
    public bool teclaParaPegar1 => TeclaParaPegar1;
    public bool teclaParaRematar1 => TeclaParaRematar1;
    void Start()
    {
        rb1 = GetComponent<Rigidbody2D>();
        ballHit = FindObjectOfType<BallHit>();
    }

    void Update()
    {

        if (Input.GetKey(KeyCode.RightShift) && Input.GetKey(KeyCode.DownArrow) && ballHit.PelotaEnRadio())
        {
            if (Time.time >= tiempoUltimaActivacion + tiempoDeEnfriamiento)
            {
                TeclaParaArmar1 = true;
                tiempoUltimaActivacion = Time.time;
            }
        }
        if (Input.GetKey(KeyCode.RightShift) && !IsJumping1 && ballHit.PelotaEnRadio())
        {
            if (Time.time >= tiempoUltimaActivacion + tiempoDeEnfriamiento)
            {
                TeclaParaPegar1 = true;
                tiempoUltimaActivacion = Time.time;
            }

        }
        if (Input.GetKey(KeyCode.Space) && IsJumping1 && ballHit.PelotaEnRadio())
        {
            if (Time.time >= tiempoUltimaActivacion + tiempoDeEnfriamiento)
            {
                TeclaParaRematar1 = true;
                tiempoUltimaActivacion = Time.time;
            }
        }



        if (TouchingWall1 == false)
        {

            if (Input.GetKey(KeyCode.RightArrow))
            {
                rb1.velocity = new Vector2(5, rb1.velocity.y);
            }


        }
        if (TouchingNet1 == false)
        {
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                rb1.velocity = new Vector2(-5, rb1.velocity.y);
            }
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
        Debug.Log(fuerzaDeSalto);
        
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
        if (collision.gameObject.CompareTag("Net"))
        {
            
            TouchingNet1 = true;

        }

    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            TouchingWall1 = false;


        }
        if (collision.gameObject.CompareTag("Net"))
        {

            TouchingNet1 = false;

        }
    }
    public void ResetTeclaParaArmar()
    {
        TeclaParaArmar1 = false;
    }
    public void ResetTeclaParaPegar()
    {
        TeclaParaPegar1 = false;
    }
    public void ResetTeclaParaRematar()
    {
        TeclaParaRematar1 = false;
    }

}