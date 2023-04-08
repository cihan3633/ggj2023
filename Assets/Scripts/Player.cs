using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerController))]
[RequireComponent(typeof(GunController))]
public class Player : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5;
    [SerializeField] private float turnSpeed = 180;
    GunController gunController;
    PlayerController playerController;
    Camera mainCam;

    void Start()
    {
        playerController = GetComponent<PlayerController>();
        gunController = GetComponent<GunController>();
        mainCam = Camera.main;
    }

    void Update()
    {
        if (Input.GetMouseButton(1))
        {
            Ray ray = mainCam.ScreenPointToRay(Input.mousePosition);
            Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
            float rayDistance;
            if (groundPlane.Raycast(ray, out rayDistance))
            {
                Vector3 point = ray.GetPoint(rayDistance);
                playerController.SetMovePosition(point, moveSpeed, turnSpeed);
            }
        }
        if (Input.GetMouseButtonDown(0))
        {
            gunController.Shoot();
        }
        //if (Input.GetMouseButtonUp(0))
        //{
        //    animator.SetBool("ShoutBool", false);
        //}
    }
}
