using UnityEngine;
using System.Collections;

public class PoolObject : MonoBehaviour
{
    public Pool pool;

    public void SaveToPool()
    {
        pool.SaveToPool(gameObject);
    }
}
