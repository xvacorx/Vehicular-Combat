using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    public float life;

    bool isWalking = false;
    Animator animator;
    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    private void Update()
    {
        if (animator != null)
        {
            if (isWalking) { animator.SetFloat("Speed", 1f); }
            else if (isWalking) { animator.SetFloat("Speed", 0f); }
        } // Walking animation
    }
}
