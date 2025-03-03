using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public int scorePlayer1 = 0;
    public int scorePlayer2 = 0;
    public GameObject ball; // Arrastra el GameObject de la pelota aqu� en el Inspector
    public Transform startPositionPlayer1; // Arrastra el punto de inicio del jugador 1 aqu� en el Inspector
    public Transform startPositionPlayer2; // Arrastra el punto de inicio del jugador 2 aqu� en el Inspector
    public Transform startPositionBall1;
    public Transform startPositionBall2;
    private BallHit ballHit;
    private MovementPlayerScript player1; // Referencia a Player 1
    private MovementPlayer2Script player2; // Referencia a Player 2
    private SceneManager sceneManager;

    public TextMeshProUGUI scoreTextPlayer1; // Texto en UI para el puntaje del jugador 1
    public TextMeshProUGUI scoreTextPlayer2; // Texto en UI para el puntaje del jugador 2

    public bool SaquePermitido = false;
    public bool SaquePermitido2 = false;
    

    void Start()
    {
        player1 = GameObject.FindWithTag("Player1").GetComponent<MovementPlayerScript>(); // Aseg�rate de que Player1 tiene esta etiqueta
        player2 = GameObject.FindWithTag("Player2").GetComponent<MovementPlayer2Script>(); // Aseg�rate de que Player2 tiene esta etiqueta
        sceneManager = new SceneManager();  
       
        ballHit = ball.GetComponent<BallHit>();
        int PlayerQueSaca = Random.Range(1, 3);
        ResetBall(PlayerQueSaca);
        ResetPlayers();
        UpdateScoreText(); // Actualizar el texto al inicio

    }

    private void Update()
    {
        

        if (player1.ToquesMaximo > 3)
        {
            
            //ResetBall(playerNumber);
            
            player1.ToquesMaximo = 0;
            ResetBall(2);
            ResetPlayers();
            scorePlayer2++;
            UpdateScoreText();

            Debug.Log("Score Player 1: " + scorePlayer1 + " | Score Player 2: " + scorePlayer2);


        }
        if(ball.transform.position.x < 0)
        {
            player1.ToquesMaximo = 0;
            
        }
        if (player2.ToquesMaximo2 > 3)
        {

            //ResetBall(playerNumber);

            player2.ToquesMaximo2 = 0;
            ResetBall(1);
            ResetPlayers();
            scorePlayer1++;
            UpdateScoreText(); // Actualizar el texto al inicio

            Debug.Log("Score Player 1: " + scorePlayer1 + " | Score Player 2: " + scorePlayer2);


        }
        if (ball.transform.position.x > 0)
        {
            player2.ToquesMaximo2 = 0;

        }
       
        
        ResetGame();


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

        UpdateScoreText();
        // Reiniciar la pelota y los jugadores
        ResetBall(playerNumber);
        ResetPlayers(); // Reiniciar las posiciones de los jugadores
    }
    private void UpdateScoreText()
    {
        // Actualizar el texto en la UI
        scoreTextPlayer1.text = "Player1: " + scorePlayer1;
        scoreTextPlayer2.text = "Player2: " + scorePlayer2;
    }

    private void ResetBall(int playerNumber)
    {
        // Detener la pelota y reiniciar desde el lado del jugador que anot�
        Rigidbody2D ballRb = ball.GetComponent<Rigidbody2D>();
        Rigidbody2D p =player1.GetComponent<Rigidbody2D>();
        Rigidbody2D p2 =player2.GetComponent<Rigidbody2D>();    
        
        
        
        ballRb.velocity = Vector2.zero; // Detener el movimiento
        ballRb.angularVelocity = 0f; // Detener cualquier rotaci�n

        // Posici�n inicial de la pelota
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
        player1.transform.position = startPositionPlayer1.position; // Jugador 1 vuelve a su posici�n inicial
        player2.transform.position = startPositionPlayer2.position; // Jugador 2 vuelve a su posici�n inicial

        // Tambi�n puedes reiniciar las velocidades o las rotaciones de los jugadores si es necesario:
        player1.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        player2.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
    }
   
    private void ResetGame()
    {
        if(scorePlayer1 ==5)
        {
            Debug.Log("Player 1 WINS!");
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);

            scorePlayer1 = 0;
            scorePlayer2 = 0;
            player2.ToquesMaximo2 = 0;
            player1.ToquesMaximo = 0;

        }

        if(scorePlayer2 ==5)
        {
              Debug.Log("Player 2 WINS!");
              SceneManager.LoadScene(SceneManager.GetActiveScene().name);


            scorePlayer2 = 0;
            scorePlayer1 = 0;
            player2.ToquesMaximo2 = 0;
            player1.ToquesMaximo = 0;

        }
    }
   
}

