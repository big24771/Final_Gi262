using UnityEngine;

public class BasicGun : thisweapon
{
    public GameObject ammo;
    public Transform firePoint;
    private float shootFoce2;
    public override void Equip(player player)
    {
        GameObject gunInstance = Instantiate(this.gameObject);
        Weapon gunComponent = gunInstance.GetComponent<Weapon>();

        player.EquipWeapon(gunComponent);

    }
    public void shoot()
    { 
        //สร้างกระสุน
        GameObject bullet = Instantiate
            (ammo,firePoint.position,firePoint.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();

        //ความเร็วกระสุน
        rb.AddForce(firePoint.forward * shootFoce2, ForceMode2D.Impulse);

    }
    
    
}
