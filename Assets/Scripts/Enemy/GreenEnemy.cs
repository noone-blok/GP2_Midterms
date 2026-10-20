using UnityEngine;

public class GreenEnemy : EnemyBase
{
    protected override void Awake()
    {
        currentColor = Color.green;
        base.Awake();
    }

    protected override void Update()
    {
        Move();
    }

    protected override void Move()
    {
        base.Move();
    }
}