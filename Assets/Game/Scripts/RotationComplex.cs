using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationComplex : MonoBehaviour
{
    public Transform origin;
    public float radius = 1.0f;
    public float rotateInterval = 10f;
    public float tShift = 0.0f;

    public float wave = 0.3f;
    public float waveInterval = 2f;

    private float rotation;
    private float t = 0f;
    private float waveY;

    // Start is called before the first frame update
    void Start()
    {
        t = tShift;
    }

    // Update is called once per frame
    void Update()
    {
        t += Time.deltaTime;
        rotation = (t / rotateInterval) * 360f;
        waveY = Mathf.Sin(t / waveInterval) * wave;

        transform.position = origin.position + Quaternion.Euler(0f, rotation, 0f) * (origin.forward * radius) + new Vector3(0f, waveY, 0f);
    }
}
