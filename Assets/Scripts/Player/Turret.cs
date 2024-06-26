using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    [SerializeField] Transform horizontalRotationTransform;
    [SerializeField] Transform verticalRotationTransform;

    void Update()
    {
        GameObject closestEnemy = FindClosestEnemy();
        if (closestEnemy != null)
        {
            // Dirección hacia el enemigo
            Vector3 direction = closestEnemy.transform.position - horizontalRotationTransform.position;

            // Rotación horizontal (Y)
            Vector3 horizontalDirection = new Vector3(direction.x, 0, direction.z);
            Quaternion horizontalRotation = Quaternion.LookRotation(horizontalDirection);
            horizontalRotationTransform.rotation = horizontalRotation;

            // Rotación vertical (X)
            Vector3 verticalDirection = horizontalRotationTransform.InverseTransformDirection(direction);
            float verticalAngle = Mathf.Atan2(verticalDirection.y, verticalDirection.z) * Mathf.Rad2Deg;
            verticalRotationTransform.localRotation = Quaternion.Euler(-verticalAngle, 0, 0); // Invertir el ángulo
        }
    }

    GameObject FindClosestEnemy()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject closestEnemy = null;
        float closestDistanceSqr = Mathf.Infinity;
        Vector3 currentPosition = transform.position;

        foreach (GameObject enemy in enemies)
        {
            Vector3 directionToEnemy = enemy.transform.position - currentPosition;
            float dSqrToEnemy = directionToEnemy.sqrMagnitude;
            if (dSqrToEnemy < closestDistanceSqr)
            {
                closestDistanceSqr = dSqrToEnemy;
                closestEnemy = enemy;
            }
        }

        return closestEnemy;
    }
}
