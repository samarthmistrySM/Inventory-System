using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f; //speed of movement
    public float turnSpeed = 200f; //speed of turning

    private Rigidbody rb;
    private float moveInput;
    private float turnInput;

    void Start()
    {
        rb = GetComponent<Rigidbody>(); //cache Rigidbody
    }

    void Update()
    {
        moveInput = Input.GetAxis("Vertical");   //W/S or Up/Down keys
        turnInput = Input.GetAxis("Horizontal"); //A/D or Left/Right keys
    }

    void FixedUpdate()
    {
        Vector3 move = transform.forward * moveInput * moveSpeed * Time.fixedDeltaTime;
        rb.MovePosition(rb.position + move); //apply movement

        float turn = turnInput * turnSpeed * Time.fixedDeltaTime;
        transform.Rotate(0f, turn, 0f); //apply rotation
        rb.MoveRotation(transform.rotation);
    }
}
