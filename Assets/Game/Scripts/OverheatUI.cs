using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[RequireComponent(typeof(Image))]
public class BarClickHighlighter : MonoBehaviour
{
    [Header("Настройки кликов")]
    [Tooltip("Сколько раз нужно кликнуть ПКМ, чтобы поменять цвет")]
    public int clicksThreshold = 6;

    [Header("Цвет")]
    [Tooltip("Новый цвет после достижения порога")]
    public Color highlightColor = new Color32(0xD5, 0x31, 0x31, 0xFF);

    [Header("Опции сброса")]
    [Tooltip("Сбрасывать счётчик после смены цвета?")]
    public bool resetAfterHighlight = true;

    [Header("Здоровье игрока при перегреве")]
    public HealthManager playerHealth;
    public int overheatDamage = 10;
    public float damageInterval = 2f;

    private int clickCount = 0;
    private Image barImage;
    private Color originalColor;
    private bool isOverheated = false;
    private Coroutine overheatDamageCoroutine;

    void Awake()
    {
        barImage = GetComponent<Image>();
        originalColor = barImage.color;
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            clickCount++;

            if (clickCount >= clicksThreshold && !isOverheated)
            {
                Debug.Log("Threshold reached — changing color!");
                HighlightBar();
                StartOverheatDamage();

                if (resetAfterHighlight)
                    clickCount = 0;
            }
        }
    }

    private void HighlightBar()
    {
        barImage.color = highlightColor;
    }

    private IEnumerator ResetColorAfterSeconds(float sec)
    {
        yield return new WaitForSeconds(sec);
        barImage.color = originalColor;
    }

    private void StartOverheatDamage()
    {
        isOverheated = true;
        overheatDamageCoroutine = StartCoroutine(OverheatDamageCoroutine());
    }

    private IEnumerator OverheatDamageCoroutine()
    {
        while (isOverheated)
        {
            if (playerHealth != null)
            {
                playerHealth.Hit(overheatDamage);
            }
            yield return new WaitForSeconds(damageInterval);
        }
    }

    public void ResetOverheat()
    {
        clickCount = 0;
        isOverheated = false;

        if (overheatDamageCoroutine != null)
        {
            StopCoroutine(overheatDamageCoroutine);
            overheatDamageCoroutine = null;
        }

        if (barImage != null)
        {
            barImage.color = originalColor;
        }
    }
}
