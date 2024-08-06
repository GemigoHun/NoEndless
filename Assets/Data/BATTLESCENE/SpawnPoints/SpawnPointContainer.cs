using UnityEngine;

public class SpawnPointContainer : GMono
{
     private static SpawnPointContainer instance;

    public static SpawnPointContainer Instance => instance;

    protected override void Awake()
    {
        base.Awake();
        if(instance != null) Debug.Log("Only 1 Battle is allowed to exist!");

        instance = this;
    }

    [SerializeField] private SwordrainSpawnPoint[] swordrainSpawnPoint;

    public SwordrainSpawnPoint[] SwordrainSpawnPoints => swordrainSpawnPoint;

    protected override void LoadComponents()
    {
        LoadSpawnPoints();
    }

    private void LoadSpawnPoints()
    {
        if(swordrainSpawnPoint != null) return;

        swordrainSpawnPoint = GetComponentsInChildren<SwordrainSpawnPoint>();
    }

    public SwordrainSpawnPoint GetRandomSpawnPoint()
    {
        return swordrainSpawnPoint[Random.Range(0, swordrainSpawnPoint.Length)];
    }
}