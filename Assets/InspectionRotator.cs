using UnityEngine;

public class InspectionRotator : MonoBehaviour
{
    public float rotationSpeed = 100f;

    void Update()
    {
        if (Input.GetMouseButton(1))
        {
            float mouseX = Input.GetAxis("Mouse X");
            float mouseY = Input.GetAxis("Mouse Y");
            transform.Rotate(Vector3.up, -mouseX * rotationSpeed * Time.unscaledDeltaTime, Space.World);
            transform.Rotate(Vector3.right, mouseY * rotationSpeed * Time.unscaledDeltaTime, Space.Self);
        }
    }
}
