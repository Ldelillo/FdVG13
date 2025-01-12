using System;
using UnityEngine;

public class MovimientoEnemigoVolador : MonoBehaviour
{
    public enum Estado { Patrullaje, Ataque }
    private Estado estadoActual = Estado.Patrullaje;

    [Header("Patrullaje")]
    public Transform[] nodos; // Nodos de patrullaje
    public float velocidadPatrullaje = 2f;
    private int nodoActual = 0;

    [Header("Detección y ataque")]
    public Transform jugador; // Referencia al jugador
    public float rangoDeteccion = 5f; // Rango para detectar al jugador
    public int daño = 10; // Daño que inflige el murciélago al jugador

    [Header("Cambio de Color")]
    public Color colorGolpe = Color.blue; // Color al golpear
    private SpriteRenderer spriteRenderer; // Referencia al SpriteRenderer
    private Color colorOriginal; // Para restaurar el color original

    //private Animator animacion;

    void Start()
    {
        // animacion = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        if (spriteRenderer != null)
        {
            colorOriginal = spriteRenderer.color; // Guardar el color original
        }
    }

    void Update()
    {
        switch (estadoActual)
        {
            case Estado.Patrullaje:
                Patrullar();
                DetectarJugador();
                break;

            case Estado.Ataque:
                PerseguirJugador();
                break;
        }
    }

    void Patrullar()
    {
        if (nodos.Length == 0) return;

        // Mover hacia el nodo actual
        Transform nodoDestino = nodos[nodoActual];
        transform.position = Vector2.MoveTowards(transform.position, nodoDestino.position, velocidadPatrullaje * Time.deltaTime);

        // Cambiar de nodo si se alcanza el actual
        if (Vector2.Distance(transform.position, nodoDestino.position) < 0.1f)
        {
            nodoActual = (nodoActual + 1) % nodos.Length; // Ciclo infinito por los nodos
        }

        // Girar según la dirección
        if (jugador.position.x > transform.position.x)
        {
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z); // Mirando a la derecha
        }
        else
        {
            transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z); // Mirando a la izquierda
        }


        // animacion.SetBool("Persigue", false);
    }

    void DetectarJugador()
    {
        if (jugador == null) return;

        float distanciaAlJugador = Vector2.Distance(transform.position, jugador.position);
        if (distanciaAlJugador <= rangoDeteccion)
        {
            estadoActual = Estado.Ataque;
        }
    }

    void PerseguirJugador()
    {
        if (jugador == null) return;

        // Mover hacia el jugador
        transform.position = Vector2.MoveTowards(transform.position, jugador.position, velocidadPatrullaje * Time.deltaTime);

        // Girar según la posición del jugador
        if (jugador.position.x > transform.position.x)
        {
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z); // Mirando a la derecha
        }
        else
        {
            transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z); // Mirando a la izquierda
        }
        float distanciaAlJugador = Vector2.Distance(transform.position, jugador.position);
        if (distanciaAlJugador >= rangoDeteccion)
        {
            estadoActual = Estado.Patrullaje;
        }
        // animacion.SetBool("Persigue", true);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Arquero") || collision.gameObject.CompareTag("Tanque"))
        {
            // Infligir daño al jugador
            collision.gameObject.GetComponent<Player>().recibirDaño(daño);

            // Cambiar color al golpear
            if (spriteRenderer != null)
            {
                spriteRenderer.color = colorGolpe;
                Invoke("RestaurarColor", 0.5f); // Restaurar el color después de 0.5 segundos
            }

        }
    }

    void RestaurarColor()
    {
        if (spriteRenderer != null)
        {
            spriteRenderer.color = colorOriginal;
        }
    }
}