using UnityEngine;
using UnityEngine.UI;

public class Tank : MonoBehaviour
{
    public float maxHealth = 100f;
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
        currentHealth = maxHealth;
        currentStamina = maxStamina;
        cooldownTimer = 0f; // Inicializa el temporizador
        UpdateHealthBar();
        UpdateStaminaBar();
    }

    void Update()
    {
        // Simulación de recibir daño
        if (Input.GetKeyDown(KeyCode.D))
        {
            TakeDamage(10f);
        }

        // Simulación de usar stamina
        if (Input.GetKeyDown(KeyCode.Space) && currentStamina > 0)
        {
            UseStamina(20f); // Ejemplo: usa 20 de stamina al atacar
            cooldownTimer = attackCooldown; // Reinicia el temporizador de cooldown
        }

        // Actualiza el cooldown
        if (cooldownTimer > 0)
        {
            cooldownTimer -= Time.deltaTime; // Reduce el temporizador
            if (cooldownTimer <= 0) // Si el cooldown ha terminado
            {
                currentStamina = maxStamina; // Restaura la stamina al máximo
                UpdateStaminaBar(); // Actualiza la barra de stamina
            }
        }

        // Actualiza la barra de vida
        UpdateHealthBar();
    }

    void TakeDamage(float amount)
    {
        currentHealth -= amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth); // Asegúrate de que la salud no sea menor que 0
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
        float healthPercentage = currentHealth / maxHealth;

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
