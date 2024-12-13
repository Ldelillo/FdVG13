using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public float attackRange = 0.5f;   // Rango del ataque melee
    public int attackDamage = 10;      // Daño del ataque

    public Transform attackPoint;      // Punto desde donde se originará el ataque
    // public Animator playerAnimator; // Controlador de animaciones del personaje

    public float attackCooldown = 1f;  // Tiempo entre ataques
    private float nextAttackTime = 0f; // Tiempo para el siguiente ataque

    public KeyCode attackKey = KeyCode.Mouse0;
    public AudioSource attackSound;

    // Opción de accesibilidad
    public bool autoTarget = false;
    private Animator animacion;
    void Start(){
        animacion = GetComponent<Animator>();
    }
    void Update()
    {
        animacion.SetBool("Ataque",false);
        if (autoTarget)
        {
            UpdateAttackPointToNearestEnemy();
        }
        else
        {
            UpdateAttackPointByMouse();
        }
        if(!gameObject.CompareTag("Arquero")){
        if (Time.time >= nextAttackTime && Input.GetKeyDown(attackKey))
        {   
            animacion.SetBool("Ataque",true);
            attackSound.Play();
            PerformAttack();
            nextAttackTime = Time.time + attackCooldown;
        }
        }
    }

    void UpdateAttackPointByMouse()
    {
        // Obtén la posición del ratón en el mundo
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // Calcula la dirección desde el jugador al ratón
        Vector3 direction = (mousePosition - transform.position).normalized;

        // Ajusta la posición de attackPoint en la dirección del ratón
        attackPoint.position = transform.position + direction * attackRange;
    }

    void UpdateAttackPointToNearestEnemy()
    {
        // Encuentra el enemigo más cercano dentro del rango
        Collider2D[] nearbyEnemies = UnityEngine.Physics2D.OverlapCircleAll(transform.position, attackRange * 2);
        Transform nearestEnemy = null;
        float shortestDistance = Mathf.Infinity;

        foreach (Collider2D enemy in nearbyEnemies)
        {
            if (enemy.CompareTag("Enemigo"))
            {
                float distanceToEnemy = Vector2.Distance(transform.position, enemy.transform.position);
                if (distanceToEnemy < shortestDistance)
                {
                    shortestDistance = distanceToEnemy;
                    nearestEnemy = enemy.transform;
                }
            }
        }

        // Si hay un enemigo cercano, apunta hacia él
        if (nearestEnemy != null)
        {
            Vector3 direction = (nearestEnemy.position - transform.position).normalized;
            attackPoint.position = transform.position + direction * attackRange;
        }
        else
        {
            // Si no hay enemigos cercanos, mantén el punto de ataque en la dirección del ratón
            UpdateAttackPointByMouse();
        }
    }

    void PerformAttack()
    {
        // Reproduce la animación de ataque
        // playerAnimator.SetTrigger("Attack");

        // Detectar enemigos en el rango de ataque
        Collider2D[] hitEnemies = UnityEngine.Physics2D.OverlapCircleAll(attackPoint.position, attackRange);

        // Infligir daño a los enemigos detectados
        foreach (Collider2D enemy in hitEnemies)
        {
            if (enemy.CompareTag("Enemigo"))
            {
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

        // Mostrar el rango de detección del enemigo en modo autoTarget
        if (autoTarget)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position, attackRange * 2);
        }
    }
}
