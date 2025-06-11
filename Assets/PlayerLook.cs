using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerLook : MonoBehaviour
{
    public Transform PlayerCamera;
    public Vector2 Sensitivities;

    private Vector2 Rotation;
    void Start()
    {
  
        PlayerCamera = GetComponentInChildren<Camera>().transform;

    }
    void Update()
    {
        if (!GameStateManager.Instance.IsNormal) return;
        Vector2 MouseInput = new Vector2
        {
            x = InputSystem.actions.FindAction("Look").ReadValue<Vector2>().y,
            y = InputSystem.actions.FindAction("Look").ReadValue<Vector2>().x
        };
        Rotation.y += MouseInput.y* Sensitivities.y;
        Rotation.x -= MouseInput.x* Sensitivities.x;

        Rotation.x = Mathf.Clamp(Rotation.x, -90f, 90f);

        transform.eulerAngles = new Vector3(0f, Rotation.y, 0f);
        PlayerCamera.localEulerAngles = new Vector3(Rotation.x, 0f, 0f);
    }
}
