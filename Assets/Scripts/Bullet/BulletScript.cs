using UnityEngine;

public class BulletScript : MonoBehaviour, IDamageable
{
    [SerializeField] private float speed = 5f;

    private Color currentColor;

    private SpriteRenderer spriteRenderer;

    void Start()
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();

        currentColor = spriteRenderer.color;

        //Debug.Log($"Color in Bullet: {currentColor}");
    }

    void Update()
    {
        transform.Translate(Vector2.up * Time.deltaTime * speed);
    }

    public void ChangeColor(Color color)
    {
        currentColor = color;
    }
    public void Damage()
    {
        Destroy(this.gameObject);
    }

    private void OnBecameInvisible()
    {
        Damage();
    }

}
