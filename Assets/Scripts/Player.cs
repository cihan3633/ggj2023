using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerController))]
public class Player : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5;
    PlayerController playerController;
    Camera mainCam;

    void Start()
    {
        playerController = GetComponent<PlayerController>();
        mainCam = Camera.main;
    }

    void Update()
    {
        MoveInput();
        RotationInput();
    }

    void MoveInput()
    {
        //Vector3 moveInput = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        //Vector3 moveVelocity = moveInput.normalized * moveSpeed;

        //Vector3 movePoint = new vec

        //playerController.SetVelocity(moveVelocity);
    }

    void RotationInput()
    {
        if (Input.GetMouseButton(0))
        {
            Ray ray = mainCam.ScreenPointToRay(Input.mousePosition);
            Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
            float rayDistance;

            if (groundPlane.Raycast(ray, out rayDistance))
            {
                Vector3 point = ray.GetPoint(rayDistance);

                Vector3 direction = (point - transform.position).normalized;
                Vector3 moveVelocity = direction * moveSpeed;
                playerController.SetVelocity(moveVelocity);

                playerController.LookAt(point);
                Debug.DrawLine(ray.origin, point, Color.red);
            }
        }
    }
}
