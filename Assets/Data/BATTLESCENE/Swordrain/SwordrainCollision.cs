using UnityEngine;

public class SwordrainCollision : GMono
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.parent.name == "Player")
        {
            Battle.Instance.DealSwordrainDamage(Game.Instance.Player, Game.Instance.Bot);
            Game.Instance.SwordrainSpawner.Despawn(transform.parent);
        }
        if(other.transform.parent.name == "Opponent")
        {
            Battle.Instance.DealSwordrainDamage(Game.Instance.Bot, Game.Instance.Player);
            Game.Instance.SwordrainSpawner.Despawn(transform.parent);
        }
    }
}