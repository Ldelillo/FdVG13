using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HealthStamina : MonoBehaviour
{
    public Player player;

    public float maxHealth;
    public float currentHealth;
    public float maxStamina = 100f;
    public float currentStamina;
    public Image HealthBar;
    public Image StaminaBar;
    public float attackCooldown = 3f; // Tiempo de cooldown del ataque
    private float cooldownTimer;
    public float healthBarLerpSpeed = 5f; // Velocidad de interpolación para la barra de salud
    public float staminaBarLerpSpeed = 5f; // Velocidad de interpolación para la barra de stamina

    void Start()
    {
        maxHealth = player.vida;
        // currentHealth = maxHealth;
        currentStamina = maxStamina;
        cooldownTimer = 0f; // Inicializa el temporizador
        UpdateHealthBar();
        UpdateStaminaBar();
    }

    void Update()
    {
        // Simulación de recibir daño
        if (Input.GetKeyDown(KeyCode.P))
        {
            TakeDamage(20f);
        }

        // Simulación de usar stamina solo si está completamente llena
        if (Input.GetKeyDown(KeyCode.Mouse0) && currentStamina >= maxStamina)
        {
            UseStamina(20f); // Usa 20 de stamina al atacar
            cooldownTimer = attackCooldown; // Reinicia el temporizador de cooldown
        }

        // Incrementa la stamina gradualmente si el cooldown ha terminado
        if (cooldownTimer <= 0 && currentStamina < maxStamina)
        {
            // Calcula cuánto debe incrementarse la stamina por segundo para llenarse en attackCooldown
            float staminaIncrement = maxStamina / attackCooldown * Time.deltaTime;
            currentStamina = Mathf.Min(currentStamina + staminaIncrement, maxStamina); // Incrementa y limita a maxStamina
            UpdateStaminaBar(); // Actualiza la barra de stamina mientras sube
        }

        // Actualiza el cooldown si todavía está en progreso
        if (cooldownTimer > 0)
        {
            cooldownTimer -= Time.deltaTime; // Reduce el temporizador
        }

        // Actualiza la barra de vida en cada frame
        UpdateHealthBar();
    }


    void TakeDamage(float amount)
    {
        player.vida -= player.defensa >= amount ? 1 : (amount - player.defensa);

        //player.vida -= amount - player.defensa;
        player.vida = Mathf.Clamp(player.vida, 0, maxHealth); // Asegúrate de que la salud no sea menor que 0
        UpdateHealthBar();
    }

    void UseStamina(float amount)
    {
        currentStamina = 0; // La stamina se reduce a 0 inmediatamente al atacar
        UpdateStaminaBar();
        StartCoroutine(AnimateStaminaBar(0f)); // Inicia la animación de la barra de stamina a 0
    }

    void UpdateHealthBar()
    {
        // Calcula el porcentaje de vida restante
        float healthPercentage = player.vida / maxHealth;

        // Inicia la corrutina para hacer la animación suave
        StartCoroutine(AnimateHealthBar(healthPercentage));

        // Cambia el color de la barra según la salud restante
        UpdateHealthBarColor(healthPercentage);
    }

    private System.Collections.IEnumerator AnimateHealthBar(float targetHealthPercentage)
    {
        // Mientras el valor de la barra no ha alcanzado el objetivo, sigue animando
        while (Mathf.Abs(HealthBar.fillAmount - targetHealthPercentage) > 0.01f)
        {
            // Ajusta el valor de la barra suavemente usando Lerp
            HealthBar.fillAmount = Mathf.Lerp(HealthBar.fillAmount, targetHealthPercentage, Time.deltaTime * healthBarLerpSpeed);
            yield return null; // Espera al siguiente frame
        }

        // Asegúrate de que la barra de vida alcance exactamente el valor final
        HealthBar.fillAmount = targetHealthPercentage;
    }

    void UpdateStaminaBar()
    {
        // Calcula el porcentaje de stamina restante
        float staminaPercentage = currentStamina / maxStamina;

        // Inicia la corrutina para hacer la animación suave
        StartCoroutine(AnimateStaminaBar(staminaPercentage));
    }

    private System.Collections.IEnumerator AnimateStaminaBar(float targetStaminaPercentage)
    {
        // Mientras el valor de la barra no ha alcanzado el objetivo, sigue animando
        while (Mathf.Abs(StaminaBar.fillAmount - targetStaminaPercentage) > 0.01f)
        {
            // Ajusta el valor de la barra suavemente usando Lerp
            StaminaBar.fillAmount = Mathf.Lerp(StaminaBar.fillAmount, targetStaminaPercentage, Time.deltaTime * staminaBarLerpSpeed);
            yield return null; // Espera al siguiente frame
        }

        // Asegúrate de que la barra de stamina alcance exactamente el valor final
        StaminaBar.fillAmount = targetStaminaPercentage;
    }

    void UpdateHealthBarColor(float healthPercentage)
    {
        // Cambia el color de la barra según la salud restante
        HealthBar.color = Color.Lerp(Color.red, Color.green, healthPercentage);
    }
}
