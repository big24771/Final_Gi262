using UnityEngine;

public class rang : Entity
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

            float distance = Vector3.Distance(transform.position, target.position);
            if (distance < safeDistance)
            {
                Vector3 dirAway = (transform.position - target.position).normalized;
                transform.position += dirAway * speed * Time.deltaTime;
            }
            else if (distance > 5 && distance < 6)
            {

            }
            else if (distance > 6)
            {

                Vector2 direction = (target.position - transform.position).normalized;
                transform.position += (Vector3)(direction * speed * Time.deltaTime);


            }

        }//movement


    }
}
