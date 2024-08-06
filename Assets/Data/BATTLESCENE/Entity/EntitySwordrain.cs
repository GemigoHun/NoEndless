using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntitySwordrain : EntityAb
{
    [SerializeField] private Transform sword;

    public Transform Sword => sword;

    private float[] waitTime = new float[] {0, 0.2f, 0.3f, 0.5f};

    public bool spawn = false;
    public bool isSpawning = false;

    protected override void Start()
    {
        StartCoroutine(SpawnSword(5));
    }

    public IEnumerator SpawnSword(int swordNums)
    {
        Debug.Log("A");
        while (swordNums > 0)
        {
            Debug.Log("B");

            Transform randomSpawnPoint = SpawnPointContainer.Instance.GetRandomSpawnPoint().transform;

            Transform newSword = Game.Instance.SwordrainSpawner.Spawn(sword, randomSpawnPoint.position, randomSpawnPoint.rotation);

            newSword.gameObject.SetActive(true);

            yield return new WaitForSeconds(waitTime[Random.Range(0, 3)]);

            swordNums--;
            Debug.Log("" + swordNums);
        }       
    }


}