using UnityEngine;

public class Swordrain : GMono
{
    [SerializeField] private SwrodrainFlying swrodrainFlying;

    public SwrodrainFlying SwrodrainFlying => swrodrainFlying;

    [SerializeField] private SwordrainCollision swordrainCollision;

    public SwordrainCollision SwordrainCollision => swordrainCollision;

    [SerializeField] private Transform model;

    public Transform Model => model;


    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadSwordrainFlying();
        LoadSwordrainCollision();
        LoadModel();
    }

    private void LoadModel()
    {
        if(model != null) return;

        model = transform.Find("Model");
    }

    private void LoadSwordrainFlying()
    {
        if(swrodrainFlying != null) return;

        swrodrainFlying = GetComponentInChildren<SwrodrainFlying>();
    }

    private void LoadSwordrainCollision()
    {
        if(swordrainCollision != null) return;

        swordrainCollision = GetComponentInChildren<SwordrainCollision>();
    }
}