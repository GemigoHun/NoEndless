using UnityEngine;

public class SwrodrainFlying : GMono
{
    [SerializeField] private Transform target;
    [SerializeField] private float speed = 2;
    [SerializeField] private bool bot;
    [SerializeField] private bool player;
    [SerializeField] private float targetRadius;

    private void Update()
    {
        Fly();
    }

    public void Fly()
    {
        targetRadius = player ? Game.Instance.Player.CapCollider.radius : bot ? Game.Instance.Bot.CapCollider.radius : 0;

        while(Vector3.Distance(transform.parent.position, target.position) < targetRadius)
        {
            transform.parent.position = Vector3.Lerp(transform.parent.position, target.position, speed);
        }
    }
}