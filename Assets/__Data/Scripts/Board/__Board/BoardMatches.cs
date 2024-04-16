using System.Collections.Generic;
using UnityEngine;

public class BoardMatches : BoardAb
{
    private Transform[,] tiles;

    [SerializeField] private bool HasMatches = false;

    protected override void Start()
    {
        base.Start();
        tiles = Board.BoardGen.Tiles;
    }
}