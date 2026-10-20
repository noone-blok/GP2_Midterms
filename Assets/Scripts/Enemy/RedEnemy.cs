using UnityEngine;

public class RedEnemy : EnemyBase
{
    protected override void Awake()
    {
        currentColor = Color.red;
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
