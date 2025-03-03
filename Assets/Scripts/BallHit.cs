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
    private bool Hitplayer1 = false;
    private bool Hitplayer2 = false;
    
    
    public bool PelotaEnRadio()
    {
        return pelotaEnRadio1;
    }
    public bool PelotaEnRadio2()
    {
        return pelotaEnRadio2;
    }

    public GameManager gameManager; // Referencia al GameManager

    void Start()
    {
        rbBall = GetComponent<Rigidbody2D>();
        player = GameObject.FindWithTag("Player1").GetComponent<MovementPlayerScript>();
        player2 = GameObject.FindWithTag("Player2").GetComponent<MovementPlayer2Script>();
    }
    private void Update()
    {
        // Debug.Log(rbBall.gravityScale);
        
        if (rbBall.transform.position.x > 0)
        {
            Hitplayer2 = false;
          //  Debug.Log(Hitplayer2);
        }
        if (rbBall.transform.position.x < 0)
        {
            Hitplayer1 = false;
           // Debug.Log(Hitplayer1);
        }

    }
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player1"))
        {
            Hitplayer1 = true;
            

        }

        if (collision.gameObject.CompareTag("Player2"))
        {
            Hitplayer2 = true;
            
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
      {
          // PLAYER 1
          if (collision.gameObject.CompareTag("Hitbox"))
          {
              pelotaEnRadio1 = true;
              //Debug.Log("ColisionaConPlayer1");
          }

          // PLAYER 2
          if (collision.gameObject.CompareTag("Hitbox2"))
          {
              pelotaEnRadio2 = true;
              // Debug.Log("ColisionaConPlayer2");
          }

          // Detecta cuando la pelota entra en la zona de gol de un jugador
          if (collision.gameObject.CompareTag("Player1Goal"))
          {
              gameManager.PlayerScored(2); // Jugador 2 anot� un punto
          }
          if (collision.gameObject.CompareTag("Player2Goal"))
          {
              gameManager.PlayerScored(1); // Jugador 1 anot� un punto
          }
        if (collision.gameObject.CompareTag("Player3Goal") && Hitplayer1 == true)
        {
            gameManager.PlayerScored(2); // Jugador 2 anot� un punto
            
        }
        if (collision.gameObject.CompareTag("Player4Goal") && Hitplayer2 == true) 
        {
            gameManager.PlayerScored(1); // Jugador 1 anot� un punto
            
             
        }
        if (collision.gameObject.CompareTag("Player3Goal") && Hitplayer1 == false)
        {
            gameManager.PlayerScored(1); // Jugador 2 anot� un punto
            
        }
        if (collision.gameObject.CompareTag("Player4Goal") && Hitplayer2 == false)
        {
            gameManager.PlayerScored(2); // Jugador 1 anot� un punto
            

        }
    }

     private void OnTriggerStay2D(Collider2D collision)
     {
         // PLAYER 1
         if (collision.CompareTag("Hitbox") && player.teclaParaArmar1)
         {
            rbBall.velocity = new Vector2(-3, 30);


           player.ResetTeclaParaArmar();
             Debug.Log("Armado");
         }
         if (collision.CompareTag("Hitbox") && player.teclaParaFakear1)
         {
            rbBall.velocity = new Vector2(-15, 20);


           player.ResetTeclaParaFakear();
             Debug.Log("Fakear");
         }
         if (collision.CompareTag("Hitbox") && player.teclaParaPegar1)
         {
             rbBall.velocity = new Vector2(-20, 25);


           player.ResetTeclaParaPegar();
             Debug.Log("Golpe Basico");
         }
        if (collision.CompareTag("Hitbox") && player.teclaParaRematar1)
        {
            
            rbBall.velocity = new Vector2(-40, -5);
           

            player.ResetTeclaParaRematar();

            Debug.Log("Remate");
        }
        else
        {
            
        }

         // PLAYER 2
         if (collision.CompareTag("Hitbox2") && player2.teclaParaArmar2)
         {
             rbBall.velocity = new Vector2(3, 30);
             player2.ResetTeclaParaArmar2();

           Debug.Log("Armado");
       }
         if (collision.CompareTag("Hitbox2") && player2.teclaParaPegar2)
         {
             rbBall.velocity = new Vector2(20, 25);
             player2.ResetTeclaParaPegar2();
             Debug.Log("Golpe Basico");
         }
         if (collision.CompareTag("Hitbox2") && player2.teclaParaRematar2)
         {
            
            rbBall.velocity = new Vector2(40, -5);
             player2.ResetTeclaParaRematar2();
             
            Debug.Log("Remate");
        }
        else
        {
           
        }
         
         // PLAYER 2
         if (collision.CompareTag("Hitbox2") && player2.teclaParaFakear2)
         {
             rbBall.velocity = new Vector2(15, 20);

             player2.ResetTeclaParaFakear2();
             Debug.Log("Fake");
         }
     }
   



    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Hitbox"))
        {
            pelotaEnRadio1 = false;
        }
        if (collision.gameObject.CompareTag("Hitbox2"))
        {
            pelotaEnRadio2 = false;
        }
    }

    public void Reset()
    {
        rbBall.velocity = Vector2.zero; // Detener la pelota
        // Puedes agregar una l�gica para darle una nueva direcci�n o velocidad aqu�
    }
}