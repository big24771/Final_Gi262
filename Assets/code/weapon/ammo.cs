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
        player player = collision.gameObject.GetComponent<player>();

        if (player != null)
        {
          
            Debug.Log("Bullet hit Player, but no damage applied.");
            Destroy(gameObject);
            return; 
        }


        Entity target = collision.gameObject.GetComponent<Entity>();

        if (target != null)
        {          
            target.takeDamage(damage);
            
            Destroy(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
