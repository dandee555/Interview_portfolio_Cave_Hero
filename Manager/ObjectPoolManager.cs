using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ObjectPoolManager : MonoBehaviour
{
    private List<PooledObjects> _pools        = new List<PooledObjects>();
    private List<GameObject>    _emptyHolders = new List<GameObject>();
    private GameObject          _pooledObjects;

    public static ObjectPoolManager Instance { get; private set; }

    public enum PoolType
    {
        Particles,
        GameObjects,
        SFX,
        Text
    }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        SetupEmptyHolders();
    }

    private void SetupEmptyHolders()
    {
        _pooledObjects = new GameObject("Pooled Objects");

        var typeName = Enum.GetNames(typeof(PoolType));

        for(int i = 0; i < typeName.Length; i++)
        {
            var emptyHolderObj = new GameObject(typeName[i]);
            emptyHolderObj.transform.SetParent(_pooledObjects.transform);
            _emptyHolders.Add(emptyHolderObj);
        }

        DontDestroyOnLoad(_pooledObjects);
    }

    private GameObject GetParentObj(PoolType poolType)
    {
        int index = (int)poolType;
        if(index < 0 || index >= _emptyHolders.Count)
        {
            throw new IndexOutOfRangeException($"Can't get parent obj because {poolType} is invalid");
        }

        return _emptyHolders[index];
    }

    public GameObject SpawnObject(GameObject objectToSpawn, Vector3 spawnPosition, Quaternion spawnRotation, PoolType poolType = PoolType.GameObjects)
    {
        //find the object pool where the "objectToSpawn" is located
        var spawnPool = _pools.Find(p => p.Name == objectToSpawn.name);

        //if the pool doesn't exist, create a new one, and add to _pools
        if(spawnPool == null)
        {
            spawnPool = new PooledObjects(objectToSpawn.name);
            _pools.Add(spawnPool);
        }

        //check if there are any inactive objects in the spawnPool
        var spawnableObject = spawnPool.InactiveObjects.FirstOrDefault();

        if (!spawnableObject)
        {
            //if there are no any inactive objects, create a new one 
            spawnableObject = Instantiate(objectToSpawn, spawnPosition, spawnRotation);
            spawnableObject.transform.SetParent(GetParentObj(poolType).transform);
        }
        else
        {
            //if there is an inactive object, activate it
            spawnableObject.transform.SetPositionAndRotation(spawnPosition, spawnRotation);
            _ = spawnPool.InactiveObjects.Remove(spawnableObject);
            spawnableObject.SetActive(true);
        }

        return spawnableObject;
    }

    public void ReturnObjectToPool(GameObject objectToPool)
    {
        if(!objectToPool)
        {
            return; 
        }

        string origName = objectToPool.name.Replace("(Clone)", string.Empty);
        var origPool    = _pools.Find(p => p.Name == origName);

        if(origPool == null)
        {
            throw new NullReferenceException($"Can't find pool of name {origName}");
        }
        else
        {
            objectToPool.SetActive(false);
            origPool.InactiveObjects.Add(objectToPool);
        }
    }
}

public class PooledObjects
{
    private string _name;
    public List<GameObject> InactiveObjects = new List<GameObject>();

    public string Name => _name;

    public PooledObjects(string name) { _name = name; }
}