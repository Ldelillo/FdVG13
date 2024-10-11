using UnityEngine;

public class MovimientoEnemigo : MonoBehaviour
{
    // Posiciones en el eje X entre las que patrullar� el enemigo
    public float xInicial = 30f;  // Primera posici�n X
    public float xFinal = 60f;    // Segunda posici�n X
    public float velocidad = 2f;  // Velocidad de movimiento

    // Distancia m�nima para detenerse frente al jugador
    public float distanciaDeteccion = 5f; // Distancia a la que detecta al jugador
    public float distanciaParada = 1f;    // Distancia m�nima para detenerse frente al jugador

    // Variables internas
    private Vector3 destinoActual;        // Almacena el destino actual del enemigo
    private bool jugadorCerca = false;    // Verifica si el jugador est� lo suficientemente cerca
    private bool seDetuvoFrenteJugador = false; // Verifica si el enemigo se ha detenido frente al jugador

    // Referencia al jugador y a su posici�n inicial
    public Transform jugador;
    private Vector3 posicionInicialJugador;

    void Start()
    {
        // Establecer el primer destino en la posici�n inicial
        destinoActual = new Vector3(xInicial, transform.position.y, transform.position.z);

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
        }

        // Si el jugador no est� cerca, patrullar entre dos puntos
        if (!jugadorCerca)
        {
            Patrullar();
        }
    }

    // M�todo para patrullar entre dos puntos
    void Patrullar()
    {
        // Mover el enemigo hacia el destino actual
        transform.position = Vector2.MoveTowards(transform.position, destinoActual, velocidad * Time.deltaTime);

        // Verificar si el enemigo ha llegado al destino
        if (Vector2.Distance(transform.position, destinoActual) < 0.1f)
        {
            // Cambiar el destino al otro punto (xInicial o xFinal)
            if (destinoActual.x == xInicial)
            {
                destinoActual = new Vector3(xFinal, transform.position.y, transform.position.z);
            }
            else
            {
                destinoActual = new Vector3(xInicial, transform.position.y, transform.position.z);
            }
        }

        // Girar el enemigo en funci�n de la direcci�n en la que se mueve
        if (destinoActual.x > transform.position.x)
        {
            transform.localScale = new Vector3(1, 1, 1);  // Mirando a la derecha
        }
        else
        {
            transform.localScale = new Vector3(-1, 1, 1);  // Mirando a la izquierda
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
        }
    }
}
