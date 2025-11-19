using UnityEngine;
using static UnityEditor.Progress;

public class player : Entity
{
    //chack weapon 
    public Transform weaponHoldPoint;
    private Weapon currentWeapon;
    public Weapon startingWeaponPrefab;

    private Item[] items;
    private Vector2 moveInput;

    
    protected override void Awake()
    {
        base.Awake();
        if (startingWeaponPrefab != null)
        {

            startingWeaponPrefab.Equip(this);
        }
    }


    public void Collect(Item item)
    {
        
    }
    public void EquipWeapon(Weapon newWeapon)
    {
        
        if (currentWeapon != null)
        {
            Destroy(currentWeapon.gameObject);
        }
        currentWeapon = newWeapon;

        if (weaponHoldPoint != null)
        {
            newWeapon.transform.SetParent(weaponHoldPoint);
            newWeapon.transform.localPosition = Vector3.zero;
            newWeapon.transform.localRotation = Quaternion.identity;
        }
    }

    private void updatePositionWeapon()
    {
        if (currentWeapon == null || weaponHoldPoint == null)
        {
            return;
        }

        
        Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        Vector3 direction = mouseWorldPosition - weaponHoldPoint.position;
        direction.z = 0; 

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        
        Quaternion targetRotation = Quaternion.Euler(0f, 0f, angle);

        weaponHoldPoint.rotation = targetRotation;

        if (direction.x < 0)
        {
            
            weaponHoldPoint.localScale = new Vector3(
                weaponHoldPoint.localScale.x,-1f, 
                weaponHoldPoint.localScale.z
            );
        }
        else
        {
           
            weaponHoldPoint.localScale = new Vector3(
                weaponHoldPoint.localScale.x,1f,  
                weaponHoldPoint.localScale.z
            );
        }
    }//mousecheck

    protected override void Update()
    {
        base.Update(); 
        //เคลื่อนที่
        moveInput.x = Input.GetAxisRaw("Horizontal");
        moveInput.y = Input.GetAxisRaw("Vertical");

        moveInput.Normalize();
        //weapon
        if (currentWeapon != null)
        {
            updatePositionWeapon();
        }

        //gun
        if (Input.GetButtonDown("Fire1")) 
        {
            BasicGun gun = currentWeapon as BasicGun;
            if (gun != null)
            {
                gun.shoot();
            }
        }
    }

    protected override void FixedUpdate() 
    {
        base.FixedUpdate();

       
        Move(moveInput); 
    }
}
