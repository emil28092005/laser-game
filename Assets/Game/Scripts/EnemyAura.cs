using UnityEngine;

public class EnemyDamageZone : MonoBehaviour
{
    [Header("Параметры урона")]
    public int minDamageAmount = 5;
    public int maxDamageAmount = 30;
    public float damageCooldown = 1.0f; // Пауза между ударами

    private float lastDamageTime;
    private int currentDamage;

    private void Start()
    {
        // При старте задаём случайный урон для этой DamageZone
        currentDamage = Random.Range(minDamageAmount, maxDamageAmount + 1);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (Time.time > lastDamageTime + damageCooldown)
            {
                var playerHealth = other.GetComponent<HealthManager>();
                if (playerHealth != null)
                {
                    playerHealth.Hit(currentDamage);
                    lastDamageTime = Time.time;
                    Debug.Log("Now HP is: " + playerHealth.Health);
                }
            }
        }
    }
}
