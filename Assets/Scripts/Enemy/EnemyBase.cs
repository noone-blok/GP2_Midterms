using TMPro;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public abstract class EnemyBase : MonoBehaviour, IDamageable
{
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
        
        if(targetObj != null)
        {
            target = targetObj.transform;
        }

        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.color = currentColor;
    }

    protected abstract void Update();

    protected virtual void Move()
    {
        if (target != null && fractionOfJourney < 1f)
        {
            fractionOfJourney += Time.deltaTime / timeToReachTarget;

            transform.position = Vector3.Lerp(startPosition, target.position, fractionOfJourney);
        }
    }

    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<BulletScript>() != null) // Meaning it has a BulletScript component...
        {
            BulletScript bullet = collision.gameObject.GetComponent<BulletScript>();

            Color bulletColor = bullet.GetComponent<SpriteRenderer>().color;

            if (bulletColor == currentColor) // if bullet's currentCOlor is equal
            {
                Debug.Log("Hit");
                this.Damage();        
            }
            else
            {
                Debug.Log("Fluke");
            }

            bullet.Damage();
        }
    }

    public virtual void Damage() // if a colored enemy is hit by a bullet of the same color...
    {
        Destroy(this.gameObject);
    }
}
