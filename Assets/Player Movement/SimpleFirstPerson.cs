using System.Collections.Generic;
using UnityEngine;

public class SimpleFirstPerson : MonoBehaviour
{
    [Tooltip("The force applied during movement. /N")]
    public float movementForce = 2000F;
    [Tooltip("The impulse appliedon the Y axis when jumping. /Ns")]
    public float jumpImpulse = 200F;
    [Tooltip("Minimum time between jumps. /s")]
    public float jumpTimeout = 0.2F;
    [Tooltip("Maximum gap to the floor to detect touching. /s")]
    public float floorDetectDistance = 0.1F;
    [Tooltip("The length of \"button down\" window. Allows pressing jump just before hitting the floor, for example.")]
    public float earlyInputAllowance = 0.1F;
    [Tooltip("Verbose debug output.")]
    public int debug = 0;

    Rigidbody rb;

    float lastJumpTime = -1F;
    float lastJumpButtonDownTime = -1F;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();

        if (!rb)
        {
            Debug.LogError("no Rigidbody component");
            enabled = false;
        }
    }

    void Update()
    {
        if (!enabled) return;


        //if (debug > 1) Debug.Log("Horizontal axis: " + Input.GetAxisRaw("Horizontal") 
        //                     + ", Vertical axis: " + Input.GetAxisRaw("Vertical"));

        Vector3 movementForceVector = new Vector3(Input.GetAxisRaw("Horizontal"), 0F, Input.GetAxisRaw("Vertical"));
        movementForceVector = movementForceVector.normalized * movementForce;
        rb.AddRelativeForce(movementForceVector);


        if (Input.GetButtonDown("Jump")) lastJumpButtonDownTime = Time.time;

        if (Time.time - lastJumpButtonDownTime < earlyInputAllowance && lastJumpButtonDownTime >= 0)
        {
            if (Physics.Raycast(transform.position, Vector3.down, floorDetectDistance)
                && (Time.time - lastJumpTime >= jumpTimeout || lastJumpTime < 0F))
            {
                lastJumpTime = Time.time;
                Vector3 jumpImpulseVector = new Vector3(0F, jumpImpulse, 0F);
                rb.AddForce(jumpImpulseVector, ForceMode.Impulse);
                if (debug > 0) Debug.Log("applied impulse with vector: " + jumpImpulseVector);
            }
        }
    }
}
