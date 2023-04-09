using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] private Transform muzzle;
    [SerializeField] private Bullet bullet;

    [Header("Gun Options")]
    [SerializeField] private float msBetweenShots = 100;
    [SerializeField] private float bulletSpeed = 35;

    [SerializeField] private int bulletsPerMag;
    [SerializeField] private float reloadTime = .33f;

    [Header("Recoil")]
    [SerializeField] private Vector2 gunRecoilMinMax = new Vector2(.0025f, .005f);
    [SerializeField] private float recoilSpeed = .1f;

    [Header("Effects")]
    [SerializeField] private Shell shell;
    [SerializeField] private Transform shellEjectionPoint;

    float nextShotTime;
    Vector3 recoilSmoothDampVelocity;
    int bulletsRemainingInMag;
    bool reloading;

    private void Start()
    {
        bulletsRemainingInMag = bulletsPerMag;
    }


    void LateUpdate()
    {
        // aim method works on update thats why its override of these animations. So these animations are should be on LateUpdate.
        // Back to original position of the gun from recoil
        transform.localPosition = Vector3.SmoothDamp(transform.localPosition, Vector3.zero, ref recoilSmoothDampVelocity, recoilSpeed);

        if (!reloading && bulletsRemainingInMag == 0)
        {
            Reload();
        }
    }

    // implement reloading
    public void Shoot()
    {
        if (!reloading && bulletsRemainingInMag > 0 && Time.time > nextShotTime)
        {
            nextShotTime = Time.time + msBetweenShots / 1000;
            Bullet newBullet = Instantiate(bullet, muzzle.position, muzzle.rotation);
            newBullet.SetBulletSpeed(bulletSpeed);
            bulletsRemainingInMag--;

            Instantiate(shell, shellEjectionPoint.position, shellEjectionPoint.rotation);

            // Gun recoil random value
            transform.localPosition -= Vector3.forward * Random.Range(gunRecoilMinMax.x, gunRecoilMinMax.y);
        }
    }

    public void Aim(Vector3 aimPint)
    {
        if (!reloading)
        {
            transform.LookAt(aimPint);
        }        
    }

    public void Reload()
    {
        if (!reloading && bulletsRemainingInMag != bulletsPerMag)
        {
            StartCoroutine(AnimateReload());
        }
    }

    IEnumerator AnimateReload()
    {
        reloading = true;
        yield return new WaitForSeconds(.2f);
        float reloadSpeed = 1 / reloadTime;
        float maxReloadAngle = 30;
        float percent = 0;
        Vector3 initialRotation = transform.localEulerAngles;

        while (percent <= 1)
        {
            percent += Time.deltaTime * reloadSpeed;
            float interpolation = 4 * (-percent * percent + percent);
            float reloadAngle = Mathf.Lerp(0, maxReloadAngle, interpolation);
            transform.localEulerAngles = initialRotation + Vector3.right * -reloadAngle;
            yield return null;
        }

        reloading = false;
        bulletsRemainingInMag = bulletsPerMag;
    }
}
