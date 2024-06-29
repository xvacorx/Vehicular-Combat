using System.Collections;
using UnityEngine;

public class ExplodingEnemy : Enemy
{
    private Transform player;
    private Animator anim;
    [SerializeField] private float explosionRadius = 5f;
    [SerializeField] private float explosionDamage = 15f;
    [SerializeField] private GameObject explosionEffect;
    [SerializeField] private float explosionOffset = 2f;
    private bool isExploding = false;
    private GameObject explode;

    Rigidbody rb;
    MeshCollider collission;
    private void Start()
    {
        collission = GetComponent<MeshCollider>();
        rb = GetComponent<Rigidbody>();
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if (player != null)
        {
            Vector3 lookPosition = new Vector3(player.position.x, transform.position.y, player.position.z);
            transform.LookAt(lookPosition);
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
        if (!isExploding)
        {
            isExploding = true;
            anim.SetTrigger("Damage");
            StartCoroutine(ExplodeAfterDelay(0.9f));
        }
    }

    private IEnumerator ExplodeAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        Vector3 explosionPosition = transform.position - transform.forward * explosionOffset;

        if (explosionEffect != null)
        {
            explode = Instantiate(explosionEffect, explosionPosition, transform.rotation);
        }

        Collider[] hitColliders = Physics.OverlapSphere(explosionPosition, explosionRadius);
        foreach (var hitCollider in hitColliders)
        {
            PlayerManager targetHealth = hitCollider.GetComponent<PlayerManager>();
            if (targetHealth != null)
            {
                targetHealth.LoseHealth(explosionDamage);
            }

            Enemy enemy = hitCollider.GetComponent<Enemy>();
            if (enemy != null && enemy != this)
            {
                enemy.LoseLife(explosionDamage);
            }
        }

        if (explode != null)
        {
            Destroy(explode, 2f);
        }

        Destroy(gameObject);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position - transform.forward * explosionOffset, explosionRadius);
    }
}