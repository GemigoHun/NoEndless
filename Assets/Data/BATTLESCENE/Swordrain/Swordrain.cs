using UnityEngine;

public class Swordrain : GMono
{
    [SerializeField] private SwrodrainFlying swrodrainFlying;

    public SwrodrainFlying SwrodrainFlying => swrodrainFlying;


    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadSwordrainFlying();
    }

    private void LoadSwordrainFlying()
    {
        if(swrodrainFlying != null) return;

        swrodrainFlying = GetComponentInChildren<SwrodrainFlying>();
    }
}