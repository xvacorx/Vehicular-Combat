using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Skills : MonoBehaviour
{
    [SerializeField] GameObject impactEffect;
    [SerializeField] GameObject impactEffect2;
    public float damage;
    public float cooldown = 5f;
    public float force = 500f;
    public float radius = 10f;
    public TextMeshProUGUI cooldownText;

    private float nextAbilityTime = 0f;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && Time.time >= nextAbilityTime)
        {
            UseAbility();
            nextAbilityTime = Time.time + cooldown; 
        }

        UpdateCooldownText();
    }

    void UseAbility()
    {
        Quaternion impactRotation = Quaternion.Euler(0, 0, 0);
        Instantiate(impactEffect, transform.position + new Vector3(0, 2, 0), impactRotation);
        Instantiate(impactEffect2, transform.position, impactRotation);
        Collider[] colliders = Physics.OverlapSphere(transform.position, radius);
        foreach (Collider collider in colliders)
        {
            Enemy enemy = collider.gameObject.GetComponent<Enemy>();
            Rigidbody rb = collider.GetComponent<Rigidbody>();
            if (rb != null && rb.gameObject != gameObject)
            {
                Vector3 direction = rb.transform.position - transform.position;
                rb.AddForce(direction.normalized * force);
            }
            if (enemy != null)
            {
                enemy.LoseLife(damage);
            }
        }
    }

    void UpdateCooldownText()
    {
        float remainingCooldown = Mathf.Max(0, nextAbilityTime - Time.time);
        if (remainingCooldown > 0) { cooldownText.text = $"Cooldown: {remainingCooldown:F2} s"; }
        else { cooldownText.text = "Boom Ready"; }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
