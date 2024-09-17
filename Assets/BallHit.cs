using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallHit : MonoBehaviour
{
    private Rigidbody2D rbBall;
    private MovementPlayerScript player;
    private MovementPlayer2Script player2;
    private bool pelotaEnRadio1 = false;
    private bool pelotaEnRadio2 = false;
    public bool PelotaEnRadio()
    {
        return pelotaEnRadio1;
    }
    public bool PelotaEnRadio2()
    {
        return pelotaEnRadio2;
    }
    void Start()
    {
        rbBall = GetComponent<Rigidbody2D>();
        player = GameObject.FindWithTag("Player1").GetComponent<MovementPlayerScript>();
        player2 = GameObject.FindWithTag("Player2").GetComponent<MovementPlayer2Script>();

    }



    private void OnTriggerEnter2D(Collider2D collision)
    {

        ////////PLAYER 1 ///////////////////
        if (collision.gameObject.CompareTag("Hitbox"))
        {
            pelotaEnRadio1 = true;
            Debug.Log("ColisionaConPlayer1");

        }


        /////////////PLAYER 2 ///////////////////////////////
        if (collision.gameObject.CompareTag("Hitbox2"))
        {
            pelotaEnRadio2 = true;
            Debug.Log("ColisionaConPlayer2");

        }

    }
    private void OnTriggerStay2D(Collider2D collision)
    {

        //////PLAYER 1 /////////////////////
        if (collision.CompareTag("Hitbox") && (player.teclaParaArmar1) )
        {
            rbBall.velocity = new Vector2(0, 15);


            player.ResetTeclaParaArmar();
            Debug.Log("Armado");

        }
        if (collision.CompareTag("Hitbox") && (player.teclaParaPegar1))
        {
            rbBall.velocity = new Vector2(-10, 10);
            player.ResetTeclaParaPegar();
            Debug.Log("Golpe Basico");

        }
        if (collision.CompareTag("Hitbox") && (player.teclaParaRematar1))
        {
            rbBall.velocity = new Vector2(-15, -15);
            Debug.Log("Remate");
            player.ResetTeclaParaRematar();

        }


        /////////PLAYER 2 ///////////////////
        if (collision.CompareTag("Hitbox2") && (player2.teclaParaArmar2))
        {
            rbBall.velocity = new Vector2(0, 15);


            player2.ResetTeclaParaArmar2();
            Debug.Log("Armado");

        }
        if (collision.CompareTag("Hitbox2") && (player2.teclaParaPegar2))
        {
            rbBall.velocity = new Vector2(10, 10);
            player2.ResetTeclaParaPegar2();
            Debug.Log("Golpe Basico");

        }
        if (collision.CompareTag("Hitbox2") && (player2.teclaParaRematar2))
        {
            rbBall.velocity = new Vector2(15, -15);
            Debug.Log("Remate");
            player2.ResetTeclaParaRematar2();

        }


    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Hitbox"))
        {
            pelotaEnRadio1= false;
        }


    }


    public void Update()
    {
        
        
        




    }

}