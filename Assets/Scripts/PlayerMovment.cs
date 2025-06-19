using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovment : MonoBehaviour
{
    public float MoveSmoothTime;
    public float GravityStrenght;
    public float MoveSpeed;
    public float RunSpeed;

    private CharacterController characterController;
    private Vector3 CurrentMoveVelocity;
    private Vector3 MoveDampVelocity;
    private Vector3 CurrentForceVelocity;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameStateManager.Instance.IsNormal) return;

        Vector2 input = InputSystem.actions.FindAction("Move").ReadValue<Vector2>();
        Vector3 PlayerInput = new Vector3(input.x, 0f, input.y);

        if (PlayerInput.magnitude > 1f)
            PlayerInput.Normalize();

        Vector3 MoveVector = transform.TransformDirection(PlayerInput);
        float CurrentSpeed = Input.GetKey(KeyCode.LeftShift) ? RunSpeed : MoveSpeed;

        CurrentMoveVelocity = Vector3.SmoothDamp(
            CurrentMoveVelocity,
            MoveVector * CurrentSpeed,
            ref MoveDampVelocity,
            MoveSmoothTime
        );

        if (characterController.isGrounded)
        {
            CurrentForceVelocity.y = -5f; 
        }
        else
        {
            CurrentForceVelocity.y -= GravityStrenght * Time.deltaTime;
        }

        Vector3 totalMovement = (CurrentMoveVelocity + CurrentForceVelocity) * Time.deltaTime;
        characterController.Move(totalMovement);
    }


}
