using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;
using static UnityEngine.GraphicsBuffer;

public class PlayerScript : MonoBehaviour, ISwitchable
{
    // LOOK INTO MAKING THE TURRET ROTATE
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
        // transform.Rotate(Vector3.forward, 90);

        // the axis of rotation needs to be forward

        currentState = States.Red;
        currentColor = Color.red;

        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.color = currentColor;

        InvokeRepeating("Fire", 1, 1);
    }

    void Update()
    {
        if (currentTarget != null)
        {
            Vector3 directionToTarget = (transform.position - currentTarget.transform.position).normalized;

            if (directionToTarget != Vector3.zero)
            {
                Debug.Log("Turn");

                float angleToTargetInDegrees = Mathf.Atan2(directionToTarget.y, directionToTarget.x) * Mathf.Rad2Deg;
                Quaternion rotationToTarget = Quaternion.Euler(0, 0, 90 - angleToTargetInDegrees);


                // transform.rotation = Quaternion.AngleAxis(90 - angleToTargetInDegrees, Vector3.forward);

                transform.rotation = Quaternion.Slerp(transform.rotation, rotationToTarget, rotationSpeed * Time.deltaTime);
            }
        }
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<EnemyBase>(out EnemyBase enemy))
        {
            Debug.Log("Enter");

            GameObject enterEnemy = collision.gameObject;

            currentTarget = enterEnemy;

            /*
            Vector3 directionToTarget = enterEnemy.transform.position - transform.position;

            if (directionToTarget != Vector3.zero)
            {
                Debug.Log("Turn");

                float angleToTargetInDegrees = Mathf.Atan2(directionToTarget.y, directionToTarget.x) * Mathf.Rad2Deg;
                Quaternion rotationToTarget = Quaternion.Euler(0, 0, angleToTargetInDegrees);
                transform.rotation = Quaternion.Slerp(transform.rotation, rotationToTarget, rotationSpeed * Time.deltaTime);
            } */
        }
    } 

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(this.gameObject);
    }
}

enum States { Red, Green, Blue};
