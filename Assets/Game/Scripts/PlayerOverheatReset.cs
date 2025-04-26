using UnityEngine;

public class PlayerOverheatReset : MonoBehaviour
{
    public BarClickHighlighter overheatScript;
    public HealthManager healthManager;
    public int healAmount = 20;
    public float healCooldown = 5f; // Кулдаун между лечениями (в секундах)

    private float lastHealTime = -Mathf.Infinity;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Platform"))
        {
            if (Time.time >= lastHealTime + healCooldown)
            {
                Debug.Log("ON A PLATFORM — Healed!");

                if (overheatScript != null)
                {
                    overheatScript.ResetOverheat();
                }

                if (healthManager != null)
                {
                    healthManager.heal(healAmount);
                }

                lastHealTime = Time.time; // Обновляем момент последнего лечения
            }
            else
            {
                Debug.Log("Platform on cooldown...");
            }
        }
    }
}
