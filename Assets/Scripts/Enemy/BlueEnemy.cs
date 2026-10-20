using UnityEngine;

public class BlueEnemy : EnemyBase
{
    protected override void Awake()
    {
        currentColor = Color.blue;
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