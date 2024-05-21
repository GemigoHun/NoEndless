using UnityEngine;

public class BotMoveTile : BotAb
{
    private Transform[,] tiles;
    private Board board;
    private BoardMatches boardMatches;
    private float waitTime = 1;

    public void MoveTile()
    {
        board = Game.Instance.Board;
        tiles = board.BoardGen.Tiles;
        boardMatches = board.BoardMatches;
        TileCanBeMatches pickedTile = Bot.BotPick.WisePick();
        int x = pickedTile.X;
        int y = pickedTile.Y;
        TileDirection direction = pickedTile.TileDirection;
        Tiles tile = GetTile(tiles[x, y]);

        if(direction == TileDirection.TOP)
        {
            tile.TileMoving.MoveToTop(tiles, tile, x, y, waitTime, board, boardMatches);
        }

        if(direction == TileDirection.RIGHT)
        {
            tile.TileMoving.MoveToRight(tiles, tile, x, y, waitTime, board, boardMatches);
        }

        if(direction == TileDirection.BOTTOM)
        {
            tile.TileMoving.MoveToBottom(tiles, tile, x, y, waitTime, board, boardMatches);
        }

        if(direction == TileDirection.LEFT)
        {
            tile.TileMoving.MoveToLeft(tiles, tile, x, y, waitTime, board, boardMatches);
        }

        Battle.Instance.TurnCount--;
    }
}