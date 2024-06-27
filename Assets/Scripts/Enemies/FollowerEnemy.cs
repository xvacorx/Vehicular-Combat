using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class FollowerEnemy : Enemy
{
    public float speed = 3f;

    public Transform player;

    Animator animator;
    private void Start()
    {
        animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }
    void Update()
    {
        if (player != null)
        {
            isWalking = true;
            Vector3 direction = player.position - transform.position;
            direction.Normalize();

            transform.position += direction * speed * Time.deltaTime;

            Vector3 lookPosition = new Vector3(player.position.x, transform.position.y, player.position.z);
            transform.LookAt(player.position);
        }
        else { isWalking = false; }
        if (isWalking) { animator.SetFloat("Speed", 1f); }
        else { animator.SetFloat("Speed", 0f); }
    }
    public override void LoseLife(float hitDamage)
    {
        life -= hitDamage;
        if (life <= 0)
        {
            Die();
        }
    }
    public void Die()
    {
        animator.SetTrigger("Damage");
        Destroy(gameObject, 1f);
    }
}