using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    Vector3 moveVelocity;
    Rigidbody myRigidbody;

    private void Start()
    {
        myRigidbody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        myRigidbody.MovePosition(myRigidbody.position + moveVelocity * Time.fixedDeltaTime);
    }

    public void SetVelocity(Vector3 moveVelocity)
    {
        this.moveVelocity = moveVelocity;
    }

    public void LookAt(Vector3 lookPoint)
    {
        Vector3 correctPoint = new Vector3(lookPoint.x, transform.position.y, lookPoint.z);
        transform.LookAt(correctPoint);
    }
}
