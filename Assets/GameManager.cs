using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int scorePlayer1 = 0;
    public int scorePlayer2 = 0;
    public GameObject ball; // Arrastra el GameObject de la pelota aquí en el Inspector
    public Transform startPositionPlayer1; // Arrastra el punto de inicio del jugador 1 aquí en el Inspector
    public Transform startPositionPlayer2; // Arrastra el punto de inicio del jugador 2 aquí en el Inspector
    public Transform startPositionBall1;
    public Transform startPositionBall2;
    private BallHit ballHit;
    private MovementPlayerScript player1; // Referencia a Player 1
    private MovementPlayer2Script player2; // Referencia a Player 2
    

    public bool SaquePermitido = false;
    public bool SaquePermitido2 = false;
    

    void Start()
    {
        player1 = GameObject.FindWithTag("Player1").GetComponent<MovementPlayerScript>(); // Asegúrate de que Player1 tiene esta etiqueta
        player2 = GameObject.FindWithTag("Player2").GetComponent<MovementPlayer2Script>(); // Asegúrate de que Player2 tiene esta etiqueta
          
       
        ballHit = ball.GetComponent<BallHit>();
        int PlayerQueSaca = Random.Range(1, 3);
        ResetBall(PlayerQueSaca);
        ResetPlayers();

    }

    public void PlayerScored(int playerNumber)
    {
        if (playerNumber == 1)
        {
            scorePlayer1++;
        }
        else if (playerNumber == 2)
        {
            scorePlayer2++;
        }

        Debug.Log("Score Player 1: " + scorePlayer1 + " | Score Player 2: " + scorePlayer2);

        // Reiniciar la pelota y los jugadores
        ResetBall(playerNumber);
        ResetPlayers(); // Reiniciar las posiciones de los jugadores
    }

    private void ResetBall(int playerNumber)
    {
        // Detener la pelota y reiniciar desde el lado del jugador que anotó
        Rigidbody2D ballRb = ball.GetComponent<Rigidbody2D>();
        Rigidbody2D p =player1.GetComponent<Rigidbody2D>();
        Rigidbody2D p2 =player2.GetComponent<Rigidbody2D>();    
        
        
        
        ballRb.velocity = Vector2.zero; // Detener el movimiento
        ballRb.angularVelocity = 0f; // Detener cualquier rotación

        // Posición inicial de la pelota
        if (playerNumber == 1)
        {
            ball.transform.position = startPositionBall1.position;
            ballRb.bodyType = RigidbodyType2D.Static;
            p.bodyType = RigidbodyType2D.Static;
            



            SaquePermitido = true; 

            
            player1.ActivarSaque();
        }
        else
        {
            SaquePermitido = false;
        }
        if (playerNumber == 2)
        {
            ball.transform.position = startPositionBall2.position;
            
            ballRb.bodyType = RigidbodyType2D.Static;
            p2.bodyType = RigidbodyType2D.Static;
            




            SaquePermitido2 = true; 
        }
        else
        {
            SaquePermitido2 = false;
        }

        
        
    }

    private void ResetPlayers()
    {
        // Reiniciar las posiciones de los jugadores a sus posiciones iniciales
        player1.transform.position = startPositionPlayer1.position; // Jugador 1 vuelve a su posición inicial
        player2.transform.position = startPositionPlayer2.position; // Jugador 2 vuelve a su posición inicial

        // También puedes reiniciar las velocidades o las rotaciones de los jugadores si es necesario:
        player1.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        player2.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
    }
   
   
}

