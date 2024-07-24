using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntitySwordrain : EntityAb
{
    [SerializeField] private Transform sword;

    public Transform Sword => sword;

    [SerializeField] private int swordNums;

    public int SwordNums => swordNums;

    public void SpawnSword()
    {
        swordNums = Battle.Instance.TileCounter[TileEnum.SWORD];

        

    }
}