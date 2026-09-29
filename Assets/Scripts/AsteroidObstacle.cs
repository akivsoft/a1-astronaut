using UnityEngine;

public class AsteroidObstacle : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float despawnX = -15f;

    [Header("Gap")]
    [SerializeField] private float minGap = 3.5f;
    [SerializeField] private float maxGap = 5f;

    [Header("Vertical Position")]
    [SerializeField] private float minCenterY = -1.5f;
    [SerializeField] private float maxCenterY = 1.5f;

    [Header("Asteroids")]
    [SerializeField] private Transform topAsteroids;
    [SerializeField] private Transform bottomAsteroids;

    private ObjectPool pool;

    public void Initialize(ObjectPool obstaclePool)
    {
        pool = obstaclePool;

        SetupObstacle();
    }

    private void Update()
    {
        transform.position += Vector3.left * moveSpeed * Time.deltaTime;

        if (transform.position.x <= despawnX)
        {
            ReturnToPool();
        }
    }

    private void SetupObstacle()
    {
        float gap = Random.Range(minGap, maxGap);
        float centerY = Random.Range(minCenterY, maxCenterY);

        float halfGap = gap / 2f;

        // Move the top asteroid group upward.
        topAsteroids.localPosition = new Vector3(
            0f,
            centerY + halfGap,
            0f
        );

        // Move the bottom asteroid group downward.
        bottomAsteroids.localPosition = new Vector3(
            0f,
            centerY - halfGap,
            0f
        );
    }

    private void ReturnToPool()
    {
        if (pool != null)
        {
            pool.ReturnObject(gameObject);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
}