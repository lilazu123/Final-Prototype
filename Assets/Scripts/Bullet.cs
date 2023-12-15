using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Vector3 direction;
    private float speed;

    public void ShootInDirection(Vector3 dir, float bulletSpeed)
    {
        direction = dir.normalized;
        speed = bulletSpeed;

        // Destroy the bullet after the specified lifetime
        Destroy(gameObject, 4f);
    }

    void Update()
    {
        // Move the bullet in its direction at a specified speed
        transform.Translate(direction * speed * Time.deltaTime);
    }

    void OnCollisionEnter(Collision collision)
    {
        // Optional: Handle collision behavior if needed
        Destroy(gameObject);
    }
}
