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
    private bool TeclaParaFakear2 = false;
    private bool TeclaParaSacar2 = false;
    public int ToquesMaximo2 = 0;
    private float tiempoDeEnfriamiento = 0.5f;
    private float tiempoUltimaActivacion = 0f;
    private GameManager gameManager;

    public bool teclaParaArmar2 => TeclaParaArmar2;
    public bool teclaParaPegar2 => TeclaParaPegar2;
    public bool teclaParaRematar2 => TeclaParaRematar2;
    public bool teclaParaFakear2 => TeclaParaFakear2;   
    void Start()
    {
        rb2 = GetComponent<Rigidbody2D>();
        ballHit = FindObjectOfType<BallHit>();
        gameManager = FindAnyObjectByType<GameManager>();
    }

    void Update()
    {
        float velocidadMovimiento = 5f; // Velocidad de movimiento

        // Controles para acciones
        if (Input.GetKey(KeyCode.S) && !IsJumping2 && ballHit.PelotaEnRadio2())
        {
            if (Time.time >= tiempoUltimaActivacion + tiempoDeEnfriamiento)
            {
                TeclaParaArmar2 = true;
                tiempoUltimaActivacion = Time.time;
                ToquesMaximo2++;
            }
        }
        if (Input.GetKey(KeyCode.S) && IsJumping2 && ballHit.PelotaEnRadio2())
        {
            if (Time.time >= tiempoUltimaActivacion + tiempoDeEnfriamiento)
            {
                TeclaParaFakear2 = true;
                tiempoUltimaActivacion = Time.time;
                ToquesMaximo2++;
            }
        }
        if (Input.GetKey(KeyCode.Space) && !IsJumping2 && ballHit.PelotaEnRadio2())
        {
            if (Time.time >= tiempoUltimaActivacion + tiempoDeEnfriamiento)
            {
                TeclaParaPegar2 = true;
                tiempoUltimaActivacion = Time.time;
                ToquesMaximo2++;
            }
        }
        if (Input.GetKey(KeyCode.Space) && IsJumping2 && ballHit.PelotaEnRadio2())
        {
            if (Time.time >= tiempoUltimaActivacion + tiempoDeEnfriamiento)
            {
                TeclaParaRematar2 = true;
                tiempoUltimaActivacion = Time.time;
                ToquesMaximo2++;
            }
        }
        if (gameManager != null && gameManager.SaquePermitido2 && Input.GetKeyDown(KeyCode.Space))
        {
            HacerSaque2();
        }

        // Movimiento a la izquierda
        if (!TouchingWall2)
        {
            if (Input.GetKey(KeyCode.A))
            {
                rb2.AddForce(new Vector2(-velocidadMovimiento, 0), ForceMode2D.Force);
            }
        }

        // Movimiento a la derecha
        if (!TouchingNet2)
        {
            if (Input.GetKey(KeyCode.D))
            {
                rb2.AddForce(new Vector2(velocidadMovimiento, 0), ForceMode2D.Force);
            }
        }

        // Saltar
        if (Input.GetKeyDown(KeyCode.W) && !IsJumping2)
        {
            rb2.gravityScale = 1; // Restablecer gravedad normal al saltar
            rb2.AddForce(Vector2.up * fuerzaDeSalto, ForceMode2D.Impulse);
            IsJumping2 = true;
        }

        // Aumentar la gravedad al caer
        if (IsJumping2 && rb2.velocity.y < 0)
        {
            rb2.gravityScale = 2; // Incrementar gravedad al caer
        }
        else if (!IsJumping2)
        {
            rb2.gravityScale = 1; // Restablecer la gravedad normal al estar en el suelo
        }
        
    }

    void FixedUpdate()
    {
        // Limitar la velocidad horizontal
        float limiteVelocidad = IsJumping2 ? 5f : 10f; // Limitar la velocidad en el aire y en el suelo
        rb2.velocity = new Vector2(Mathf.Clamp(rb2.velocity.x, -limiteVelocidad, limiteVelocidad), rb2.velocity.y);
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
        if (collision.gameObject.CompareTag("Ball"))
        {
            ToquesMaximo2++;
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
    public void ResetTeclaParaFakear2()
    {
        TeclaParaFakear2 = false;
    }
    void HacerSaque2()
    {
        Rigidbody2D ballRb = gameManager.ball.GetComponent<Rigidbody2D>();
        Rigidbody2D rb = rb2.GetComponent<Rigidbody2D>();



        // Cambiar el cuerpo rígido de la pelota a dinámico
        ballRb.bodyType = RigidbodyType2D.Dynamic;
        rb.bodyType = RigidbodyType2D.Dynamic;



        // Aplicar fuerza inicial a la pelota en la dirección especificada
        ballRb.velocity = new Vector2(13, 10);

        // Desactivar la capacidad de hacer otro saque hasta que se anote otro punto
        gameManager.SaquePermitido2 = false;

        // Restablecer la variable TeclaParaSacar1 para evitar múltiples saques con una sola pulsación
        TeclaParaSacar2 = false;
        Debug.Log("Saque");
    }

    // Método para activar la capacidad de saque en el jugador
    public void ActivarSaque2()
    {
        TeclaParaSacar2 = true;
    }

}

