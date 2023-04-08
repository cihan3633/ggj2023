using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] private Transform muzzle;
    [SerializeField] private Bullet bullet;
    [SerializeField] private float msBetweenShots = 250;
    [SerializeField] private float bulletSpeed = 35;
    
    float nextShotTime;

    public void Shoot()
    {
        if (Time.time > nextShotTime)
        {
            nextShotTime = Time.time + msBetweenShots / 1000;
            Bullet newBullet = Instantiate(bullet, muzzle.position, muzzle.rotation);
            newBullet.SetBulletSpeed(bulletSpeed);
        }
    }

    public void Aim(Vector3 aimPint)
    {
        transform.LookAt(aimPint);
    }
}
