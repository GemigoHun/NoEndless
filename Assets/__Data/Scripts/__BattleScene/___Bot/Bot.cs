using UnityEngine;

public class Bot : GMono
{
    [SerializeField] private BotPick botPick;

    public BotPick BotPick => botPick;

    [SerializeField] private BotMoveTile botMoveTile;

    public BotMoveTile BotMoveTile => botMoveTile;

    [SerializeField] private BotDestroyAndFill botDestroyAndFill;

    public BotDestroyAndFill BotDestroyAndFill => botDestroyAndFill;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadBotPick();
        LoadBotMoveTile();
        LoadBotDestroyAndFill();
    }

    private void LoadBotPick()
    {
        if(botPick != null) return;

        botPick = GetComponentInChildren<BotPick>();
    }

    private void LoadBotMoveTile()
    {
        if(botMoveTile != null) return;

        botMoveTile = GetComponentInChildren<BotMoveTile>();
    }

    private void LoadBotDestroyAndFill()
    {
        if(botDestroyAndFill != null) return;

        botDestroyAndFill = GetComponentInChildren<BotDestroyAndFill>();
    }
}