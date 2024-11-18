using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimientoJugador : MonoBehaviour
{
    public float speed = 5f;      // Velocidad de movimiento del cubo
    public float jumpForce;  // Fuerza del salto
    private int jumpCount = 0;    // Contador de saltos realizados
    public int maxJumps;      // N�mero m�ximo de saltos permitidos (2 para doble salto)
    public bool isGrounded;      // Para verificar si est� en el suelo
    private Rigidbody2D rb;       // Referencia al Rigidbody2D del cubo
    public float horizontal;
    public float mirando;

    void Start()
    {
        // Obtener la referencia al Rigidbody2D
        rb = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        // Movimiento horizontal (A/D o flechas)
        horizontal = Input.GetAxis("Horizontal");

        // Crear el vector de movimiento (solo en el eje X)
        Vector2 movement = new Vector2(horizontal * speed, rb.velocity.y);
        mirando = movement.x;

        // Aplicar movimiento al Rigidbody2D
        rb.velocity = movement;

        // Detectar si se presiona la barra espaciadora
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // Si el jugador est� en el suelo o tiene saltos restantes, puede saltar
            if (isGrounded || jumpCount < maxJumps)
            {
                Jump();
            }
        }
    }

    // M�todo para aplicar la fuerza del salto
    void Jump()
    {
        // Resetear la velocidad en el eje Y para un salto consistente
        rb.velocity = new Vector2(rb.velocity.x, 1.5f);

        // Aplicar la fuerza de salto
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);

        // Aumentar el contador de saltos
        jumpCount++;

        // Indicar que ya no est� en el suelo despu�s del primer salto
        isGrounded = false;
    }

    // Detectar cu�ndo est� tocando el suelo
    void OnCollisionEnter2D(Collision2D collision)
    {
        // Si el cubo colisiona con el suelo, reiniciar el contador de saltos
        if (collision.gameObject.CompareTag("Suelo"))
        {
            isGrounded = true;  // Est� en el suelo
            jumpCount = 0;      // Resetear el contador de saltos
        }
    }
}
