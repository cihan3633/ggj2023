using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
    [SerializeField] private Transform weaponHold;
    [SerializeField] private Gun startingGun;
   
    Gun equippedGun;

    public float GetWeaponHeight
    {
        get { return weaponHold.position.y; }
    }

    public int GetRemainingBullets
    {
        get { return equippedGun.bulletsRemainingInMagazine; }
    }

    private void Start()
    {
        EquipGun(startingGun);
    }

    public void EquipGun(Gun gunToEquip)
    {
        if (equippedGun != null)
        {
            Destroy(equippedGun.gameObject);
        }

        equippedGun = Instantiate(gunToEquip, weaponHold.position, weaponHold.rotation);
        equippedGun.transform.parent = weaponHold;
    }

    public void Shoot()
    {
        if (equippedGun != null)
        {
            equippedGun.Shoot();
        }
        
    }

    public void Aim(Vector3 aimPoint)
    {
        if (equippedGun != null)
        {
            equippedGun.Aim(aimPoint);
        }
    }

    public void Reload()
    {
        if (equippedGun != null)
        {
            equippedGun.Reload();
        }
    }

    public void ThrowGrenade()
    {
        if (equippedGun != null)
        {
            equippedGun.UseGrenade();
        }
    }
}
