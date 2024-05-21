using System.Collections;
using UnityEngine;

public class BoardDestroyedMatches : BoardAb
{
    private Transform[,] tiles;
    private IEnumerator destroy;
    private TileSpawner tileSpawner;
    private float waitTime = 0;

    public float WaitTime
    {
        get { return waitTime; }
        set { waitTime = value; }
    }

    public IEnumerator DestroyAndFill()
    {
        
        yield return new WaitForSeconds(1);

        tiles = Board.BoardGen.Tiles;

        Board.BoardMatches.MarkAsMatches(tiles);
        Board.BoardMatches.CountTurn();
        StartDestroy();
    }

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
        Board.BoardMatches.CountTurn();

        while(HasMatches())
        {
            yield return new WaitForSeconds(1);
            if(destroy != null) StopCoroutine(destroy);

            destroy = DestroyMatches();

            StartCoroutine(destroy);
        }

        yield return new WaitForSeconds(1);

        Battle.Instance.BotPlayed = false;

        if(Battle.Instance.TurnCount <= 0) Battle.Instance.EndTurn = true;
        else
        {
            Battle.Instance.CanDrag = true;
            Battle.Instance.CountDownTurn = 30;
            Battle.Instance.BotPlayed = false;
        }
        
        Board.BoardMatches.GetListMatches();
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