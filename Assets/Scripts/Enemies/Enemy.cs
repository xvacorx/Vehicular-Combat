using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.UIElements;

public abstract class Enemy : MonoBehaviour
{
    public float life;
    public float damage;

    public  bool isWalking = false;

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PlayerProjectile") && PlayerManager.Instance != null)
        {
            float playerDamage = PlayerManager.Instance.damage;
            LoseLife(playerDamage);
        }
    }
    public void LoseLife(float hitDamage)
    {
        life -= hitDamage;
        if (life <= 0)
        {
            Destroy(gameObject);
        }
    }
    public void Despawn()
    {
        Destroy(gameObject);
    }
}
