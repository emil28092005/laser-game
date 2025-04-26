using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class HealthUI : MonoBehaviour
{
    public HealthManager playerHealth;
    public Image healthFill;
    public TextMeshProUGUI healthText;

    private Coroutine textAnimationCoroutine;

    private void Start()
    {
        if (playerHealth != null)
            playerHealth.ChangeEvent.AddListener(OnHealthChanged);

        UpdateHealthBarImmediate();
    }

    private void OnHealthChanged(int amount)
    {
        UpdateHealthBarAnimated();
    }

    private void UpdateHealthBarImmediate()
    {
        if (healthFill != null && playerHealth != null)
        {
            healthFill.fillAmount = (float)playerHealth.Health / playerHealth.MaxHealth;
        }

        if (healthText != null && playerHealth != null)
        {
            healthText.text = $"{playerHealth.Health} / {playerHealth.MaxHealth}";
        }
    }

    private void UpdateHealthBarAnimated()
    {
        if (healthFill != null && playerHealth != null)
        {
            healthFill.fillAmount = (float)playerHealth.Health / playerHealth.MaxHealth;
        }

        if (healthText != null && playerHealth != null)
        {
            if (textAnimationCoroutine != null)
                StopCoroutine(textAnimationCoroutine);

            textAnimationCoroutine = StartCoroutine(AnimateHealthText());
        }
    }

    private IEnumerator AnimateHealthText()
    {
        int displayedHealth = int.Parse(healthText.text.Split('/')[0].Trim()); // Текущее число на экране
        int targetHealth = playerHealth.Health; // Реальное здоровье

        float duration = 0.3f; // длительность анимации
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / duration);
            int currentHealth = Mathf.RoundToInt(Mathf.Lerp(displayedHealth, targetHealth, t));
            healthText.text = $"{currentHealth} / {playerHealth.MaxHealth}";
            yield return null;
        }

        // в конце выставляем точно правильное значение
        healthText.text = $"{playerHealth.Health} / {playerHealth.MaxHealth}";
    }
}
