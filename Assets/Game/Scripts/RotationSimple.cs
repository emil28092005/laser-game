using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationSimple : MonoBehaviour
{
    public float interval = 10f;
    private float t;

    // Start is called before the first frame update
    void Start()
    {
        t = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        t += Time.deltaTime;
        float rotation = t / interval * 360f;
        transform.rotation = Quaternion.Euler(0f, rotation, 0f);
    }
}
