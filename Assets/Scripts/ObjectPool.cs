using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [Header("Pool Settings")]
    [SerializeField] private GameObject prefab;
    [SerializeField] private int poolSize = 10;

    private readonly Queue<GameObject> pool = new Queue<GameObject>();

    private void Awake()
    {
        CreatePool();
    }

    private void CreatePool()
    {
        for (int i = 0; i < poolSize; i++)
        {
            GameObject obj = Instantiate(prefab, transform);

            obj.SetActive(false);

            pool.Enqueue(obj);
        }
    }

    public GameObject GetObject()
    {
        GameObject obj;

        if (pool.Count > 0)
        {
            obj = pool.Dequeue();
        }
        else
        {
            obj = Instantiate(prefab, transform);
        }

        obj.SetActive(true);

        return obj;
    }

    public void ReturnObject(GameObject obj)
    {
        obj.SetActive(false);

        obj.transform.SetParent(transform);

        pool.Enqueue(obj);
    }
}