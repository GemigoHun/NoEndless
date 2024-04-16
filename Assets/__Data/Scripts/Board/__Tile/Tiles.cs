using UnityEngine;

public class Tiles : GMono
{
    [SerializeField] private TilePrefab tilePrefab;

    public TilePrefab TilePrefab => tilePrefab;

    [SerializeField] private TileList tileList;

    public TileList TileList => tileList;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadTilePrefab();
        LoadTileList();
    }

    private void LoadTilePrefab()
    {
        if(tilePrefab != null)  return;

        tilePrefab = GetComponentInChildren<TilePrefab>();
    }

    private void LoadTileList()
    {
        if(tileList != null) return;

        tileList = GetComponentInChildren<TileList>();
    }
}