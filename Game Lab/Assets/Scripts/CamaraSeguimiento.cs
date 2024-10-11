using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamaraSeguimiento : MonoBehaviour
{
    public Transform target;       // El objeto que la cámara seguirá (el cubo o personaje)
    public float smoothSpeed = 0.125f; // Suavidad del movimiento de la cámara
    public Vector2 minPosition;    // Límites mínimos para la cámara
    public Vector2 maxPosition;    // Límites máximos para la cámara
    public float yOffset = 2f;     // Altura fija de la cámara (como en Super Mario)

    void LateUpdate()
    {
        if (target != null)
        {
            // Mantén la cámara con la posición del jugador en X y un offset fijo en Y
            Vector3 desiredPosition = new Vector3(target.position.x, yOffset, transform.position.z);

            // Suaviza el movimiento de la cámara con Lerp para un seguimiento más suave
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

            // Asegurarse de que la cámara no se salga de los límites del nivel
            smoothedPosition.x = Mathf.Clamp(smoothedPosition.x, minPosition.x, maxPosition.x);
            smoothedPosition.y = Mathf.Clamp(smoothedPosition.y, minPosition.y, maxPosition.y);

            // Actualiza la posición de la cámara
            transform.position = smoothedPosition;
        }
    }
}
