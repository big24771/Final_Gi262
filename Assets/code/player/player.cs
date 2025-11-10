using UnityEngine;
using static UnityEditor.Progress;

public class player : Entity
{
    private Item[] items;
    private Vector2 moveInput;

    
    protected override void Awake()
    {
        base.Awake(); 
    }

    public void Collect(Item item)
    {
        
    }

    protected override void Update()
    {
        base.Update(); 

        moveInput.x = Input.GetAxisRaw("Horizontal");
        moveInput.y = Input.GetAxisRaw("Vertical");

        moveInput.Normalize();
    }

    protected override void FixedUpdate() 
    {
        base.FixedUpdate();

       
        Move(moveInput); 
    }
}
