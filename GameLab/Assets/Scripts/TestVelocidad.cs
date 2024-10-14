using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestVelocidad : MonoBehaviour
{
    private MovimientoJugador playerMovementScript; // Referencia al script MovimientoJugador
    public float speedIncrement = 1f; // Cantidad para aumentar o disminuir la velocidad

    void Start()
    {
        // Buscar automáticamente el script MovimientoJugador en el objeto del jugador
        playerMovementScript = FindObjectOfType<MovimientoJugador>();
    }

    void Update()
    {
        // Si el script de MovimientoJugador no está asignado, salir
        if (playerMovementScript == null) return;

        // Ajustar la velocidad con la tecla '+' (RightBracket)
        if (Input.GetKeyDown(KeyCode.RightBracket))
        {
            playerMovementScript.speed += speedIncrement;
            Debug.Log("Velocidad aumentada: " + playerMovementScript.speed);
        }

        // Ajustar la velocidad con la tecla '-' (Slash)
        if (Input.GetKeyDown(KeyCode.Slash))
        {
            playerMovementScript.speed -= speedIncrement;
            // Prevenir que la velocidad sea negativa
            if (playerMovementScript.speed < 0) playerMovementScript.speed = 0;
            Debug.Log("Velocidad disminuida: " + playerMovementScript.speed);
        }
    }
}
