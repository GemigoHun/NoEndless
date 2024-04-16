using UnityEngine;

public class Board : GMono
{
    [SerializeField] private int size;

    public int Size => size;

    [SerializeField] private BoardGen boardGen;

    public BoardGen BoardGen => boardGen;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadBoardGen();
    }

    private void LoadBoardGen()
    {
        if(boardGen != null) return;

        boardGen = GetComponentInChildren<BoardGen>();
    }
}
