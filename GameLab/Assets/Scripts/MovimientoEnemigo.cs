using System;
using System.Data;
using Unity.Mathematics;
using UnityEngine;

public class MovimientoEnemigo : MonoBehaviour
{
    // Posiciones en el eje X entre las que patrullar� el enemigo
    public float xIzquierdaMax = 10f;  // Primera posici�n X
    public float xDerechaMax = 10f;    // Segunda posici�n X
    public float velocidad = 2f;  // Velocidad de movimiento

    // Distancia m�nima para detenerse frente al jugador
    public float distanciaDeteccion = 3f; // Distancia a la que detecta al jugador
    public float distanciaParada = 1f;    // Distancia m�nima para detenerse frente al jugador

    // Variables internas
    private Vector3 destinoActual;        // Almacena el destino actual del enemigo
    private bool jugadorCerca = false;    // Verifica si el jugador est� lo suficientemente cerca
    private bool seDetuvoFrenteJugador = false; // Verifica si el enemigo se ha detenido frente al jugador

    // Referencia al jugador y a su posici�n inicial
    public Transform jugador;
    private Vector3 posicionInicialJugador;
    private Vector2 posicionInicial;

    private Animator movimiento;


    void Start()
    {
        //Recogemos y guardamos la posicion inicial del jugador
        posicionInicial = transform.position;
        // Establecer el primer destino en la posici�n inicial
        destinoActual = new Vector3(posicionInicial.x + xIzquierdaMax, transform.position.y, transform.position.z);
        movimiento = GetComponent<Animator>();

        // Guardar la posici�n inicial del jugador
        if (jugador != null)
        {
            posicionInicialJugador = jugador.position;
        }
    }

    void Update()
    {
        // Comprobar la distancia entre el enemigo y el jugador
        if (jugador != null)
        {
            float distanciaAlJugador = Vector2.Distance(transform.position, jugador.position);

            if (distanciaAlJugador <= distanciaDeteccion)
            {
                // Si el jugador est� lo suficientemente cerca, detener la patrulla
                movimiento.SetBool("Persigue",true);
                jugadorCerca = true;

                // Mover hacia el jugador hasta que est� a la distancia de parada
                if (distanciaAlJugador > distanciaParada)
                {
                    // Mover el enemigo hacia el jugador
                    transform.position = Vector2.MoveTowards(transform.position, jugador.position, velocidad * Time.deltaTime);

                    // Girar el enemigo para mirar al jugador
                    if (jugador.position.x > transform.position.x)
                    {
                        transform.localScale = new Vector3(1, 1, 1);  // Mirando a la derecha
                    }
                    else
                    {
                        transform.localScale = new Vector3(-1, 1, 1);  // Mirando a la izquierda
                    }
                }
                else
                {
                    // El enemigo se detiene cuando est� suficientemente cerca del jugador
                    seDetuvoFrenteJugador = true;
                }
                return; // Salir de Update para no seguir patrullando
            }
            else{
                jugadorCerca = false;
            }
        }

        // Si el jugador no est� cerca, patrullar entre dos puntos
        if (!jugadorCerca)
        {
            movimiento.SetBool("Persigue",false);
            Patrullar();
        }
    }

    // M�todo para patrullar entre dos puntos
    void Patrullar()
    {
        // Mover el enemigo hacia el destino actual
        transform.position = Vector2.MoveTowards(transform.position, destinoActual, velocidad * Time.deltaTime);
        // Girar el enemigo en funci�n de la direcci�n en la que se mueve
        if (destinoActual.x > transform.position.x)
        {
            transform.localScale = new Vector3(1, 1, 1);  // Mirando a la derecha
        }
        else
        {
            transform.localScale = new Vector3(-1, 1, 1);  // Mirando a la izquierda
        }

        // Verificar si el enemigo ha llegado al destino
        if (Vector2.Distance(transform.position, destinoActual) < 0.1f || transform.position.x - (posicionInicial.x - xIzquierdaMax) <=0f ||
         transform.position.x - (posicionInicial.x + xDerechaMax)  >=0f) //Estas dos condiciones extras son necesarias para evitar bugs despues de pasar por patrullaje
        {
            // Cambiar el destino al otro punto (xIzquierdaMax o xDerechaMax)
            if (destinoActual.x == posicionInicial.x - xIzquierdaMax)
            {
                destinoActual = new Vector3(posicionInicial.x + xDerechaMax, transform.position.y, transform.position.z);
            }
            else
            {
                destinoActual = new Vector3(posicionInicial.x - xIzquierdaMax, transform.position.y, transform.position.z);
            }
        }


    }

    // Detectar colisiones con el jugador
    void OnCollisionEnter2D(Collision2D collision)
    {
        // Verificar si el objeto con el que colisiona tiene la etiqueta "Player"
        if (collision.gameObject.CompareTag("Player"))
        {
            // Mover al jugador a su posici�n inicial
            collision.gameObject.transform.position = posicionInicialJugador;
            movimiento.SetBool("Persigue",false);
            jugadorCerca = false;
            Patrullar(); //Si la serpiente impacta con el jugador tenemos que volver al estado inicial
        }
    }
}
