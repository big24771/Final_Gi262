using UnityEngine;

public class ammo : MonoBehaviour
{
    public float time = 2f;
    public int damage;
    void Start()
    {
        
        Destroy(gameObject, time);
    }

   
    void OnCollisionEnter2D(Collision2D collision)
    {
        Entity targetEntity = collision.gameObject.GetComponent<Entity>();
        Destroy(this.gameObject);
        targetEntity.takeDamage(damage);
    }
}
