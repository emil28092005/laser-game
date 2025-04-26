using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public HealthManager health;
    private CharacterController cc;
    private Rigidbody rb;

    public Transform cameraTransform;

    public float sensitivity = 100.0f;
    public float walkSpeed = 8.0f;
    public float sprintSpeed = 16.0f;
    public float gravity = 1f;

    void Start()
    {
        health = GetComponent<HealthManager>();
        if (health != null)
            health.DeathEvent.AddListener(OnDeath);

        cc = GetComponent<CharacterController>();
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // lock the cursor
        Cursor.lockState = CursorLockMode.Locked;

        // rotate the camera
        Vector3 rotation = new Vector3(-Input.GetAxis("Mouse Y"), Input.GetAxis("Mouse X"), 0);
        rotation *= sensitivity;

        //cameraTransform.eulerAngles += rotation * Time.deltaTime;

        transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y + rotation.y * Time.deltaTime, 0);
        cameraTransform.eulerAngles = new Vector3(cameraTransform.eulerAngles.x + rotation.x * Time.deltaTime, transform.eulerAngles.y, 0);

        // get rid of the z tilt
        //cameraTransform.eulerAngles = new Vector3(cameraTransform.eulerAngles.x, 0, 0);


        // walking/sprinting logic
        float moveSpeed = Input.GetKey(KeyCode.LeftShift) ? sprintSpeed : walkSpeed;
        Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"))/*.normalized*/ * moveSpeed;
        
        // make it relative to the camera rotation
        movement = Quaternion.Euler(0, cameraTransform.eulerAngles.y, 0) * movement;

        // apply the speed
        //transform.position += movement * Time.deltaTime;
        cc.Move(movement * Time.deltaTime);

        cc.Move(new Vector3(0f, -gravity * Time.deltaTime, 0f));
    }

    public void OnDeath()
    {
        Debug.Log("player died");
        Destroy(gameObject); // уничтожить игрока

    }
}