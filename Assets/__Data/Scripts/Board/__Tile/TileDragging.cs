using System.Collections;
using UnityEditor;
using UnityEngine;

public class TileDragging : TileAb
{
    [SerializeField] private Vector3 firstMousePos, finalMousePos;
    [SerializeField] private float angle;
    [SerializeField] private float waitTime = 1;
    [SerializeField] private bool reverse = false;
    private Transform[,] tiles;
    private Board board;
    private BoardMatches boardMatches;
    private TileSpawner tileSpawner;

    private void OnMouseDown()
    {
        firstMousePos = to2DVec(InputManager.Instance.MousePos);
    }

    private void OnMouseUp()
    {
        if(transform.parent.position.y >= 4.5f) return;

        finalMousePos = to2DVec(InputManager.Instance.MousePos);
        Vector3 direction = finalMousePos - firstMousePos;
        angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        ChangePos();
    }

    public void ChangePos()
    {
        board = Game.Instance.Board;
        tiles = board.BoardGen.Tiles;
        boardMatches = board.BoardMatches;

        int tileX = Tile.TilePrefab.X;
        int tileY = Tile.TilePrefab.Y;     
        Tiles tile = GetTile(tiles[tileX, tileY]);
        reverse = MoveTile(tile, tileX, tileY);

        if(reverse) return;

        StartCoroutine(waitForChange());
    }

    private IEnumerator waitForChange()
    {
        yield return new WaitForSeconds(1);
        boardMatches.MarkAsMatches(tiles);
        boardMatches.CountTurn();
        //board.BoardDestroyedMatches.StartDestroy();
    }

    private bool MoveTile(Tiles tile, int x, int y)
    {
        if(angle > 45 && angle <= 135 && y + 1 < board.Size)
        {
            Tiles top = GetTile(tiles[x, y + 1]);

            StartCoroutine(Tile.TileMoving.Moving(board.BoardGen.GetWorldPosition(x, y + 1, -1)));
            StartCoroutine(top.TileMoving.Moving(board.BoardGen.GetWorldPosition(x, y, -1)));
            
            if(!boardMatches.CanBeDestroyed(tile, x, y + 1, tiles, TileDirection.TOP)
                && !boardMatches.CanBeDestroyed(top, x, y, tiles, TileDirection.BOTTOM))
            {
                StartCoroutine(Tile.TileMoving.Moving(board.BoardGen.GetWorldPosition(x, y, -1), waitTime));
                StartCoroutine(top.TileMoving.Moving(board.BoardGen.GetWorldPosition(x, y + 1, -1), waitTime));
                return true;            
            }

            top.TilePrefab.SetXY(x, y);
            tile.TilePrefab.SetXY(x, y + 1);

            Transform current = tiles[x, y];
            tiles[x, y] = tiles[x, y + 1];
            tiles[x, y + 1] = current; 
        }

        if(angle > -45 && angle <= 45 && x + 1 < board.Size)
        {
            Tiles right = GetTile(tiles[x + 1, y]);

            StartCoroutine(Tile.TileMoving.Moving(board.BoardGen.GetWorldPosition(x + 1, y, -1)));
            StartCoroutine(right.TileMoving.Moving(board.BoardGen.GetWorldPosition(x, y, -1)));
            
            if(!boardMatches.CanBeDestroyed(tile, x + 1, y, tiles, TileDirection.RIGHT)
                && !boardMatches.CanBeDestroyed(right, x, y, tiles, TileDirection.LEFT))
            {
                StartCoroutine(Tile.TileMoving.Moving(board.BoardGen.GetWorldPosition(x, y, -1), waitTime));
                StartCoroutine(right.TileMoving.Moving(board.BoardGen.GetWorldPosition(x + 1, y, -1), waitTime));             
                return true;
            }

            right.TilePrefab.SetXY(x, y);
            tile.TilePrefab.SetXY(x + 1, y);

            Transform current = tiles[x, y];
            tiles[x, y] = tiles[x + 1, y];
            tiles[x + 1, y] = current; 
        }

        if(angle > -135 && angle <= -45 && y - 1 < board.Size)
        {
            Tiles bottom = GetTile(tiles[x, y - 1]);

            StartCoroutine(Tile.TileMoving.Moving(board.BoardGen.GetWorldPosition(x, y - 1, -1)));
            StartCoroutine(bottom.TileMoving.Moving(board.BoardGen.GetWorldPosition(x, y, -1)));
            
            if(!boardMatches.CanBeDestroyed(tile, x, y - 1, tiles, TileDirection.BOTTOM)
                && !boardMatches.CanBeDestroyed(bottom, x, y, tiles, TileDirection.TOP))
            {
                StartCoroutine(Tile.TileMoving.Moving(board.BoardGen.GetWorldPosition(x, y, -1), waitTime));
                StartCoroutine(bottom.TileMoving.Moving(board.BoardGen.GetWorldPosition(x, y - 1, -1), waitTime));
                return true;        
            }

            bottom.TilePrefab.SetXY(x, y);
            tile.TilePrefab.SetXY(x, y - 1);

            Transform current = tiles[x, y];
            tiles[x, y] = tiles[x, y - 1];
            tiles[x, y - 1] = current; 
        }

        if(angle > 135 || angle <= -135 && x - 1 < board.Size)
        {
            Tiles left = GetTile(tiles[x - 1, y]);

            StartCoroutine(Tile.TileMoving.Moving(board.BoardGen.GetWorldPosition(x - 1, y, -1)));
            StartCoroutine(left.TileMoving.Moving(board.BoardGen.GetWorldPosition(x, y, -1)));
            
            if(!boardMatches.CanBeDestroyed(tile, x - 1, y, tiles, TileDirection.LEFT)
                && !boardMatches.CanBeDestroyed(left, x, y, tiles, TileDirection.RIGHT))
            {
                StartCoroutine(Tile.TileMoving.Moving(board.BoardGen.GetWorldPosition(x, y, -1), waitTime));
                StartCoroutine(left.TileMoving.Moving(board.BoardGen.GetWorldPosition(x - 1, y, -1), waitTime));
                return true;           
            }

            left.TilePrefab.SetXY(x, y);
            tile.TilePrefab.SetXY(x - 1, y);

            Transform current = tiles[x, y];
            tiles[x, y] = tiles[x - 1, y];
            tiles[x - 1, y] = current;
        }

        return false;
    }
}