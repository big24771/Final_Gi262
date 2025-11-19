using UnityEngine;

public class Entity : MonoBehaviour
{
    public string Name; 
    public float speed; 
    protected Rigidbody2D rb;
    [SerializeField] protected int healt;
    protected SpriteRenderer Flip;


    protected virtual void Start() { }
    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        if (rb == null)
        {
            rb = gameObject.AddComponent<Rigidbody2D>();
            rb.gravityScale = 0;
            rb.freezeRotation = true;
        }

        Flip = GetComponent<SpriteRenderer>(); 
        if (Flip == null)
        {
            
            Flip = GetComponentInChildren<SpriteRenderer>();
        }
        if (Flip == null)
        {
            Debug.LogWarning($" {gameObject.name} ");
        }
    }

    protected virtual void Update() { } 

    protected virtual void FixedUpdate() { } 

    public virtual void takeDamage(int damage) 
    {
        healt -= damage;
        if (healt <= 0)
        {
            die();
        }

    } 

    protected void Move(Vector2 direction)
    {
        if (rb != null)
        {
            rb.velocity = direction * speed;
            if (direction.x > 0 && Flip != null) 
            {
                Flip.flipX = false; 
            }
            else if (direction.x < 0 && Flip != null) 
            {
                Flip.flipX = true; 
            }

        }
    }

    protected void die() 
    {
        gameObject.SetActive(false);


    } 
}
