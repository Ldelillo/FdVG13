using UnityEngine;

public class Damageable : MonoBehaviour
{
    public int health = 100;
    public AudioSource golpeado;
    public Player jugador;

    public void TakeDamage(int damage)
    {
        golpeado.Play();
        health -= damage;

        // Si la salud llega a 0 o menos, destruir el objeto
        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        // Reproducir animación de muerte, sonido o algún efecto visual
        Destroy(gameObject); // Destruir el objeto del enemigo
    }
}
