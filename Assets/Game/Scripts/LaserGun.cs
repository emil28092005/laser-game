using System.Collections;                     // Подключает пространство имён для базовых коллекций Unity (не используется напрямую, но часто включается по умолчанию)
using System.Collections.Generic;             // Подключает пространство имён для обобщённых коллекций (List, Dictionary и т.д.)
using UnityEngine;                            // Основное пространство имён Unity — всё, что связано с движком и игровыми объектами

public class LaserGun : MonoBehaviour          // Объявление класса LaserGun, наследующегося от MonoBehaviour — базового класса всех компонентов Unity
{
    //private LineRenderer laserLine;          // Закомментированное поле для LineRenderer (если бы вы рисовали лазер линией)

    public GameObject laserObject;             // Ссылка на объект-луч (например, цилиндр или спрайт), который будет включаться/выключаться
    public float laserDuration = 1f;           // Время (в секундах), в течение которого лазер остаётся активным (используется, если рисовать корутиной)
    public Transform laserOrigin;              // Точка (Transform) в пространстве, откуда исходит лазер
    public Camera playerCamera;                // Камера игрока — нужна, чтобы определить направление выстрела
    public float laserRange = 50f;             // Максимальная дальность луча

    public LayerMask layermask; 

    void Start()                              // Метод Start вызывается один раз при активации компонента (перед первым Update)
    {
        //laserLine = GetComponent<LineRenderer>();  // Если бы вы использовали LineRenderer, тут получили бы его из текущего объекта
    }
void Update()
{
    if (Input.GetButton("Fire1"))
    {
        Vector3 rayOrigin = playerCamera.ViewportToWorldPoint(new Vector2(0.5f, 0.5f));
        Ray ray = new Ray(rayOrigin, playerCamera.transform.forward);
        RaycastHit hit;

        Vector3 rayEnd;
        Vector3 rayStart = laserOrigin.position;

        if (Physics.Raycast(ray, out hit, laserRange, layermask))
        {
            rayEnd = hit.point;

            // Ищем HealthManager только на первом попавшемся объекте
            HealthManager health = hit.transform.GetComponent<HealthManager>();
            if (health != null && hit.transform.gameObject.tag != "Player")
            {
                health.Die(); // Убиваем врага
            }

            if (hit.transform.gameObject.TryGetComponent<Circuit>(out Circuit circuit))
            {
                circuit.isOn = true;
            }

        }
        else
        {
            rayEnd = rayOrigin + playerCamera.transform.forward * laserRange;
        }

        Vector3 laserVec = (rayEnd - rayStart);
        float laserLength = laserVec.magnitude * 0.5f;
        Vector3 laserDirection = (rayEnd - rayStart).normalized;

        laserObject.transform.position = rayStart;
        laserObject.transform.localScale = new Vector3(0.2f, laserLength, 0.2f);
        laserObject.transform.rotation = transform.rotation;

        laserObject.SetActive(true);
    }
    else
    {
        laserObject.SetActive(false);
    }
}

}