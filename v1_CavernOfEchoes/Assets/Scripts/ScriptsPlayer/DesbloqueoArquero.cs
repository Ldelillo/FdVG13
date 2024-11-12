using UnityEngine;

public class DesbloqueoArquero : MonoBehaviour
{
    public bool habilidadDesbloqueada = false; // Controla si la habilidad ya est� desbloqueada
    private bool jugadorEnZona = false; // Para detectar si el jugador est� en la zona

    public GameObject iconoTeclaE;

    private void Start()
    {
        // Aseg�rate de que el �cono est� desactivado al inicio
        if (iconoTeclaE != null)
        {
            iconoTeclaE.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Verifica si el jugador entra en el �rea de desbloqueo
        if (other.CompareTag("Player") && !habilidadDesbloqueada)
        {
            jugadorEnZona = true;
            if (iconoTeclaE != null)
            {
                iconoTeclaE.SetActive(true); // Activa el �cono al entrar en la zona
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        // Cuando el jugador sale de la zona
        if (other.CompareTag("Player"))
        {
            jugadorEnZona = false;
            if (iconoTeclaE != null)
            {
                iconoTeclaE.SetActive(false); // Desactiva el �cono al salir de la zona
            }
        }
    }

    private void Update()
    {
        // Solo intenta desbloquear la habilidad si el jugador est� en la zona y a�n no est� desbloqueada
        if (jugadorEnZona && !habilidadDesbloqueada && Input.GetKeyDown(KeyCode.E))
        {
            DesbloquearHabilidad();
            if (iconoTeclaE != null)
            {
                iconoTeclaE.SetActive(false); // Desactiva el �cono despu�s de desbloquear la habilidad
            }
        }
    }

    private void DesbloquearHabilidad()
    {
        habilidadDesbloqueada = true;
        // Llama al m�todo del jugador para activar la habilidad
        Player jugador = FindObjectOfType<Player>();
        if (jugador != null)
        {
            jugador.HabilitarArquero();
            Debug.Log("�Habilidad de cambio de personaje desbloqueada!");
        }
    }
}
