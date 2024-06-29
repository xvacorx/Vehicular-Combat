using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PowerUp : MonoBehaviour
{
    public float lifeTime;
    public abstract void ApplyPowerUp(GameObject player);
    private void Start()
    {
        Destroy(gameObject, lifeTime);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ApplyPowerUp(other.gameObject);
            Destroy(gameObject);
        }
    }
}
