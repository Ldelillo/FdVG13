using UnityEngine;
using UnityEngine.UI;

public class HealthStamina : MonoBehaviour
{
    public Player player;

    public float maxHealth;
    //public float currentHealth;
    public float maxStamina = 100f;
    public float currentStamina;
    public Image HealthBar;
    public Image StaminaBar;

    // Cooldown del ataque b�sico
    public float attackCooldown = 3f;
    private float attackCooldownTimer;

    // Cooldown de la habilidad especial
    public float abilityCooldown = 10f;
    private float abilityCooldownTimer;
    public Image abilityCooldownImage;
    public Text abilityCooldownText;
    private bool isAbilityOnCooldown = false;

    public float healthBarLerpSpeed = 5f;
    public float staminaBarLerpSpeed = 10f;

    private int originalDefense; // Defensa original del jugador
    public int defenseUPHability = 500; // Aumento de defensa

    void Start()
    {
        maxHealth = player.vida;
        currentStamina = maxStamina;
        attackCooldownTimer = 0f;
        abilityCooldownTimer = 0f;
        UpdateHealthBar();
        UpdateStaminaBar();

        if (abilityCooldownImage != null) abilityCooldownImage.fillAmount = 0f;
        if (abilityCooldownText != null) abilityCooldownText.text = "";
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P)) TakeDamage(20f);

        if (Input.GetKeyDown(KeyCode.Mouse0) && currentStamina >= maxStamina)
        {
            UseStamina(100f);
            attackCooldownTimer = attackCooldown;
        }

        if (attackCooldownTimer <= 0 && currentStamina < maxStamina)
        {
            float staminaIncrement = maxStamina / attackCooldown * Time.deltaTime;
            currentStamina = Mathf.Min(currentStamina + staminaIncrement, maxStamina);
            UpdateStaminaBar();
        }

        if (attackCooldownTimer > 0) attackCooldownTimer -= Time.deltaTime;

        if (isAbilityOnCooldown)
        {
            abilityCooldownTimer -= Time.deltaTime;
            abilityCooldownImage.fillAmount = abilityCooldownTimer / abilityCooldown;
            if (abilityCooldownText != null) abilityCooldownText.text = Mathf.Ceil(abilityCooldownTimer).ToString();

            if (abilityCooldownTimer <= 0)
            {
                isAbilityOnCooldown = false;
                abilityCooldownImage.fillAmount = 0f;
                if (abilityCooldownText != null) abilityCooldownText.text = "";
            }
        }

        if (Input.GetKeyDown(KeyCode.Mouse1) && !isAbilityOnCooldown) ActivateAbility();

        UpdateHealthBar();
    }

    void ActivateAbility()
    {
        // Verifica si el jugador tiene la etiqueta "tanque"
        if (gameObject.CompareTag("Tanque")) // Cambia "gameObject" si tu jugador es un objeto diferente
        {
            UseSpecialAbilityTank(); // Llama a la habilidad especial
            isAbilityOnCooldown = true;
            abilityCooldownTimer = abilityCooldown;
            abilityCooldownImage.fillAmount = 1f;
            if (abilityCooldownText != null) abilityCooldownText.text = Mathf.Ceil(abilityCooldownTimer).ToString();
        }
        else
        {
            Debug.Log("Este personaje no puede usar la habilidad especial."); // Mensaje de error (opcional)
        }
    }


    void TakeDamage(float amount)
    {
        player.vida -= player.actual.defensa >= amount ? 1 : (amount - player.actual.defensa);
        player.vida = Mathf.Clamp(player.vida, 0, maxHealth);
        UpdateHealthBar();
    }

    public void UseStamina(float amount)
    {
        float targetStamina = Mathf.Max(0, currentStamina - amount);
        StartCoroutine(AnimateStaminaBar(targetStamina / maxStamina));
        currentStamina = targetStamina;
    }

    void UpdateHealthBar()
    {
        float healthPercentage = player.vida / maxHealth;
        StartCoroutine(AnimateHealthBar(healthPercentage));
        UpdateHealthBarColor(healthPercentage);
    }

    private System.Collections.IEnumerator AnimateHealthBar(float targetHealthPercentage)
    {
        while (Mathf.Abs(HealthBar.fillAmount - targetHealthPercentage) > 0.01f)
        {
            HealthBar.fillAmount = Mathf.Lerp(HealthBar.fillAmount, targetHealthPercentage, Time.deltaTime * healthBarLerpSpeed);
            yield return null;
        }
        HealthBar.fillAmount = targetHealthPercentage;
    }

    void UpdateStaminaBar()
    {
        StaminaBar.fillAmount = currentStamina / maxStamina;
    }

    private System.Collections.IEnumerator AnimateStaminaBar(float targetStaminaPercentage)
    {
        while (Mathf.Abs(StaminaBar.fillAmount - targetStaminaPercentage) > 0.01f)
        {
            StaminaBar.fillAmount = Mathf.Lerp(StaminaBar.fillAmount, targetStaminaPercentage, Time.deltaTime * staminaBarLerpSpeed);
            yield return null;
        }
        StaminaBar.fillAmount = targetStaminaPercentage;
    }

    void UpdateHealthBarColor(float healthPercentage)
    {
        HealthBar.color = Color.Lerp(Color.red, Color.green, healthPercentage);
    }

    void UseSpecialAbilityTank()
    {

        // L�gica de la habilidad especial (ejemplo: curar al jugador)
        // float healAmount = 20f; // Cantidad de vida a curar
        // player.vida += healAmount;
        // player.vida = Mathf.Clamp(player.vida, 0, maxHealth); // Aseg�rate de que no exceda la salud m�xima

        // UpdateHealthBar(); // Actualiza la barra de salud

        // Guarda la defensa original
        originalDefense = player.actual.defensa;

        // Aumenta la defensa
        player.actual.defensa += defenseUPHability;

        // Llama a la coroutine para revertir el efecto despu�s de 3 segundos
        StartCoroutine(ResetDefenseAfterDelay(3f));
    }

    private System.Collections.IEnumerator ResetDefenseAfterDelay(float delay)
    {
        player.habInUse = true;
        yield return new WaitForSeconds(delay); // Espera el tiempo especificado

        // Revertir la defensa al valor original
        player.actual.defensa -= defenseUPHability;
        player.habInUse = false;
    }
}
