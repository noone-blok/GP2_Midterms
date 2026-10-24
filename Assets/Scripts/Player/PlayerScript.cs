using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;
using static UnityEngine.GraphicsBuffer;

public class PlayerScript : MonoBehaviour, ISwitchable, IDamageable
{
    [SerializeField] private GameObject bullet;
    [SerializeField] private GameObject fireLocation;

    private float range = 9f;
    
    [SerializeField] private float rotationSpeed = 5f;

    private GameObject currentTarget;

    private float distance;
    private float nearestDistance = float.MaxValue;

    private States currentState; 
    private Color currentColor;

    private SpriteRenderer spriteRenderer;

    void Start()
    {
        currentState = States.Red;
        currentColor = Color.red;

        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.color = currentColor;

        InvokeRepeating("Fire", 1, 1);
    }

    void Update()
    {
        GetCurrentTarget();

        if (currentTarget != null)
        {
            Vector3 directionToTarget = (transform.position - currentTarget.transform.position).normalized;

            if (directionToTarget != Vector3.zero)
            {
                float angleToTargetInDegrees = Mathf.Atan2(directionToTarget.y, directionToTarget.x) * Mathf.Rad2Deg;

                Quaternion rotationToTarget = Quaternion.Euler(0, 0, angleToTargetInDegrees + 90); // ON GOD, NEVER REMOVE THE + 90

                transform.rotation = Quaternion.Slerp(transform.rotation, rotationToTarget, rotationSpeed * Time.deltaTime);
            }
        }
    }

    void Fire()
    {
        Transform location = fireLocation.transform;
        GameObject theBullet = Instantiate(bullet, location.position, transform.rotation);
        theBullet.GetComponent<SpriteRenderer>().color = currentColor;
        //Debug.Log($"Color from Instantiate: {theBullet.GetComponent<SpriteRenderer>().color}");
    }

    void OnMouseDown()
    {
        Switch();
    }

    public void Switch()
    {
        switch (currentState)
        {
            case States.Red:
                currentState = States.Green;
                currentColor = Color.green;
                break;
            case States.Green:
                currentState = States.Blue;
                currentColor = Color.blue;
                break;
            case States.Blue:
                currentState = States.Red;
                currentColor = Color.red;
                break;
            default:
                Debug.Log("Something has broken here...");
                break;
        }

        spriteRenderer.color = currentColor;

        //Debug.Log(currentState);
    }

    void GetCurrentTarget()
    {
        Collider2D[] colliderArray = Physics2D.OverlapCircleAll(transform.position, range);

        foreach (var collider in colliderArray)
        {
            if (collider.TryGetComponent<EnemyBase>(out EnemyBase enemy))
            {
                GameObject enterEnemy = collider.gameObject;

                distance = (transform.position - enterEnemy.transform.position).sqrMagnitude;

                if (distance < nearestDistance && currentTarget == null)
                {
                    distance = nearestDistance;
                    currentTarget = enterEnemy;
                }
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Damage();
    }

    public void Damage()
    {
        GameManager.instance.EndGame();
        Destroy(this.gameObject);
    }
}

enum States { Red, Green, Blue};
