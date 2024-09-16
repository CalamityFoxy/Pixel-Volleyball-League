using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallHit : MonoBehaviour
{
    private Rigidbody2D rbBall;
    private MovementPlayerScript player;
    private bool pelotaEnRadio = false;
    public bool PelotaEnRadio()
    {
        return pelotaEnRadio;
    }

    void Start()
    {
        rbBall = GetComponent<Rigidbody2D>();
        player = GameObject.FindWithTag("Player1").GetComponent<MovementPlayerScript>();


    }
    

    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Hitbox"))
        {

            //////////////PLAYER UNO///////////////////
           /* rbBall.velocity = new Vector2(-10, 10);
            Debug.Log("ColisionaConPlayer1");*/
            
        }
        if (collision.gameObject.CompareTag("Hitbox"))
        {
            pelotaEnRadio=true;
            Debug.Log("ColisionaConPlayer1");

        }



              /////////////PLAYER 2 ///////////////////////////////
        if (collision.gameObject.CompareTag("Hitbox2"))//player2
        {
            rbBall.velocity = new Vector2(10, 10);
            Debug.Log("ColisionaConPlayer2");

        }

    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Hitbox") && (player.teclaParaArmar1))
        {
            rbBall.velocity = new Vector2(0, 10);
            Debug.Log("Le Pegaste a la pelota");
            player.ResetTeclaParaArmar();

        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Hitbox"))
        {
            pelotaEnRadio = false;
        }
    }



    public void Update()
    {
      //  Debug.Log(rbBall.velocity);
    }

}
