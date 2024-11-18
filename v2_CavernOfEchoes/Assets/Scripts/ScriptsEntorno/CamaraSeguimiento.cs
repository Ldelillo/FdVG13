using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamaraSeguimiento : MonoBehaviour
{
    public Transform target;       // El objeto que la c�mara seguir� (el cubo o personaje)
    public float smoothSpeed = 0.125f; // Suavidad del movimiento de la c�mara
    public Vector2 minPosition;    // L�mites m�nimos para la c�mara
    public Vector2 maxPosition;    // L�mites m�ximos para la c�mara
    public float yOffset = 2f;     // Altura adicional de la c�mara

    void LateUpdate()
    {
        if (target != null)
        {
            // Actualiza la posici�n deseada en X y Y para que siga al jugador, y aplica un offset adicional en Y si se requiere
            Vector3 desiredPosition = new Vector3(target.position.x, target.position.y + yOffset, transform.position.z);

            // Suaviza el movimiento de la c�mara con Lerp para un seguimiento m�s suave
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

            // Asegurarse de que la c�mara no se salga de los l�mites del nivel
            smoothedPosition.x = Mathf.Clamp(smoothedPosition.x, minPosition.x, maxPosition.x);
            smoothedPosition.y = Mathf.Clamp(smoothedPosition.y, minPosition.y, maxPosition.y);

            // Actualiza la posici�n de la c�mara
            transform.position = smoothedPosition;
        }
    }
}
