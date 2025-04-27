using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(HealthManager))]
public class EnemyAI : MonoBehaviour
{
    private NavMeshAgent agent;
    private HealthManager health;
    private Renderer rend;
    private Transform playerTransform;
    [FormerlySerializedAs("correspondingLevelManager")] public LevelManager levelManager;
    void Awake()
    {
        agent   = GetComponent<NavMeshAgent>();
        health  = GetComponent<HealthManager>();
        rend    = GetComponent<Renderer>() ?? GetComponentInChildren<Renderer>();
        health.DeathEvent.AddListener(OnDeath);

        var rb = GetComponent<Rigidbody>();
        var playerCol = GameObject.FindWithTag("Player").GetComponent<Collider>();
        var selfCol   = GetComponent<Collider>();
        Physics.IgnoreCollision(playerCol, selfCol);
        levelManager.AddEnemyToList(gameObject);
    }

    void Start()
    {
        // Ищем игрока по тэгу “Player” (рекомендуется назначить тег в инспекторе)
        GameObject playerObj = GameObject.FindWithTag("Player");
        if (playerObj != null)
            playerTransform = playerObj.transform;

         // Устанавливаем рандомную скорость для NavMeshAgent
         if (agent != null)
        {
            agent.speed = Random.Range(2, 10);
        }
        
         
    }

    void Update()
    {
        // Защита от null
        if (playerTransform == null || agent == null)
            return;

        // Даем команду агенту идти к игроку
        agent.SetDestination(playerTransform.position);

        // Обновляем цвет по здоровью (ваша логика)
        float hpPercent = (float)health.Health / health.MaxHealth;
        Color c = new Color(1f, 1f - hpPercent, 1f - hpPercent);
        if (rend != null)
            rend.material.color = c;
    }

    private void OnDeath()
    {
        if (agent != null)
            agent.isStopped = true;
        levelManager.RemoveEnemyFromList(gameObject);
        Destroy(gameObject);
    }
}
