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
}