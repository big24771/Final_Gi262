using Unity.VisualScripting;
using UnityEngine;

public class melee : Entity
{
    private Transform target;
    public float safeDistance = 5f;
    [SerializeField] public int damage;


    protected override void Start()
    {
        
        target = GameObject.Find("player").transform;
    }

    protected override void Update()
    {
         if (target != null)
         {
            Vector2 direction = (target.position - transform.position).normalized;
            transform.position += (Vector3)(direction * speed * Time.deltaTime);
         }//movement
         

    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Entity playerEntity = collision.gameObject.GetComponent<Entity>();

            if (playerEntity != null) 
            {
                playerEntity.takeDamage(damage);
            }
        }
    }
}
