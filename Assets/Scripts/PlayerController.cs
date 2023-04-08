using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{

    Rigidbody myRigidbody;
    Vector3 velocity;
    Vector3 moveVelocity;
    Vector3 heightCorrectedPoint;
    float distanceThreshold = .1f;
    bool hasTargetPoint;
    private Animator anime;

    private void Start()
    {
        myRigidbody = GetComponent<Rigidbody>();
        anime = GetComponent<Animator>();
    }

    private void Update()
    {
        AnimateWalking();
    }

    private void FixedUpdate()
    {
        //if we got a mouse point, calculate the distance and move to that point
        if (hasTargetPoint)
        {
            // calculating the squared value. Cause squareRoot is fairly expensive calculation
            float sqrDistanceToPoint = (myRigidbody.position - heightCorrectedPoint).sqrMagnitude;
            myRigidbody.MovePosition(myRigidbody.position + moveVelocity * Time.fixedDeltaTime);
            print(moveVelocity.magnitude);

            // if we are close enough to mouse point, player can stop. (Dont need to exact point)
            if (sqrDistanceToPoint < distanceThreshold)
            {
                hasTargetPoint = false;
            }
        }
        //myRigidbody.MovePosition(myRigidbody.position + velocity * Time.fixedDeltaTime);
    }

    public void ResetMovement()
    {
        SetMovePosition(transform.position, 0, 720);
    }

    public void SetMovePosition(Vector3 movePosition, float moveSpeed, float turnSpeed)
    {
        hasTargetPoint = moveSpeed > 0 ? true : false;
        
        // make sure the mouse's Y value is equal to player's Y value
        heightCorrectedPoint = new Vector3(movePosition.x, transform.position.y, movePosition.z);
        Vector3 directionToTarget = (heightCorrectedPoint - transform.position).normalized;

        StopAllCoroutines();
        StartCoroutine(TurnToFace(directionToTarget, turnSpeed));

        moveVelocity = directionToTarget * moveSpeed;
    }

    //turning face towards to spesific position(mouse position)
    IEnumerator TurnToFace(Vector3 directionToTarget, float turnSpeed)
    {
        float targetAngle = 90 - Mathf.Atan2(directionToTarget.z, directionToTarget.x) * Mathf.Rad2Deg;
        // due to incompatibility with animation
        //targetAngle += 50;

        while (Mathf.Abs(Mathf.DeltaAngle(transform.eulerAngles.y, targetAngle)) > 0.05f)
        {
            float angle = Mathf.MoveTowardsAngle(transform.eulerAngles.y, targetAngle, turnSpeed * Time.deltaTime);
            transform.eulerAngles = Vector3.up * angle;
            yield return null;
        }
    }

    public void SetVelocity(Vector3 _velocity)
    {
        velocity = _velocity;
    }

    public void LookAt(Vector3 point)
    {
        Vector3 lookPoint = new Vector3(point.x, transform.position.y, point.z);
        transform.LookAt(lookPoint);
    }

    void AnimateWalking()
    {
        anime.SetBool("RunBoolAnim", hasTargetPoint);
        print("run bool anim = " + anime.GetBool("RunBoolAnim"));
    }
}
