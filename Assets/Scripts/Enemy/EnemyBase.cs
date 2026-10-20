using TMPro;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public abstract class EnemyBase : MonoBehaviour, IDamageable
{
    //[SerializeField] protected float speed = 0.02f;

    protected GameObject targetObj;
    protected Transform target;

    protected float timeToReachTarget = 6f;

    protected Vector3 startPosition;
    protected float fractionOfJourney = 0f;


    protected Color currentColor;
    protected SpriteRenderer spriteRenderer;

    protected virtual void Awake()
    {
        startPosition = transform.position;

        targetObj = GameObject.FindGameObjectWithTag("Player");
        target = targetObj.transform;

        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.color = currentColor;
    }

    protected abstract void Update();

    protected virtual void Move()
    {
        if (target != null && fractionOfJourney < 1f)
        {
            //2. Accumulate the percentage of time passed
            fractionOfJourney += Time.deltaTime / timeToReachTarget;

            //3. Teleport to the exact percentage point between start and target
            transform.position = Vector3.Lerp(startPosition, target.position, fractionOfJourney);
        }
    }

    public virtual void Damage(Color color)
    {
       if(color == currentColor)
       {
            Debug.Log("Hit");
            Destroy(this.gameObject);
       }
       else
       {
            Debug.Log("Fluke");
       }
    }
}
