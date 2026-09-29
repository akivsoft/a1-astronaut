using UnityEngine;

public class ObstacleMover : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 4f;
    [SerializeField] private float destroyX = -15f;

    private ObjectPool pool;

    public void Initialize(ObjectPool objectPool)
    {
        pool = objectPool;
    }

    private void Update()
    {
        transform.position += Vector3.left * moveSpeed * Time.deltaTime;

        if (transform.position.x <= destroyX)
        {
            ReturnToPool();
        }
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