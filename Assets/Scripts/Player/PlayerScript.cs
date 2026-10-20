using UnityEngine;
using UnityEngine.EventSystems;
using static UnityEngine.GraphicsBuffer;

public class PlayerScript : MonoBehaviour, ISwitchable
{
    // LOOK INTO MAKING THE TURRET ROTATE

    float currentTime;

    [SerializeField] private GameObject bullet;
    [SerializeField] private GameObject fireLocation;

    // PLAYER HAS A TARGET LIST!!! 
    private float range = 9f;
    private float distance;
    private float nearestDistance;
    
    [SerializeField] private float rotationSpeed = 5f;

    private GameObject[] targetList;
    private GameObject currentTarget; 

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

    }

    void Fire()
    {
        // LOOK INTO MAKING THIS CODE CLEANER

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

    /* void Rotate()
    {
        // The Player's range is nine, right? 
        int i = 0;

        Collider2D[] colliderArray = Physics2D.OverlapCircleAll(transform.position, range);

        foreach(Collider2D collider in colliderArray)
        {
            if(collider.TryGetComponent<EnemyBase>(out EnemyBase enemy))
            {
                Vector3 directionToTarget = (enemy.position - transform.position).normalized;
                if (directionToTarget != Vector3.zero)
                {
                    Quaternion targetRotation = Quaternion.LookRotation(directionToTarget);
                    transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
                    //slerp - Spherical Linear Interpolation
                }

                float dotProduct = Vector3.Dot(transform.forward, directionToTarget);

                if (dotProduct > 0.95f)
                {
                    Debug.Log("Target Locked");
                }
                else
                {
                    Debug.Log("Searching");
                }

            }
        }

        // How does Physics Overlap work...? 
    } */

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // NEEDS TO ONLY WORK IF IT HAS ENEMY BASE COMPONENY
        if (collision.TryGetComponent<EnemyBase>(out EnemyBase enemy))
        {
            GameObject enterEnemy = collision.gameObject;

            // WRITE ACTUAL ROTATION CODE 

            // Vector3 vectorToTarget = (enterEnemy.transform.position - transform.position).normalized;

            Vector3 targetPos = (enterEnemy.transform.position - transform.position).normalized;


            /* Vector3 directionToTarget = (enterEnemy.transform.position - transform.position).normalized;
            if (directionToTarget != Vector3.zero)
            {
                Quaternion targetRotation = Quaternion.LookRotation(directionToTarget);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
                //slerp - Spherical Linear Interpolation
            }

            float dotProduct = Vector3.Dot(transform.forward, directionToTarget);

            if (dotProduct > range)
            {
                Debug.Log("Target Locked");
            }
            else
            {
                Debug.Log("Searching");
            } */
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(this.gameObject);
    }
}

enum States { Red, Green, Blue};
