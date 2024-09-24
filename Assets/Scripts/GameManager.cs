using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro; // Importar para usar TextMeshPro

public class GameManager : MonoBehaviour
{
    public int scorePlayer1 = 0;
    public int scorePlayer2 = 0;
    public GameObject ball;
    public Transform startPositionPlayer1;
    public Transform startPositionPlayer2;
    public Transform startPositionBall1;
    public Transform startPositionBall2;
    private BallHit ballHit;
    private MovementPlayerScript player1;
    private MovementPlayer2Script player2;

    public TextMeshProUGUI scoreTextPlayer1; // Texto en UI para el puntaje del jugador 1
    public TextMeshProUGUI scoreTextPlayer2; // Texto en UI para el puntaje del jugador 2

    public bool SaquePermitido = false;
    public bool SaquePermitido2 = false;

    void Start()
    {
        player1 = GameObject.FindWithTag("Player1").GetComponent<MovementPlayerScript>();
        player2 = GameObject.FindWithTag("Player2").GetComponent<MovementPlayer2Script>();
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
            player1.ToquesMaximo = 0;
            ResetBall(2);
            ResetPlayers();
            scorePlayer2++;
            UpdateScoreText(); // Actualizar el texto cuando anote el jugador 2
        }

        if (ball.transform.position.x < 0)
        {
            player1.ToquesMaximo = 0;
        }

        if (player2.ToquesMaximo2 > 3)
        {
            player2.ToquesMaximo2 = 0;
            ResetBall(1);
            ResetPlayers();
            scorePlayer1++;
            UpdateScoreText(); // Actualizar el texto cuando anote el jugador 1
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

        UpdateScoreText(); // Actualizar el texto cuando un jugador anote
        ResetBall(playerNumber);
        ResetPlayers();
    }

    private void UpdateScoreText()
    {
        // Actualizar el texto en la UI
        scoreTextPlayer1.text = "Player 1: " + scorePlayer1;
        scoreTextPlayer2.text = "Player 2: " + scorePlayer2;
    }

    private void ResetBall(int playerNumber)
    {
        Rigidbody2D ballRb = ball.GetComponent<Rigidbody2D>();
        Rigidbody2D p = player1.GetComponent<Rigidbody2D>();
        Rigidbody2D p2 = player2.GetComponent<Rigidbody2D>();

        ballRb.velocity = Vector2.zero; // Detener la pelota
        ballRb.angularVelocity = 0f;

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
        // Reiniciar las posiciones de los jugadores
        player1.transform.position = startPositionPlayer1.position;
        player2.transform.position = startPositionPlayer2.position;
        player1.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        player2.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
    }

    private void ResetGame()
    {
        if (scorePlayer1 == 5)
        {
            Debug.Log("Player 1 WINS!");
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            scorePlayer1 = 0;
            scorePlayer2 = 0;
            player2.ToquesMaximo2 = 0;
            player1.ToquesMaximo = 0;
        }

        if (scorePlayer2 == 5)
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
