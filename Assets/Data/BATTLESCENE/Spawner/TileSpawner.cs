using System.Collections;
using UnityEngine;

public class TileSpawner : Spawner
{
    [SerializeField] private Transform tileObject;

    public Transform TileObject => tileObject;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadTileObject();
    }

    private void LoadTileObject()
    {
        if(tileObject != null) return;

        tileObject = transform.Find("Prefabs").Find("Tile");
    }

    public override void Despawn(Transform obj)
    {
        if(objPool.Contains(obj)) return;
        objPool.Add(obj);
        obj.gameObject.SetActive(false);
    }
}