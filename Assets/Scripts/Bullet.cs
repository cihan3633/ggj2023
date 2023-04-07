using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    float bulletSpeed = 35;

    public void SetBulletSpeed(float newBulletSpeed)
    {
        bulletSpeed = newBulletSpeed;
    }

    void Update()
    {
        transform.Translate(Vector3.forward * bulletSpeed * Time.deltaTime);
    }
}
