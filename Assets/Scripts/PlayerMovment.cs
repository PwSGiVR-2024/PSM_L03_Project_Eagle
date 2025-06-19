using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovment : MonoBehaviour
{
    public float MoveSmoothTime;
    public float GravityStrenght;
    public float MoveSpeed;
    public float RunSpeed;

    [Header("Footstep Audio")]
    [SerializeField] private AudioSource footstepSource;
    [SerializeField] private AudioClip footstepClip;
    [SerializeField] private float footstepDelay = 0.4f;
    [SerializeField] private float pitchMin = 0.95f;
    [SerializeField] private float pitchMax = 1.05f;

    private float footstepTimer = 0f;

    private CharacterController characterController;
    private Vector3 CurrentMoveVelocity;
    private Vector3 MoveDampVelocity;
    private Vector3 CurrentForceVelocity;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

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

            if (MoveVector.magnitude > 0.1f)
            {
                footstepTimer -= Time.deltaTime;
                if (footstepTimer <= 0f)
                {
                    footstepSource.pitch = Random.Range(pitchMin, pitchMax);
                    footstepSource.PlayOneShot(footstepClip);
                    footstepTimer = footstepDelay;
                }
            }
            else
            {
                footstepTimer = 0f;
            }
        }
        else
        {
            CurrentForceVelocity.y -= GravityStrenght * Time.deltaTime;
            footstepTimer = 0f;
        }

        Vector3 totalMovement = (CurrentMoveVelocity + CurrentForceVelocity) * Time.deltaTime;
        characterController.Move(totalMovement);
    }
}
