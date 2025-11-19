using UnityEngine;

public class swod : thisweapon
{
    public int lenght;



    public void Slash()
    { 
        
    
    }

    public override void Equip(player player)
    {
        GameObject swordInstance = Instantiate(this.gameObject);
        Weapon swordComponent = swordInstance.GetComponent<Weapon>();

        player.EquipWeapon(swordComponent);

    }

    public override void DealDamage(Entity target)
    {
        base.DealDamage(target);
    }
}
