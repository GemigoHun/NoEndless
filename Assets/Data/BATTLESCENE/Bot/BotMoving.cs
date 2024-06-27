using UnityEngine;

public class BotMoving : EntityMoving
{
    protected override void GetTargetRadius()
    {
        targetRadius = Game.Instance.Player.CapCollider.radius;
    }
}