using UnityEngine;

public class SimpleFirstPerson : MonoBehaviour {

    [Tooltip("Ns")]
    public float jumpImpulse = 200F;
    public bool debug = false;

    Vector3 jumpImpulseVector;
    Rigidbody rb;

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

        bool jumpKeyDown = Input.GetKeyDown("space");

        if (jumpKeyDown)
        {
            rb.AddForce(jumpImpulseVector, ForceMode.Impulse);
            if (debug) Debug.Log("applied impulse with vector: " + jumpImpulseVector);
        }
    }
}
