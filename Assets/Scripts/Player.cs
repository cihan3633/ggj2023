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
        //MoveInput();
        //RotationInput();

        Ray ray = mainCam.ScreenPointToRay(Input.mousePosition);
        Plane groundPlane = new Plane(Vector3.up, Vector3.up * gunController.GetWeaponHeight);
        float rayDistance;
        Vector3 point = Vector3.zero;
        if (groundPlane.Raycast(ray, out rayDistance))
        {
            point = ray.GetPoint(rayDistance);
            //playerController.LookAt(point);
            crossHair.transform.position = point;
            //crosshairDetectTarget (change the color of crosshair)
            //if ((new Vector2(point.x, point.y) - new Vector2(transform.position.x, transform.position.z)).sqrMagnitude > 2.25)
            //{
            //    gunController.Aim(point);
            //}
        }

        if (Input.GetMouseButton(1))
        {
            playerController.SetMovePosition(point, moveSpeed, turnSpeed);
            //AnimatePlayer(.71f, .71f);
        }
        if (Input.GetMouseButton(0) || Input.GetKeyDown(KeyCode.Q))
        {
            // make sure the gun does not turn inside of the player
            if ((new Vector2(point.x, point.y) - new Vector2(transform.position.x, transform.position.z)).sqrMagnitude > 2.25)
            {
                gunController.Aim(point);
            }
            playerController.SetMovePosition(point, 0 , turnSpeed);
            gunController.Shoot();
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            gunController.Reload();
        }
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
