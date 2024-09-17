using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementPlayer2Script : MonoBehaviour
{


    private float fuerzaDeSalto = 12f;

    private BallHit ballHit;
    private Rigidbody2D rb2;
    private bool IsJumping2 = false;
    private bool TouchingWall2 = false;
    private bool TouchingNet2 = false;
    private bool TeclaParaArmar2 = false;
    private bool TeclaParaPegar2 = false;
    private bool TeclaParaRematar2 = false;
    private float tiempoDeEnfriamiento = 0.5f;
    private float tiempoUltimaActivacion = 0f;

    public bool teclaParaArmar2 => TeclaParaArmar2;
    public bool teclaParaPegar2 => TeclaParaPegar2;
    public bool teclaParaRematar2 => TeclaParaRematar2;
    void Start()
    {
        rb2 = GetComponent<Rigidbody2D>();
        ballHit = FindObjectOfType<BallHit>();
    }

    void Update()
    {

        if (Input.GetKey(KeyCode.S) && ballHit.PelotaEnRadio2())
        {
            if (Time.time >= tiempoUltimaActivacion + tiempoDeEnfriamiento)
            {
                TeclaParaArmar2 = true;
                tiempoUltimaActivacion = Time.time;
            }
        }
        if (Input.GetKey(KeyCode.Space) && !IsJumping2 && ballHit.PelotaEnRadio2())
        {
            if (Time.time >= tiempoUltimaActivacion + tiempoDeEnfriamiento)
            {
                TeclaParaPegar2 = true;
                tiempoUltimaActivacion = Time.time;
            }

        }
        if (Input.GetKey(KeyCode.Space) && IsJumping2 && ballHit.PelotaEnRadio2())
        {
            if (Time.time >= tiempoUltimaActivacion + tiempoDeEnfriamiento)
            {
                TeclaParaRematar2 = true;
                tiempoUltimaActivacion = Time.time;
            }
        }



        if (TouchingWall2 == false)
        {

            if (Input.GetKey(KeyCode.A))
            {
                rb2.velocity = new Vector2(-5, rb2.velocity.y);
            }


        }
        if (TouchingNet2 == false)
        {
            if (Input.GetKey(KeyCode.D))
            {
                rb2.velocity = new Vector2(5, rb2.velocity.y);
            }
        }




        if (TouchingWall2 == true)
        {
            rb2.velocity = new Vector2(rb2.velocity.x, Mathf.Clamp(rb2.velocity.y, -0.75f, float.MaxValue));

        }


        if (Input.GetKeyDown(KeyCode.W) && !IsJumping2)
        {
            rb2.AddForce(Vector2.up * fuerzaDeSalto, ForceMode2D.Impulse);
            IsJumping2 = true;
        }
        Debug.Log(fuerzaDeSalto);

    }



    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.CompareTag("Ground"))
        {
            IsJumping2 = false;
        }
        if (collision.gameObject.CompareTag("Wall"))
        {
            TouchingWall2 = true;


        }
        if (collision.gameObject.CompareTag("Net"))
        {

            TouchingNet2 = true;

        }

    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            TouchingWall2 = false;


        }
        if (collision.gameObject.CompareTag("Net"))
        {

            TouchingNet2 = false;

        }
    }
    public void ResetTeclaParaArmar2()
    {
        TeclaParaArmar2 = false;
    }
    public void ResetTeclaParaPegar2()
    {
        TeclaParaPegar2 = false;
    }
    public void ResetTeclaParaRematar2()
    {
        TeclaParaRematar2 = false;
    }


}
