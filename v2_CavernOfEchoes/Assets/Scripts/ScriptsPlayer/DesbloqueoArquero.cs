using UnityEngine;

public class DesbloqueoArquero : MonoBehaviour
{
    public bool habilidadDesbloqueada = false; // Controla si la habilidad ya est� desbloqueada
    private bool jugadorEnZona = false; // Para detectar si el jugador est� en la zona

    public GameObject iconoTeclaE; // �cono de la tecla E
    public GameObject panelGuia; // Panel de gu�a
    private bool panelAbierto = false; // Controla si el panel est� abierto

    private void Start()
    {
        // Aseg�rate de que el �cono de tecla E y el panel de gu�a est�n desactivados al inicio
        if (iconoTeclaE != null)
        {
            iconoTeclaE.SetActive(false);
        }

        if (panelGuia != null)
        {
            panelGuia.SetActive(false); // Desactiva el panel de gu�a al inicio
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

        // Verifica si el jugador presiona Esc para cerrar el panel
        if (panelAbierto && Input.GetKeyDown(KeyCode.Escape))
        {
            CerrarPanelGuia();
        }
    }

    private void DesbloquearHabilidad()
    {
        habilidadDesbloqueada = true;

        // Activa la habilidad de arquero en el jugador
        Player jugador = FindObjectOfType<Player>();
        if (jugador != null)
        {
            jugador.HabilitarArquero();
            Debug.Log("�Habilidad de cambio de personaje desbloqueada!");
        }

        // Activa el panel de gu�a y congela el juego
        if (panelGuia != null)
        {
            panelGuia.SetActive(true);
            panelAbierto = true;
            Time.timeScale = 0; // Congela el juego
        }
    }

    private void CerrarPanelGuia()
    {
        if (panelGuia != null)
        {
            panelGuia.SetActive(false);
            panelAbierto = false;
            Time.timeScale = 1; // Reanuda el juego
        }
    }
}
