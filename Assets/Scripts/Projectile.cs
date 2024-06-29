using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] GameObject explosionEffect;
    public float speed = 20f;
    public float lifeTime = 5f;

    [SerializeField] bool enemy;
    [SerializeField] bool player;
    private void Start()
    {
        Destroy(gameObject, lifeTime);
    }
    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (player)
        {
            if (!other.gameObject.CompareTag("Player") && !other.gameObject.CompareTag("EnemyProjectile") && !other.gameObject.CompareTag("PowerUp"))
            {
                GameObject explosion = Instantiate(explosionEffect, transform.position, Quaternion.identity);
                Destroy(gameObject);
                Destroy(explosion, 1f);
            }
        }
        if (enemy)
        {
            if (!other.gameObject.CompareTag("Enemy") && !other.gameObject.CompareTag("EnemyProjectile") && !other.gameObject.CompareTag("PowerUp"))
            {
                if(other.gameObject.TryGetComponent(out PlayerManager player)) { player.LoseHealth(5f); }
                GameObject explosion = Instantiate(explosionEffect, transform.position, Quaternion.identity);
                Destroy(gameObject);
                Destroy(explosion, 1f);
            }
        }
    }
}
