using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviour
{
    [SerializeField] private float explosionRadius = 7;
    [SerializeField] private float explosionDelay = 2.5f;
    [SerializeField] private LayerMask collisionMask;
    [SerializeField] ParticleSystem explosionEffect;
    float countdown;
    bool exploded = false;

    void Start()
    {
        countdown = explosionDelay;
    }

    void Update()
    {
        countdown -= Time.deltaTime;
        if (!exploded && countdown <= 0)
        {
            Explode();
        }
    }

    void Explode()
    {
        exploded = true;
        print("Explosing...");
        // Effects...
        //explosionEffect.Play();
        //Collider[] nearbyObjects = Physics.OverlapSphere(transform.position, explosionRadius, collisionMask);
        //foreach(Collider currentObject in nearbyObjects)
        //{
        //    // add force to them and damage them
        //    print(currentObject.name);
        //}

        Destroy(gameObject);
    }
}
