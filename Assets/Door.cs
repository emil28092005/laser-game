using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public bool isOpened;

    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isOpened)
        {
            animator.SetBool("Opened", true);
        }
        else
        {
            animator.SetBool("Opened", false);
        }
    }
}
