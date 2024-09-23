using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementPlayerScript : MonoBehaviour
{
    private float fuerzaDeSalto = 12f;
    private BallHit ballHit;
    private Rigidbody2D rb1;
    private bool IsJumping1 = false;
    private bool Saque1 = false;
    private bool TouchingWall1 = false;
    private bool TouchingNet1 = false;
    private bool TeclaParaArmar1 = false;
    private bool TeclaParaPegar1 = false;
    private bool TeclaParaRematar1 = false;
    private bool TeclaParaFakear1 = false;
    private bool TeclaParaSacar1 = false;
    private float tiempoDeEnfriamiento = 0.5f;
    private float tiempoUltimaActivacion = 0f;
    private GameManager gameManager;
    
    

    public bool teclaParaArmar1 => TeclaParaArmar1;
    public bool teclaParaPegar1 => TeclaParaPegar1;
    public bool teclaParaRematar1 => TeclaParaRematar1;
    public bool teclaParaFakear1 => TeclaParaFakear1;
    
    

    void Start()
    {
        rb1 = GetComponent<Rigidbody2D>();
        ballHit = FindObjectOfType<BallHit>();
        gameManager = FindAnyObjectByType<GameManager>();   
    }

    void Update()
    {
        float velocidadMovimiento = 5f; // Velocidad de movimiento en el suelo o en el aire

        // Controles para acciones de armar, pegar y rematar
        if (Input.GetKey(KeyCode.DownArrow) && !IsJumping1 && ballHit.PelotaEnRadio())
        {
            if (Time.time >= tiempoUltimaActivacion + tiempoDeEnfriamiento)
            {
                TeclaParaArmar1 = true;
                tiempoUltimaActivacion = Time.time;
            }
        }
        if (Input.GetKey(KeyCode.DownArrow) && IsJumping1 && ballHit.PelotaEnRadio())
        {
            if (Time.time >= tiempoUltimaActivacion + tiempoDeEnfriamiento)
            {
                TeclaParaFakear1 = true;
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

        if (Input.GetKey(KeyCode.RightShift) && IsJumping1 && ballHit.PelotaEnRadio())
        {
            if (Time.time >= tiempoUltimaActivacion + tiempoDeEnfriamiento)
            {
                TeclaParaRematar1 = true;
                tiempoUltimaActivacion = Time.time;
            }
        }
        if (gameManager != null && gameManager.SaquePermitido && Input.GetKeyDown(KeyCode.RightShift))
        {
            HacerSaque();
        }

        // Movimiento hacia la derecha
        if (!TouchingWall1)
        {
            if (Input.GetKey(KeyCode.RightArrow))
            {
                rb1.AddForce(new Vector2(velocidadMovimiento, 0), ForceMode2D.Force);
            }
        }

        // Movimiento hacia la izquierda
        if (!TouchingNet1)
        {
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                rb1.AddForce(new Vector2(-velocidadMovimiento, 0), ForceMode2D.Force);
            }
        }

        // Saltar
        if (Input.GetKeyDown(KeyCode.UpArrow) && !IsJumping1)
        {
            rb1.gravityScale = 1; // Gravedad normal al saltar
            rb1.AddForce(Vector2.up * fuerzaDeSalto, ForceMode2D.Impulse);
            IsJumping1 = true;
        }

        // Ajustar gravedad cuando está cayendo
        if (IsJumping1 && rb1.velocity.y < 0)
        {
            rb1.gravityScale = 2; // Incrementa la gravedad al caer
        }
        else if (!IsJumping1)
        {
            rb1.gravityScale = 1; // Restablece la gravedad al estar en el suelo
        }

        
    }

    void FixedUpdate()
    {
        // Limitar la velocidad horizontal a un rango más alto en suelo, más bajo en el aire
        float limiteVelocidad = IsJumping1 ? 5f : 10f; // Limitar la velocidad horizontal cuando esté en el aire
        rb1.velocity = new Vector2(Mathf.Clamp(rb1.velocity.x, -limiteVelocidad, limiteVelocidad), rb1.velocity.y);
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
    public void ResetTeclaParaFakear()
    {
        TeclaParaFakear1 = false;
    }
    void HacerSaque()
    {
        Rigidbody2D ballRb = gameManager.ball.GetComponent<Rigidbody2D>();
        Rigidbody2D rb = rb1.GetComponent<Rigidbody2D>();
        
           

        // Cambiar el cuerpo rígido de la pelota a dinámico
        ballRb.bodyType = RigidbodyType2D.Dynamic;
        rb.bodyType = RigidbodyType2D.Dynamic;



        // Aplicar fuerza inicial a la pelota en la dirección especificada
        ballRb.velocity = new Vector2(-10, 10);

        // Desactivar la capacidad de hacer otro saque hasta que se anote otro punto
        gameManager.SaquePermitido = false;

        // Restablecer la variable TeclaParaSacar1 para evitar múltiples saques con una sola pulsación
        TeclaParaSacar1 = false;
    }

    // Método para activar la capacidad de saque en el jugador
    public void ActivarSaque()
    {
        TeclaParaSacar1 = true;
    }

}
