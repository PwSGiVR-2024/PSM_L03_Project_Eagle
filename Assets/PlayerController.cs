using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float walkSpeed = 5f;
    private Rigidbody rb;
    InputAction moveAction;
    Vector3 moveDirection;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        moveAction = InputSystem.actions.FindAction("Move");
    }
    void Update()
    {
        moveDirection = moveAction.ReadValue<Vector3>();
    }
    private void FixedUpdate()
    {
        rb.MovePosition(transform.position + moveDirection * Time.fixedDeltaTime * walkSpeed);
    }
    private void HandleMovement()
    {
        moveDirection = moveAction.ReadValue<Vector3>();

    }
}
