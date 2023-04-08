using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] private Transform muzzle;
    [SerializeField] private Bullet bullet;
    [SerializeField] private float msBetweenShots = 100;
    [SerializeField] private float bulletSpeed = 35;

    [SerializeField] private Shell shell;
    [SerializeField] private Transform shellEjectionPoint;
    
    float nextShotTime;

    public void Shoot()
    {
        if (Time.time > nextShotTime)
        {
            nextShotTime = Time.time + msBetweenShots / 1000;
            Bullet newBullet = Instantiate(bullet, muzzle.position, muzzle.rotation);
            newBullet.SetBulletSpeed(bulletSpeed);

            Instantiate(shell, shellEjectionPoint.position, shellEjectionPoint.rotation);
        }
    }

    public void Aim(Vector3 aimPint)
    {
        transform.LookAt(aimPint);
    }
}
