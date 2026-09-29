using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    [SerializeField] private ObjectPool obstaclePool;

    [Header("Spawn")]
    [SerializeField] private float spawnX = 14f;
    [SerializeField] private float spawnInterval = 2.5f;

    private float timer;

    private void Update()
    {
        timer += Time.deltaTime;

        if (timer >= spawnInterval)
        {
            timer = 0f;
            SpawnObstacle();
        }
    }

    private void SpawnObstacle()
    {
        GameObject obstacle = obstaclePool.GetObject();

        obstacle.transform.position = new Vector3(
            spawnX,
            0f,
            0f
        );

        obstacle.transform.rotation = Quaternion.identity;

        AsteroidObstacle asteroidObstacle =
            obstacle.GetComponent<AsteroidObstacle>();

        if (asteroidObstacle != null)
        {
            asteroidObstacle.Initialize(obstaclePool);
        }
    }
}