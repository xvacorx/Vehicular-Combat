using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ShooterEnemy : Enemy
{
    Transform player;

    [SerializeField] GameObject projectilePrefab;
    [SerializeField] Transform firePoint;

    public float followDistance = 20f;
    public float stopDistance = 15f;
    public float moveSpeed = 5f;

    bool isWalking;
    Animator animator;
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        animator = GetComponent<Animator>();
        StartCoroutine(ShootRoutine());
    }
    private void Update()
    {
        if (player != null)
        {
            Vector3 lookPosition = new Vector3(player.position.x, transform.position.y, player.position.z);
            transform.LookAt(lookPosition);

            float distanceToPlayer = Vector3.Distance(transform.position, player.position);

            if (distanceToPlayer > followDistance)
            {
                MoveTowardsPlayer();
            }
            else if (distanceToPlayer < stopDistance)
            {
                MoveAwayFromPlayer();
            }
            else { isWalking = false; }
        }
        if (isWalking) { animator.SetFloat("Speed", 1f); }
        else { animator.SetFloat("Speed", 0f); }
    }

    private void MoveTowardsPlayer()
    {
        Vector3 direction = (player.position - transform.position).normalized;
        transform.position += direction * moveSpeed * Time.deltaTime;
        isWalking = true;
    }

    private void MoveAwayFromPlayer()
    {
        Vector3 direction = (transform.position - player.position).normalized;
        transform.position += direction * moveSpeed * Time.deltaTime;
        isWalking = true;
    }
    private IEnumerator ShootRoutine()
    {
        while (true)
        {
            Debug.Log("Starting attack animation");
            animator.SetTrigger("Attack");
            yield return new WaitForSeconds(0.25f);

            Debug.Log("Shooting");
            Shoot();
            yield return new WaitForSeconds(2.75f);
        }
    }
    private void Shoot()
    {
        Debug.Log("Enemy Shoot");
        Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
    }
}