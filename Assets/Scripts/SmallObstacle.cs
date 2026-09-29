using UnityEngine;

public class SmallObstacleSpawner : MonoBehaviour
{
    [Header("Pool")]
    [SerializeField] private ObjectPool smallObstaclePool;

    [Header("Spawn")]
    [SerializeField] private float spawnX = 12f;
    [SerializeField] private float spawnInterval = 1f;

    [Header("Random Position")]
    [SerializeField] private float minY = -4f;
    [SerializeField] private float maxY = 4f;

    [Header("Random Scale")]
    [SerializeField] private float minScale = 0.5f;
    [SerializeField] private float maxScale = 1f;

    private float spawnTimer;

    private void Update()
    {
        spawnTimer += Time.deltaTime;

        if (spawnTimer >= spawnInterval)
        {
            spawnTimer = 0f;

            SpawnSmallObstacle();
        }
    }

    private void SpawnSmallObstacle()
    {
        GameObject obstacle = smallObstaclePool.GetObject();

        float randomY = Random.Range(minY, maxY);
        float randomScale = Random.Range(minScale, maxScale);

        obstacle.transform.position = new Vector3(
            spawnX,
            randomY,
            0f
        );

        obstacle.transform.localScale = Vector3.one * randomScale;

        ObstacleMover mover = obstacle.GetComponent<ObstacleMover>();

        if (mover != null)
        {
            mover.Initialize(smallObstaclePool);
        }
    }
}