using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public float attackRange = 0.5f;   // Rango del ataque melee
    public int attackDamage = 10;      // Daño del ataque

    public Transform attackPoint;      // El punto desde donde se originará el ataque
    //public Animator animator;          // Controlador de animaciones del personaje

    public float attackCooldown = 0.5f;   // Tiempo entre ataques
    private float nextAttackTime = 0f;    // Tiempo para el siguiente ataque

    public KeyCode attackKey = KeyCode.Z;
    void Update()
    {
        if (Time.time >= nextAttackTime && Input.GetKeyDown(attackKey))
        {
            PerformAttack();
            nextAttackTime = Time.time + attackCooldown;
        }
    }

    void PerformAttack()
    {
        // Reproduce la animación de ataque
        //animator.SetTrigger("Attack");

        // Detectar enemigos en el rango de ataque
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange);

        // Infligir daño a los enemigos detectados
        foreach (Collider2D enemy in hitEnemies)
        {   
            if(enemy.CompareTag("Enemigo")){
            enemy.GetComponent<Damageable>().TakeDamage(attackDamage);
            }
        }
    }

    // Visualizar el rango de ataque en el editor
    void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
