using UnityEngine;
using System.Collections;

public class PoolManager : MonoBehaviour
{
    // Return pool of object with name
    public Pool FindPool(string _name)
    {
        Pool[] _pools = GetComponentsInChildren<Pool>();

        foreach (Pool _pool in _pools)
        {
            if (_pool.poolObject.name == _name)
            {
                return _pool;
            }
        }

        return null;
    }

    // Return pool of specific type
    public Pool GetPool<T>()
    {
        Pool[] _pools = GetComponentsInChildren<Pool>();

        foreach (Pool _pool in _pools)
        {
            if (_pool.transform.GetChild(0).GetComponent(typeof(T)))
            {
                return _pool;
            }
        }

        return null;
    }
}
