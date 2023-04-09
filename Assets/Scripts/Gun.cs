using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] private Transform muzzle;
    [SerializeField] private Bullet bullet;
    [SerializeField] private Rigidbody grenade;

    [Header("Gun Options")]
    [SerializeField] private float msBetweenShots = 100;
    [SerializeField] private float bulletSpeed = 35;
    [SerializeField] private float msBetweenGrenade = 1000;
    [SerializeField] private float grenadeForce = 40;


    [SerializeField] private int bulletsPerMag;
    [SerializeField] private float reloadTime = .33f;

    [Header("Recoil")]
    [SerializeField] private Vector2 gunRecoilMinMax = new Vector2(.002f, .005f);
    [SerializeField] private float recoilSpeed = .1f;

    [Header("Effects")]
    [SerializeField] private Shell shell;
    [SerializeField] private Transform shellEjectionPoint;
    [SerializeField] private ParticleSystem shootingParticle;

    float nextShotTime;
    float nextGrenadeTime;
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

            shootingParticle.Play();
            // Gun recoil random value
            GunRecoil(false);
        }
    }

    public void UseGrenade()
    {
        if (!reloading && Time.time > nextGrenadeTime)
        {
            nextGrenadeTime = Time.time + msBetweenGrenade / 1000;
            Rigidbody newGrenade = Instantiate(grenade, muzzle.position, muzzle.rotation) as Rigidbody;
            newGrenade.AddForce(transform.forward * grenadeForce, ForceMode.VelocityChange);
            newGrenade.AddTorque(Random.insideUnitSphere * grenadeForce);
            GunRecoil(true);
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

    void GunRecoil(bool isGrenade)
    {
        float recoilValue = isGrenade ? .008f : Random.Range(gunRecoilMinMax.x, gunRecoilMinMax.y);
        transform.localPosition -= Vector3.forward * recoilValue;
    }

    IEnumerator AnimateReload()
    {
        reloading = true;
        yield return new WaitForSeconds(.2f);
        float reloadSpeed = 1 / reloadTime;
        float maxReloadAngle = 45;
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
