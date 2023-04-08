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
    Vector3 recoilSmoothDampVelocity;

    void LateUpdate()
    {
        // Back to original position of the gun from recoil
        transform.localPosition = Vector3.SmoothDamp(transform.localPosition, Vector3.zero, ref recoilSmoothDampVelocity, .1f);
    }

    // implement reloading
    public void Shoot()
    {
        if (Time.time > nextShotTime)
        {
            nextShotTime = Time.time + msBetweenShots / 1000;
            Bullet newBullet = Instantiate(bullet, muzzle.position, muzzle.rotation);
            newBullet.SetBulletSpeed(bulletSpeed);

            Instantiate(shell, shellEjectionPoint.position, shellEjectionPoint.rotation);
        }
        // Gun recoil random value
        transform.localPosition -= Vector3.forward * .0005f;
    }

    public void Aim(Vector3 aimPint)
    {
        transform.LookAt(aimPint);
    }
}
