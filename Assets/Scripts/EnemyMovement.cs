using UnityEngine;
using System.Collections;


public class EnemyMovement : MonoBehaviour
{
    public GameObject enemyPrefab; // Drag your enemy prefab here in the inspector
    public float moveSpeed = 2f;
    private float leftBound;
    private float rightBound;
    public int maxHealth = 2;
    private int currentHealth;

    private bool movingRight = true;

    void Start()
    {
        currentHealth = maxHealth;

        // Calculate the left and right boundaries based on the camera's viewport
        Vector3 leftBoundary = Camera.main.ViewportToWorldPoint(new Vector3(0, 0.5f, 0));
        Vector3 rightBoundary = Camera.main.ViewportToWorldPoint(new Vector3(1, 0.5f, 0));

        leftBound = leftBoundary.x;
        rightBound = rightBoundary.x;

        StartCoroutine(SpawnEnemyRoutine()); // Start the coroutine for spawning enemies
    }

    void Update()
    {
        MoveEnemy();
    }


    void MoveEnemy()
    {
        if (movingRight)
        {
            transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);
            if (transform.position.x >= rightBound)
            {
                movingRight = false;
            }
        }
        else
        {
            transform.Translate(Vector3.left * moveSpeed * Time.deltaTime);
            if (transform.position.x <= leftBound)
            {
                movingRight = true;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Bullet"))
        {
            TakeDamage();
            Destroy(other.gameObject);
        }
    }

    void TakeDamage()
    {
        currentHealth--;
        if (currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }

    IEnumerator SpawnEnemyRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(8f); // Wait for 15 seconds
            SpawnEnemy();
        }
    }

    void SpawnEnemy()
    {
        Vector3 spawnPosition = new Vector3(Random.Range(leftBound, rightBound), transform.position.y, transform.position.z);
        Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
    }
}
