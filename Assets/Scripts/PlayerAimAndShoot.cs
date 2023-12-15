using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAimAndShoot : MonoBehaviour
{
    public Transform gunPivot; // Reference to the gun's pivot point
    public GameObject bulletPrefab; // Reference to the bullet prefab
    public float bulletSpeed = 10f; // Speed of the bullet

    private bool isShooting = false; // Flag to check if already shooting

    void Update()
    {
        if (Input.GetButtonDown("Fire1") && !isShooting)
        {
            isShooting = true;
            StartCoroutine(ShootBullet());
        }
    }

    IEnumerator ShootBullet()
    {
        // Get the mouse position in world space
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = gunPivot.position.z; // Ensure it's at the same Z as gunPivot

        // Calculate the direction from the gun towards the mouse position
        Vector3 shootingDirection = (mousePosition - gunPivot.position).normalized;

        // Instantiate the bullet prefab at the gun's position and rotation
        GameObject bullet = Instantiate(bulletPrefab, gunPivot.position, Quaternion.identity);

        // Get the Bullet script component from the instantiated bullet
        Bullet bulletScript = bullet.GetComponent<Bullet>();

        // Set the bullet's movement direction using the Bullet script
        if (bulletScript != null)
        {
            bulletScript.ShootInDirection(shootingDirection, bulletSpeed);
        }
        else
        {
            Debug.LogError("Bullet script not found!");
        }

        // Wait for a short duration before allowing shooting again
        yield return new WaitForSeconds(0.1f);
        isShooting = false;
    }
}
