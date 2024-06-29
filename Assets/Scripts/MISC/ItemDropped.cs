using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDropped : MonoBehaviour
{
    public float rotationSpeed = 45f;
    public float minHeight = 1.5f;

    void Update()
    {
        transform.Rotate(0, rotationSpeed * Time.deltaTime, 0);

        if (transform.position.y < minHeight)
        {
            Vector3 newPosition = transform.position;
            newPosition.y = minHeight;
            transform.position = newPosition;
        }
    }
}
