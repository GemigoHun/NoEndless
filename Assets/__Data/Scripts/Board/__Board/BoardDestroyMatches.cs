using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardDestroyedMatches : BoardAb
{
    private Transform[,] tiles;
    private IEnumerator destroy;
    private TileSpawner tileSpawner;

    public void StartDestroy()
    {
        destroy = DestroyMatches();
        StartCoroutine(destroy);
    }

    public IEnumerator DestroyMatches()
    {
        tiles = Board.BoardGen.Tiles;

        DestroyM();
        Board.BoardFilling.Fill();
        StartCoroutine(Board.BoardGen.GenFullBoard());
        Board.BoardMatches.MarkAsMatches(tiles);

        while(HasMatches())
        {
            yield return new WaitForSeconds(1);
            if(destroy != null) StopCoroutine(destroy);

            destroy = DestroyMatches();

            StartCoroutine(destroy);
        }
    }

    public bool HasMatches()
    {
        for(int x = 0; x < Board.Size; x++)
        {
            for(int y = 0; y < Board.Size; y++)
            {
                Tiles t = GetTile(tiles[x, y]);

                if(t.TilePrefab.CanBeDestroyed) return true;
            }
        }

        return false;
    }

    public void DestroyM()
    {
        tileSpawner = Game.Instance.TileSpawner;

        for(int x = 0; x < Board.Size; x++)
        {
            for(int y = 0; y < Board.Size; y++)
            {
                Tiles t = GetTile(tiles[x, y]);

                if(t.TilePrefab.CanBeDestroyed)
                {
                    tileSpawner.Despawn(tiles[x, y]);
                    //Destroy(tiles[x, y].gameObject);
                    tiles[x, y] = null;
                }
            }
        }
    }
}