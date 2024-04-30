using UnityEngine;

public class Board : GMono
{
    [SerializeField] private int size = 8;

    public int Size => size;

    [SerializeField] private BoardGen boardGen;

    public BoardGen BoardGen => boardGen;

    [SerializeField] private BoardMatches boardMatches;

    public BoardMatches BoardMatches => boardMatches;

    [SerializeField] private BoardFilling boardFilling;

    public BoardFilling BoardFilling => boardFilling;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadBoardGen();
        LoadBoardMatches();
        LoadBoardFilling();
    }

    private void LoadBoardGen()
    {
        if(boardGen != null) return;

        boardGen = GetComponentInChildren<BoardGen>();
    }

    private void LoadBoardMatches()
    {
        if(boardMatches != null) return;

        boardMatches = GetComponentInChildren<BoardMatches>();
    }

    private void LoadBoardFilling()
    {
        if(boardFilling != null) return;

        boardFilling = GetComponentInChildren<BoardFilling>();
    }
}
