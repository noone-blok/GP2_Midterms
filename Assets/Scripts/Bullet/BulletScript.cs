using UnityEngine;

public class BulletScript : MonoBehaviour
{
    [SerializeField] private float speed = 5f;

    private Color currentColor;

    private SpriteRenderer spriteRenderer;

    void Start()
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();

        currentColor = spriteRenderer.color;

        //Debug.Log($"Color in Bullet: {currentColor}");

        // The color in the PlayerScript isn't transferring here? 

        // I need to get the color from the sprite renderer
    }

    void Update()
    {
        transform.Translate(Vector2.up * Time.deltaTime * speed);
    }

    public void ChangeColor(Color color)
    {
        currentColor = color;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        EnemyBase enemy = collision.gameObject.GetComponent<EnemyBase>();

        enemy.Damage(this.currentColor);

        Destroy(this.gameObject);
    }
}
