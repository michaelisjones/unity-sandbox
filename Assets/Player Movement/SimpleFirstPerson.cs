using System.Collections.Generic;
using UnityEngine;

public class SimpleFirstPerson : MonoBehaviour
{
    [Tooltip("The impulse appliedon the Y axis when jumping. /Ns")]
    public float jumpImpulse = 200F;
    [Tooltip("Minimum time between jumps. /s")]
    public float jumpTimeout = 0.2F;
    [Tooltip("Verbose debug output.")]
    public bool debug = false;

    Vector3 jumpImpulseVector;
    Rigidbody rb;

    float lastJumpTime = -1F;

    void Awake()
    {
        jumpImpulseVector = new Vector3(0F, jumpImpulse, 0F);
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

        if (Input.GetButtonDown("Jump"))
        {
            if (Physics.Raycast(transform.position, Vector3.down, 0.2F) 
                && (Time.time - lastJumpTime >= jumpTimeout || lastJumpTime < 0F))
            {
                lastJumpTime = Time.time;
                rb.AddForce(jumpImpulseVector, ForceMode.Impulse);
                if (debug) Debug.Log("applied impulse with vector: " + jumpImpulseVector);
            }
        }
    }
}
