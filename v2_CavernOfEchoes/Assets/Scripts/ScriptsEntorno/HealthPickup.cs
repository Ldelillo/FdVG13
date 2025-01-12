using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    public float healthAmount = 20; // Cantidad de salud que restaura

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Verifica si el objeto que lo toca es el jugador
        if (collision.CompareTag("Player") || collision.CompareTag("Tanque") || collision.CompareTag("Arquero"))
        {
            // Accede al script Player y actualiza la salud
            Player player = collision.GetComponent<Player>();
            if (player != null)
            {
                // Aumentar la vida del jugador pero sin exceder un valor máximo
                player.vida = Mathf.Min(player.vida + healthAmount, 100); // 100 como valor máximo
                Debug.Log($"Salud restaurada. Vida actual: {player.vida}");
            }

            // Destruye el objeto de salud después de recogerlo
            Destroy(gameObject);
        }
    }
}
