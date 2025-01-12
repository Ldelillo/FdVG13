using UnityEngine;

public class FallingPlatform : MonoBehaviour
{
    public float delayBeforeFall = 1f; // Tiempo antes de que comience a descender
    public float fallSpeed = 2f;      // Velocidad de descenso
    public float fallDistance = 10f; // Distancia m�xima que debe bajar

    private bool isFalling = false;
    private Vector3 startPosition;   // Posici�n inicial de la plataforma
    private Vector3 targetPosition; // Posici�n objetivo hacia donde debe caer

    void Start()
    {
        // Guardamos la posici�n inicial y calculamos la posici�n final
        startPosition = transform.position;
        targetPosition = startPosition + Vector3.down * fallDistance;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") || collision.CompareTag("Tanque") || collision.CompareTag("Arquero"))
        {
            // Iniciar la ca�da tras un retraso
            Invoke("StartFalling", delayBeforeFall);
        }
    }

    void StartFalling()
    {
        isFalling = true;
    }

    void Update()
    {
        if (isFalling)
        {
            // Interpolamos la posici�n hacia abajo
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, fallSpeed * Time.deltaTime);

            // Detenemos la ca�da al alcanzar la posici�n final
            if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
            {
                isFalling = false;
            }
        }
    }
}
