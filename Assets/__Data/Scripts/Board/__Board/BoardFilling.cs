using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardFilling : BoardAb
{
    private Transform[,] tiles;

    public IEnumerator Fill()
    {
        tiles = Board.BoardGen.Tiles;

        for(int x = 0; x < Board.Size; x++)
        {
            for(int y = 0; y < Board.Size; y++)
            {
                if(tiles[x, y] == null)
                {
                    int yP = y;
                    while(tiles[x, yP] == null)
                    {
                        yP++;
                    }

                    Tiles tile = GetTile(tiles[x, yP]);
                    Vector3 toPos = Board.BoardGen.GetWorldPosition(x, y, -1);
                    StartCoroutine(tile.TileMoving.Moving(toPos));

                    tile.TilePrefab.SetXY(x, y);
                    tiles[x, y] = tiles[x, yP];
                    tiles[x, yP] = null;
                }

                yield return null;
            }
        }
    }
}