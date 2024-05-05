using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BoardMatches : BoardAb
{
    [SerializeField] private List<TileCanBeMatches> matches;
    private Transform[,] tiles;
    [SerializeField] private int count = 0;
    [SerializeField] private int totalCount = 0;

    public void MarkAsMatches(Transform[,] tiles)
    {
        for(int x = 0; x < Board.Size; x++)
        {
            for(int y = 0; y < Board.Size; y++)
            {
                if(tiles[x, y] == null) continue;

                Tiles tile = GetTile(tiles[x, y]);

                if(tile.TilePrefab.CanBeDestroyed == false)
                {
                    if(y + 2 < Board.Size)
                    {
                        Tiles tileXY1 = GetTile(tiles[x, y + 1]);
                        Tiles tileXY2 = GetTile(tiles[x, y + 2]);

                        if(tile.TilePrefab.TileEnum == tileXY1.TilePrefab.TileEnum
                            && tile.TilePrefab.TileEnum == tileXY2.TilePrefab.TileEnum)
                        {
                            tile.TilePrefab.CanBeDestroyed = true;
                            if(tileXY1.TilePrefab.CanBeDestroyed == false)  tileXY1.TilePrefab.CanBeDestroyed = true;
                            if(tileXY2.TilePrefab.CanBeDestroyed == false)  tileXY2.TilePrefab.CanBeDestroyed = true;
                        }
                    }

                    if(x + 2 < Board.Size)
                    {
                        Tiles tileX1Y = GetTile(tiles[x + 1, y]);
                        Tiles tileX2Y = GetTile(tiles[x + 2, y]);

                        if(tile.TilePrefab.TileEnum == tileX1Y.TilePrefab.TileEnum
                            && tile.TilePrefab.TileEnum == tileX2Y.TilePrefab.TileEnum)
                        {
                            tile.TilePrefab.CanBeDestroyed = true;
                            if(tileX1Y.TilePrefab.CanBeDestroyed == false)  tileX1Y.TilePrefab.CanBeDestroyed = true;
                            if(tileX2Y.TilePrefab.CanBeDestroyed == false)  tileX2Y.TilePrefab.CanBeDestroyed = true;
                        }
                    }

                    if(y - 2 >= 0)
                    {
                        Tiles tileXYM1 = GetTile(tiles[x, y - 1]);
                        Tiles tileXYM2 = GetTile(tiles[x, y - 2]);

                        if(tile.TilePrefab.TileEnum == tileXYM1.TilePrefab.TileEnum
                            && tile.TilePrefab.TileEnum == tileXYM2.TilePrefab.TileEnum)
                        {
                            tile.TilePrefab.CanBeDestroyed = true;
                            if(tileXYM1.TilePrefab.CanBeDestroyed == false)  tileXYM1.TilePrefab.CanBeDestroyed = true;
                            if(tileXYM2.TilePrefab.CanBeDestroyed == false)  tileXYM2.TilePrefab.CanBeDestroyed = true;
                        }
                    }

                    if(x - 2 >= 0)
                    {
                        Tiles tileXM1Y = GetTile(tiles[x - 1, y]);
                        Tiles tileXM2Y = GetTile(tiles[x - 2, y]);

                        if(tile.TilePrefab.TileEnum == tileXM1Y.TilePrefab.TileEnum
                            && tile.TilePrefab.TileEnum == tileXM2Y.TilePrefab.TileEnum)
                        {
                            tile.TilePrefab.CanBeDestroyed = true;
                            if(tileXM1Y.TilePrefab.CanBeDestroyed == false)  tileXM1Y.TilePrefab.CanBeDestroyed = true;
                            if(tileXM2Y.TilePrefab.CanBeDestroyed == false)  tileXM2Y.TilePrefab.CanBeDestroyed = true;
                        }
                    }
                }
            }
        }
    }

    public bool CanBeDestroyed(Tiles tile, int x, int y, Transform [,] tiles, TileDirection direction)
    {
        if(y + 1 < Board.Size && direction != TileDirection.BOTTOM)
        {
            Tiles tileXY1 = GetTile(tiles[x, y + 1]);
            
            if(y - 1 >= 0)
            {
                Tiles tileXYM1 = GetTile(tiles[x, y - 1]);

                if(tile.TilePrefab.TileEnum == tileXY1.TilePrefab.TileEnum
                    && tile.TilePrefab.TileEnum == tileXYM1.TilePrefab.TileEnum && direction != TileDirection.TOP)
                    return true;
            }
            
            if(y + 2 < Board.Size)
            {
                Tiles tileXY2 = GetTile(tiles[x, y + 2]);

                if(tile.TilePrefab.TileEnum == tileXY1.TilePrefab.TileEnum
                    && tile.TilePrefab.TileEnum == tileXY2.TilePrefab.TileEnum)
                    return true;
            }
        }

        if(x + 1 < Board.Size && direction != TileDirection.LEFT)
        {
            Tiles tileX1Y = GetTile(tiles[x + 1, y]);
            
            if(x - 1 >= 0)
            {
                Tiles tileXM1Y = GetTile(tiles[x - 1, y]);

                if(tile.TilePrefab.TileEnum == tileX1Y.TilePrefab.TileEnum
                    && tile.TilePrefab.TileEnum == tileXM1Y.TilePrefab.TileEnum && direction != TileDirection.RIGHT)
                    return true;
            }
            
            if(x + 2 < Board.Size)
            {
                Tiles tileX2Y = GetTile(tiles[x + 2, y]);

                if(tile.TilePrefab.TileEnum == tileX1Y.TilePrefab.TileEnum
                    && tile.TilePrefab.TileEnum == tileX2Y.TilePrefab.TileEnum)
                    return true;
            }
        }

        if(y - 1 >= 0 && direction != TileDirection.TOP)
        {
            Tiles tileXYM1 = GetTile(tiles[x, y - 1]);
            
            if(y + 1 < Board.Size)
            {
                Tiles tileXY1 = GetTile(tiles[x, y + 1]);

                if(tile.TilePrefab.TileEnum == tileXYM1.TilePrefab.TileEnum
                        && tile.TilePrefab.TileEnum == tileXY1.TilePrefab.TileEnum && direction != TileDirection.BOTTOM)
                        return true;
            }
            
            if(y - 2 >= 0)
            {
                Tiles tileXYM2 = GetTile(tiles[x, y - 2]);

                if(tile.TilePrefab.TileEnum == tileXYM1.TilePrefab.TileEnum
                    && tile.TilePrefab.TileEnum == tileXYM2.TilePrefab.TileEnum)
                    return true;
            }
        }

        if(x - 2 >= 0 && direction != TileDirection.RIGHT)
        {
            Tiles tileXM1Y = GetTile(tiles[x - 1, y]);
            
            if(x + 1 < Board.Size)
            {
                Tiles tileX1Y = GetTile(tiles[x + 1, y]);

                if(tile.TilePrefab.TileEnum == tileXM1Y.TilePrefab.TileEnum
                    && tile.TilePrefab.TileEnum == tileX1Y.TilePrefab.TileEnum && direction != TileDirection.LEFT)
                    return true;
            }
            
            if(x - 2 >= 0)
            {
                Tiles tileXM2Y = GetTile(tiles[x - 2, y]);

                if(tile.TilePrefab.TileEnum == tileXM1Y.TilePrefab.TileEnum
                    && tile.TilePrefab.TileEnum == tileXM2Y.TilePrefab.TileEnum)
                    return true;
            }
        }

        return false;
    }

    public List<TileCanBeMatches> GetListMatches()
    {
        matches = new();
        tiles = Board.BoardGen.Tiles;

        for(int x = 0; x < Board.Size; x++)
        {
            for(int y = 0; y < Board.Size; y++)
            {
                GetTopMatches(x, y);
                GetRightMatches(x, y);
                GetBottomMatches(x, y);
                GetLeftMatches(x, y);
            }
        }

        return matches;
    }

    private void GetTopMatches(int x, int y)
    {
        if(y + 1 >= Board.Size) return;

        int count = 1;

        Tiles tileXY = GetTile(tiles[x, y]);

        if(x + 1 < Board.Size && x - 1 >= 0)
        {
            Tiles tileX1Y = GetTile(tiles[x + 1, y + 1]);
            Tiles tileXM1Y = GetTile(tiles[x - 1, y + 1]);

            if(tileX1Y.TilePrefab.TileEnum == tileXY.TilePrefab.TileEnum
                && tileXM1Y.TilePrefab.TileEnum == tileXY.TilePrefab.TileEnum)
            {
                count = count + 2;

                if(x + 2 < Board.Size)
                {
                    Tiles tileX2Y = GetTile(tiles[x + 2, y + 1]);

                    if(tileX2Y.TilePrefab.TileEnum == tileXY.TilePrefab.TileEnum) count++;
                }

                if(x - 2 >= 0)
                {
                    Tiles tileXM2Y = GetTile(tiles[x - 2, y + 1]);

                    if(tileXM2Y.TilePrefab.TileEnum == tileXY.TilePrefab.TileEnum) count++;
                }
            }
                    
            if(tileX1Y.TilePrefab.TileEnum == tileXY.TilePrefab.TileEnum && x + 2 < Board.Size)
            {
                Tiles tileX2Y = GetTile(tiles[x + 2, y + 1]);

                if(tileX2Y.TilePrefab.TileEnum == tileXY.TilePrefab.TileEnum) count++;
            }

            if(tileXM1Y.TilePrefab.TileEnum == tileXY.TilePrefab.TileEnum && x - 2 > Board.Size)
            {
                Tiles tileXM2Y = GetTile(tiles[x - 2, y + 1]);

                if(tileXM2Y.TilePrefab.TileEnum == tileXY.TilePrefab.TileEnum) count++;
            }
        }

        if(x == 0)
        {
            Tiles tileX1Y = GetTile(tiles[x + 1, y + 1]);
            Tiles tileX2Y = GetTile(tiles[x + 2, y + 1]);

            if(tileXY.TilePrefab.TileEnum == tileX1Y.TilePrefab.TileEnum
                && tileXY.TilePrefab.TileEnum == tileX2Y.TilePrefab.TileEnum) count = count + 2;
        }

        if(x == Board.Size - 1)
        {
            Tiles tileXM1Y = GetTile(tiles[x - 1, y + 1]);
            Tiles tileXM2Y = GetTile(tiles[x - 2, y + 1]);

            if(tileXY.TilePrefab.TileEnum == tileXM1Y.TilePrefab.TileEnum
                && tileXY.TilePrefab.TileEnum == tileXM2Y.TilePrefab.TileEnum) count = count + 2;
        }

        if(y + 3 < Board.Size)
        {
            Tiles tileXY1 = GetTile(tiles[x, y + 1 + 1]);
            Tiles tileXY2 = GetTile(tiles[x, y + 1 + 2]);

            if(tileXY1.TilePrefab.TileEnum == tileXY.TilePrefab.TileEnum
                && tileXY2.TilePrefab.TileEnum == tileXY.TilePrefab.TileEnum) count = count + 2;
        }

        if(count >= 3)
        {
            TileCanBeMatches tile = new(x, y, TileDirection.TOP, tileXY.TilePrefab.TileEnum, count);

            matches.Add(tile);
        }
    }

    private void GetRightMatches(int x, int y)
    {
        if(x + 1 >= Board.Size) return;

        int count = 1;

        Tiles tileXY = GetTile(tiles[x, y]);

        if(y + 1 < Board.Size && y - 1 >= 0)
        {
            Tiles tileXY1 = GetTile(tiles[x + 1, y + 1]);
            Tiles tileXYM1 = GetTile(tiles[x + 1, y - 1]);

            if(tileXY1.TilePrefab.TileEnum == tileXY.TilePrefab.TileEnum
                && tileXYM1.TilePrefab.TileEnum == tileXY.TilePrefab.TileEnum)
            {
                count = count + 2;

                if(y + 2 < Board.Size)
                {
                    Tiles tileXY2 = GetTile(tiles[x + 1, y + 2]);

                    if(tileXY2.TilePrefab.TileEnum == tileXY.TilePrefab.TileEnum) count++;
                }

                if(y - 2 >= 0)
                {
                    Tiles tileXYM2 = GetTile(tiles[x + 1, y - 2]);

                    if(tileXYM2.TilePrefab.TileEnum == tileXY.TilePrefab.TileEnum) count++;
                }
            }
                    
            if(tileXY1.TilePrefab.TileEnum == tileXY.TilePrefab.TileEnum && y + 2 < Board.Size)
            {
                Tiles tileXY2 = GetTile(tiles[x + 1, y + 2]);

                if(tileXY2.TilePrefab.TileEnum == tileXY.TilePrefab.TileEnum) count++;
            }

            if(tileXYM1.TilePrefab.TileEnum == tileXY.TilePrefab.TileEnum && y - 2 > Board.Size)
            {
                Tiles tileXYM2 = GetTile(tiles[x + 1, y - 2]);

                if(tileXYM2.TilePrefab.TileEnum == tileXY.TilePrefab.TileEnum) count++;
            }
        }

        if(y == 0)
        {
            Tiles tileXY1 = GetTile(tiles[x + 1, y + 1]);
            Tiles tileXY2 = GetTile(tiles[x + 1, y + 2]);

            if(tileXY.TilePrefab.TileEnum == tileXY1.TilePrefab.TileEnum
                && tileXY.TilePrefab.TileEnum == tileXY2.TilePrefab.TileEnum) count = count + 2;
        }

        if(y == Board.Size - 1)
        {
            Tiles tileXYM1 = GetTile(tiles[x + 1, y - 1]);
            Tiles tileXYM2 = GetTile(tiles[x + 1, y - 2]);

            if(tileXY.TilePrefab.TileEnum == tileXYM1.TilePrefab.TileEnum
                && tileXY.TilePrefab.TileEnum == tileXYM2.TilePrefab.TileEnum) count = count + 2;
        }

        if(x + 3 < Board.Size)
        {
            Tiles tileX1Y = GetTile(tiles[x + 1 + 1, y]);
            Tiles tileX2Y = GetTile(tiles[x + 1 + 2, y]);

            if(tileX1Y.TilePrefab.TileEnum == tileXY.TilePrefab.TileEnum
                && tileX2Y.TilePrefab.TileEnum == tileXY.TilePrefab.TileEnum) count = count + 2;
        }

        if(count >= 3)
        {
            TileCanBeMatches tile = new(x, y, TileDirection.RIGHT, tileXY.TilePrefab.TileEnum, count);

            matches.Add(tile);
        }
    }

    private void GetBottomMatches(int x, int y)
    {
        if(y - 1 < 0) return;

        int count = 1;

        Tiles tileXY = GetTile(tiles[x, y]);

        if(x + 1 < Board.Size && x - 1 >= 0)
        {
            Tiles tileX1Y = GetTile(tiles[x + 1, y - 1]);
            Tiles tileXM1Y = GetTile(tiles[x - 1, y - 1]);

            if(tileX1Y.TilePrefab.TileEnum == tileXY.TilePrefab.TileEnum
                && tileXM1Y.TilePrefab.TileEnum == tileXY.TilePrefab.TileEnum)
            {
                count = count + 2;

                if(x + 2 < Board.Size)
                {
                    Tiles tileX2Y = GetTile(tiles[x + 2, y - 1]);

                    if(tileX2Y.TilePrefab.TileEnum == tileXY.TilePrefab.TileEnum) count++;
                }

                if(x - 2 >= 0)
                {
                    Tiles tileXM2Y = GetTile(tiles[x - 2, y - 1]);

                    if(tileXM2Y.TilePrefab.TileEnum == tileXY.TilePrefab.TileEnum) count++;
                }
            }
                    
            if(tileX1Y.TilePrefab.TileEnum == tileXY.TilePrefab.TileEnum && x + 2 < Board.Size)
            {
                Tiles tileX2Y = GetTile(tiles[x + 2, y - 1]);

                if(tileX2Y.TilePrefab.TileEnum == tileXY.TilePrefab.TileEnum) count++;
            }

            if(tileXM1Y.TilePrefab.TileEnum == tileXY.TilePrefab.TileEnum && x - 2 > Board.Size)
            {
                Tiles tileXM2Y = GetTile(tiles[x - 2, y - 1]);

                if(tileXM2Y.TilePrefab.TileEnum == tileXY.TilePrefab.TileEnum) count++;
            }
        }

        if(x == 0)
        {
            Tiles tileX1Y = GetTile(tiles[x + 1, y - 1]);
            Tiles tileX2Y = GetTile(tiles[x + 2, y - 1]);

            if(tileXY.TilePrefab.TileEnum == tileX1Y.TilePrefab.TileEnum
                && tileXY.TilePrefab.TileEnum == tileX2Y.TilePrefab.TileEnum) count = count + 2;
        }

        if(x == Board.Size - 1)
        {
            Tiles tileXM1Y = GetTile(tiles[x - 1, y - 1]);
            Tiles tileXM2Y = GetTile(tiles[x - 2, y - 1]);

            if(tileXY.TilePrefab.TileEnum == tileXM1Y.TilePrefab.TileEnum
                && tileXY.TilePrefab.TileEnum == tileXM2Y.TilePrefab.TileEnum) count = count + 2;
        }

        if(y - 3 >= 0)
        {
            Tiles tileXYM1 = GetTile(tiles[x, y - 1 - 1]);
            Tiles tileXYM2 = GetTile(tiles[x, y - 1 - 2]);

            if(tileXYM1.TilePrefab.TileEnum == tileXY.TilePrefab.TileEnum
                && tileXYM2.TilePrefab.TileEnum == tileXY.TilePrefab.TileEnum) count = count + 2;
        }

        if(count >= 3)
        {
            TileCanBeMatches tile = new(x, y, TileDirection.BOTTOM, tileXY.TilePrefab.TileEnum, count);

            matches.Add(tile);
        }
    }

    private void GetLeftMatches(int x, int y)
    {
        if(x - 1 < 0) return;

        int count = 1;

        Tiles tileXY = GetTile(tiles[x, y]);

        if(y + 1 < Board.Size && y - 1 >= 0)
        {
            Tiles tileXY1 = GetTile(tiles[x - 1, y + 1]);
            Tiles tileXYM1 = GetTile(tiles[x - 1, y - 1]);

            if(tileXY1.TilePrefab.TileEnum == tileXY.TilePrefab.TileEnum
                && tileXYM1.TilePrefab.TileEnum == tileXY.TilePrefab.TileEnum)
            {
                count = count + 2;

                if(y + 2 < Board.Size)
                {
                    Tiles tileXY2 = GetTile(tiles[x - 1, y + 2]);

                    if(tileXY2.TilePrefab.TileEnum == tileXY.TilePrefab.TileEnum) count++;
                }

                if(y - 2 >= 0)
                {
                    Tiles tileXYM2 = GetTile(tiles[x - 1, y - 2]);

                    if(tileXYM2.TilePrefab.TileEnum == tileXY.TilePrefab.TileEnum) count++;
                }
            }
                    
            if(tileXY1.TilePrefab.TileEnum == tileXY.TilePrefab.TileEnum && y + 2 < Board.Size)
            {
                Tiles tileXY2 = GetTile(tiles[x - 1, y + 2]);

                if(tileXY2.TilePrefab.TileEnum == tileXY.TilePrefab.TileEnum) count++;
            }

            if(tileXYM1.TilePrefab.TileEnum == tileXY.TilePrefab.TileEnum && y - 2 > Board.Size)
            {
                Tiles tileXYM2 = GetTile(tiles[x - 1, y - 2]);

                if(tileXYM2.TilePrefab.TileEnum == tileXY.TilePrefab.TileEnum) count++;
            }
        }

        if(y == 0)
        {
            Tiles tileXY1 = GetTile(tiles[x - 1, y + 1]);
            Tiles tileXY2 = GetTile(tiles[x - 1, y + 2]);

            if(tileXY.TilePrefab.TileEnum == tileXY1.TilePrefab.TileEnum
                && tileXY.TilePrefab.TileEnum == tileXY2.TilePrefab.TileEnum) count = count + 2;
        }

        if(y == Board.Size - 1)
        {
            Tiles tileXYM1 = GetTile(tiles[x - 1, y - 1]);
            Tiles tileXYM2 = GetTile(tiles[x - 1, y - 2]);

            if(tileXY.TilePrefab.TileEnum == tileXYM1.TilePrefab.TileEnum
                && tileXY.TilePrefab.TileEnum == tileXYM2.TilePrefab.TileEnum) count = count + 2;
        }

        if(x - 3 >= 0)
        {
            Tiles tileX1Y = GetTile(tiles[x - 1 - 1, y]);
            Tiles tileX2Y = GetTile(tiles[x - 1 - 2, y]);

            if(tileX1Y.TilePrefab.TileEnum == tileXY.TilePrefab.TileEnum
                && tileX2Y.TilePrefab.TileEnum == tileXY.TilePrefab.TileEnum) count = count + 2;
        }

        if(count >= 3)
        {
            TileCanBeMatches tile = new(x, y, TileDirection.LEFT, tileXY.TilePrefab.TileEnum, count);

            matches.Add(tile);
        }
    }

    public void CountTurn()
    {
        tiles = Board.BoardGen.Tiles;

        for(int x = 0; x < Board.Size; x++)
        {
            for(int y = 0; y < Board.Size; y++)
            {
                count = 0;

                Count(x, y);

                Debug.Log(count);

                if(count >= 4) totalCount++;
            }
        }
    }

    private void Count(int x, int y)
    {
        Tiles tile = GetTile(tiles[x, y]);

        if(!tile.TilePrefab.CanBeDestroyed) return;
        if(tile.TilePrefab.HasCount) return;

        tile.TilePrefab.HasCount = true;
        count++;

        if(y + 1 < Board.Size)
        {
            Tiles tileXY1 = GetTile(tiles[x, y + 1]);
            
            if(tileXY1.TilePrefab.CanBeDestroyed && !tileXY1.TilePrefab.HasCount
                && tile.TilePrefab.TileEnum == tileXY1.TilePrefab.TileEnum)
            {
                Count(x, y + 1);
            }
        }

        if(x + 1 < Board.Size)
        {
            Tiles tileX1Y = GetTile(tiles[x + 1, y]);
            
            if(tileX1Y.TilePrefab.CanBeDestroyed && !tileX1Y.TilePrefab.HasCount
                && tile.TilePrefab.TileEnum == tileX1Y.TilePrefab.TileEnum)
            {
                Count(x + 1, y);
            } 
        }

        if(y - 1 >= 0)
        {
            Tiles tileXYM1 = GetTile(tiles[x, y - 1]);
            
            if(tileXYM1.TilePrefab.CanBeDestroyed && !tileXYM1.TilePrefab.HasCount
                && tile.TilePrefab.TileEnum == tileXYM1.TilePrefab.TileEnum)
            {
                Count(x, y - 1);
            }
        }

        if(x - 1 >= 0)
        {
            Tiles tileXM1Y = GetTile(tiles[x - 1, y]);
            
            if(tileXM1Y.TilePrefab.CanBeDestroyed && !tileXM1Y.TilePrefab.HasCount
                && tile.TilePrefab.TileEnum == tileXM1Y.TilePrefab.TileEnum)
            {
                Count(x - 1, y);
            } 
        }
    }
}

