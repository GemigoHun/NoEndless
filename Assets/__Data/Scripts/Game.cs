using UnityEngine;

public class Game : GMono
{
    private static Game instance;

    public static Game Instance => instance;

    [SerializeField] private TileBackgroundSpawner tileBackgroundSpawner;

    public TileBackgroundSpawner TileBackgroundSpawner => tileBackgroundSpawner;

    [SerializeField] private TileSpawner tileSpawner;

    public TileSpawner TileSpawner=> tileSpawner;

    [SerializeField] private Tiles tile;

    public Tiles Tile => tile;

    [SerializeField] private Board board;

    public Board Board => board;

    [SerializeField] private Bot bot;

    public Bot Bot => bot;

    protected override void Awake()
    {
        base.Awake();
        if(instance != null) Debug.LogError("Only 1 Game instacne is allowed to exist!");

        instance = this;
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadTileBGSpawner();
        LoadTileSpawner();
        LoadTile();
        LoadBoard();
        LoadBot();
    }

    private void LoadTileBGSpawner()
    {
        if(tileBackgroundSpawner != null) return;

        tileBackgroundSpawner = FindObjectOfType<TileBackgroundSpawner>();
    }

    private void LoadTileSpawner()
    {
        if(tileSpawner != null) return;

        tileSpawner = FindObjectOfType<TileSpawner>();
    }

    private void LoadTile()
    {
        if(tile != null) return;

        tile = FindObjectOfType<Tiles>();
    }

    private void LoadBoard()
    {
        if(board != null) return;

        board = FindObjectOfType<Board>();
    }

    private void LoadBot()
    {
        if(bot != null) return;

        bot = FindObjectOfType<Bot>();
    }
}