using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerController))]
[RequireComponent(typeof(GunController))]
public class Player : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5;
    [SerializeField] private float turnSpeed = 180;
    [SerializeField] private Crosshair crossHair;
    Animator animator;
    GunController gunController;
    PlayerController playerController;
    Camera mainCam;

    void Start()
    {
        playerController = GetComponent<PlayerController>();
        gunController = GetComponent<GunController>();
        animator = GetComponent<Animator>();
        mainCam = Camera.main;
    }

    void Update()
    {
        MoveInput();
        RotationInput();
        //if (Input.GetMouseButton(1))
        //{
        //    Ray ray = mainCam.ScreenPointToRay(Input.mousePosition);
        //    Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
        //    float rayDistance;
        //    if (groundPlane.Raycast(ray, out rayDistance))
        //    {
        //        Vector3 point = ray.GetPoint(rayDistance);
        //        playerController.SetMovePosition(point, moveSpeed, turnSpeed);
        //    }
        //}
        //if (Input.GetMouseButtonDown(0))
        //{
        //    gunController.Shoot();
        //}
        //if (Input.GetMouseButtonUp(0))
        //{
        //    animator.SetBool("ShoutBool", false);
        //}
    }

    void MoveInput()
    {
        Vector3 movementInput = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        Vector3 velocity = (movementInput.normalized) * moveSpeed;
        //velocity = transform.TransformDirection(velocity);
        playerController.SetVelocity(velocity);
        AnimatePlayer(movementInput.x, movementInput.z);
    }

    void RotationInput()
    {
        Ray ray = mainCam.ScreenPointToRay(Input.mousePosition);
        Plane groundPlane = new Plane(Vector3.up, Vector3.up*gunController.GetWeaponHeight);
        float rayDistance;

        if (groundPlane.Raycast(ray, out rayDistance))
        {
            Vector3 point = ray.GetPoint(rayDistance);
            playerController.LookAt(point);
            //Debug.DrawLine(ray.origin, point, Color.red);
            crossHair.transform.position = point;
            //crosshairDetectTarget (change the color of crosshair)
            if ((new Vector2(point.x, point.y) - new Vector2(transform.position.x, transform.position.z)).sqrMagnitude > 2.25)
            {
                gunController.Aim(point);
            }
        }
    }

    void AnimatePlayer(float horizontal, float vertical)
    {
        bool walking = horizontal != 0 || vertical != 0;
        animator.SetBool("RunBoolAnim", walking);
    }
}
