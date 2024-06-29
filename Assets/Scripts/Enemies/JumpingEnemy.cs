using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpingEnemy : Enemy
{
    public Transform player;
    public float jumpForce = 10f;
    public float jumpInterval = 5f;
    public GameObject impactEffect;
    public float impactRadius = 5f;
    public int stompDamage = 10;

    Rigidbody rb;
    MeshCollider collission;
    Animator animator;
    void Start()
    {
        collission = GetComponent<MeshCollider>();
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
        StartCoroutine(JumpCycle());
    }
    private void Update()
    {
        transform.LookAt(player.position);
    }
    IEnumerator JumpCycle()
    {
        while (true)
        {
            JumpTowardsPlayer();
            yield return new WaitForSeconds(jumpInterval);
        }
    }

    void JumpTowardsPlayer()
    {
        Vector3 direction = (player.position - transform.position).normalized;
        direction.y = 0;
        Vector3 jumpDirection = direction * jumpForce + Vector3.up * jumpForce;
        rb.AddForce(jumpDirection, ForceMode.Impulse);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            GenerateImpact();
        }
    }

    void GenerateImpact()
    {
        Quaternion impactRotation = Quaternion.Euler(-90, 0, 0);
        Instantiate(impactEffect, transform.position, impactRotation);

        Collider[] hitColliders = Physics.OverlapSphere(transform.position, impactRadius);
        foreach (Collider hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag("Player"))
            {
                hitCollider.GetComponent<PlayerManager>().LoseHealth(stompDamage);
            }
        }
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
        gameObject.tag = "Untagged";
        collission.enabled = false;
        rb.isKinematic = true;
        animator.SetTrigger("Damage");
        Destroy(gameObject, 1f);
    }

    void OnDrawGizmos()
    {
        // Draw the impact area in the editor
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, impactRadius);
    }
}
