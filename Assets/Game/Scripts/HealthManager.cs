using System;                           // Пространство имён .NET с базовыми типами (не используется напрямую, но часто подключается по умолчанию)
using UnityEngine;                      // Основное пространство имён Unity — содержит MonoBehaviour, Mathf и пр.
using UnityEngine.Events;               // Подключает UnityEvent — систему событий Unity

public class HealthManager : MonoBehaviour   // Класс HealthManager, наследник MonoBehaviour, чтобы его можно было вешать на GameObject
{
    public int MaxHealth;                // Публичное поле — максимальное здоровье, задаётся в инспекторе
    public int Health {                  // Авто-свойство для чтения текущего здоровья извне, скрывая сеттер
        get { return health; }           //   геттер возвращает приватное поле health
        private set { health = value; }  //   приватный сеттер позволяет менять health только внутри этого класса
    }
    private int health;                  // Приватное поле, хранящее текущее здоровье

    public UnityEvent DeathEvent;        // Событие без параметров, вызывается при смерти
    public UnityEvent<int> ChangeEvent;  // Событие с параметром int, передаёт величину изменения здоровья

    private void Awake()                 // Awake вызывается одним из первых — при создании объекта
    {
        health = MaxHealth;             // Инициализируем текущее здоровье максимальным
    }

    private void HandleHealth()          // Вспомогательный метод для проверки границ и обработки смерти
    {
        Health = Mathf.Min(MaxHealth, Health); // Если health > MaxHealth, обрезаем до MaxHealth
        Health = Mathf.Max(0, Health);         // Если health < 0, обрезаем до 0

        if (Health == 0)               // Если текущее здоровье упало до нуля
            Die();                     //   вызываем метод Die для обработки смерти
    }

    public void Die()                   // Метод «смерти»
    {
        DeathEvent.Invoke();           // Вызываем все слушатели события DeathEvent
    }

    public void Hit(int amount)        // Метод нанесения урона
    {
        Health -= amount;              // Уменьшаем текущие очки здоровья на amount
        HandleHealth();                // Корректируем границы и проверяем смерть
        ChangeEvent.Invoke(amount);    // Уведомляем всех подписчиков о величине изменения (передаём отрицательное число)
    }

    public void heal(int amount)       // Метод лечения
    {
        Health += amount;              // Увеличиваем текущее здоровье на amount
        HandleHealth();                // Корректируем границы и проверяем смерть (на случай, если health превысило MaxHealth)
        ChangeEvent.Invoke(amount);    // Уведомляем всех подписчиков о величине изменения (передаём положительное число)
    }
}
