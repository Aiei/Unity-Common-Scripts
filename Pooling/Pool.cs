using UnityEngine;
using System.Collections;

public class Pool : MonoBehaviour
{
    public GameObject poolObject;

    public int size = 4;

    [HideInInspector]
    public GameObject[] objects;

    public void Start()
    {
        objects = new GameObject[size];

        for (int a = 0; a < size; a++)
        {
            objects[a] = (GameObject)Instantiate(poolObject);
            PoolObject _poolObject = objects[a].GetComponent<PoolObject>();
            _poolObject.pool = gameObject.GetComponent<Pool>();
            SaveToPool(objects[a]);
        }
    }

    public void SaveToPool(GameObject _object)
    {
        if (_object.activeSelf)
        {
            _object.transform.parent = transform;
            _object.SetActive(false);
        }
    }

    public GameObject GetObject(Transform owner)
    {
        foreach (GameObject _object in objects)
        {
            if (_object.activeSelf != true)
            {
                _object.transform.parent = owner;
                _object.SetActive(true);

                return _object;
            }
        }

        return null;
    }
}
