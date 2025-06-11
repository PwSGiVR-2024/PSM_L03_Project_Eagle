using UnityEngine;

public class InspectionCameraController : MonoBehaviour
{
    [Header("Referencje")]
    [SerializeField] Camera inspectionCamera;
    [SerializeField] Transform inspectionContainer; // InspectionArea

    [Header("Parametry ruchu")]
    public float distance = 2.0f; // pocz¹tkowa odleg³oœæ od obiektu
    public float minDistance = 0.7f;
    public float maxDistance = 4.0f;
    public float rotationSpeed = 100f;
    public float zoomSpeed = 2.0f;

    private float yaw = 0f;
    private float pitch = 0f;

    void Start()
    {
        if (inspectionContainer == null) inspectionContainer = transform;
        ResetCamera();
    }

    void Update()
    {
        if (!inspectionCamera.gameObject.activeInHierarchy) return;
        if (!GameStateManager.Instance.IsInspect) return;
        if (Input.GetMouseButton(1))
        {
            float mouseX = Input.GetAxis("Mouse X");
            float mouseY = Input.GetAxis("Mouse Y");
            yaw += mouseX * rotationSpeed * Time.unscaledDeltaTime;
            pitch -= mouseY * rotationSpeed * Time.unscaledDeltaTime;
            pitch = Mathf.Clamp(pitch, -80f, 80f);
        }

        // Zoom kó³kiem myszy
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (Mathf.Abs(scroll) > 0.01f)
        {
            distance -= scroll * zoomSpeed;
            distance = Mathf.Clamp(distance, minDistance, maxDistance);
        }

        UpdateCameraPosition();
    }

    public void ResetCamera()
    {
        yaw = 0f;
        pitch = 0f;
        distance = Mathf.Clamp(distance, minDistance, maxDistance);
        UpdateCameraPosition();
    }

    private void UpdateCameraPosition()
    {
        if (inspectionCamera == null || inspectionContainer == null) return;

        // Sferyczne wspó³rzêdne (pitch, yaw, distance)
        Vector3 offset = Quaternion.Euler(pitch, yaw, 0) * new Vector3(0, 0, -distance);
        inspectionCamera.transform.position = inspectionContainer.position + offset;
        inspectionCamera.transform.LookAt(inspectionContainer.position, Vector3.up);
    }
}
