using UnityEngine;

public class OpponentInBattle : GMono
{
    private static OpponentInBattle instance;

    public static OpponentInBattle Instance => instance;

    [SerializeField] private int currentHP = 0;

    public int CurrentHP
    {
        get { return currentHP; }
        set { currentHP = value; }
    }

    [SerializeField] private int maxHP = 100;

    protected override void Awake()
    {
        base.Awake();
        if(instance != null) Debug.Log("Only 1");

        instance = this;
    }

    protected override void ResetValues()
    {
        currentHP = maxHP;
    }
}