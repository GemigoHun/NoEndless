using System.Collections.Generic;
using UnityEngine;

public class Spawner : GMono
{
    [SerializeField] private List<Transform> prefabs;
    [SerializeField] private Transform holder;
    [SerializeField] protected List<Transform> objPool;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadHolder();
        LoadPrefabs();
    }

    private void LoadHolder()
    {
        if(holder != null) return;

        holder = transform.Find("Holder");
    }

    private void LoadPrefabs()
    {
        if(prefabs.Count > 0) return;

        Transform prefabObjs = transform.Find("Prefabs");

        foreach(Transform prefabObj in prefabObjs)
        {
            prefabs.Add(prefabObj);
        }

        HidePrefabs();
    }

    private void HidePrefabs()
    {
        foreach(Transform prefabObj in prefabs)
        {
            prefabObj.gameObject.SetActive(false);
        }
    }

    public Transform Spawn(Transform prefab, Vector3 pos, Quaternion rot)
    {
        Transform obj = GetPrefabByName(prefab);

        if(obj == null) Debug.LogError("Cannot find prefab name: " + prefab.name);

        Transform newObj = GetObjFromPool(obj);

        newObj.SetPositionAndRotation(pos, rot);

        return newObj;
    }

    private Transform GetPrefabByName(Transform prefab)
    {
        foreach(Transform obj in prefabs)
        {
            if(prefab.name == obj.name) return prefab;
        }

        return null;
    }

    private Transform GetObjFromPool(Transform obj)
    {
        foreach(Transform poolObj in objPool)
        {
            if(poolObj == null) continue;

            if(poolObj.name == obj.name)
            {
                objPool.Remove(poolObj);
                return poolObj;
            }
        }

        Transform newObj = Instantiate(obj);
        newObj.name = obj.name;

        newObj.SetParent(holder);

        return newObj;
    }

    public virtual void Despawn(Transform obj)
    {
        if(objPool.Contains(obj)) return;
        objPool.Add(obj);
        obj.gameObject.SetActive(false);
    }
}