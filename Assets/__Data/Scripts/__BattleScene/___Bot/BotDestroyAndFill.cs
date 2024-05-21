using System.Collections;
using UnityEngine;

public class BotDestroyAndFill : BotAb
{
    private BoardDestroyedMatches boardDestroyedMatches;
    
    public void DestroyAndFill()
    {      
        Bot.BotMoveTile.MoveTile();

        boardDestroyedMatches = Game.Instance.Board.BoardDestroyedMatches;

        StartCoroutine(boardDestroyedMatches.DestroyAndFill());
    }

}