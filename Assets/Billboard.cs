using UnityEngine;

public class Billboard : MonoBehaviour
{
    void LateUpdate()
    {
        if (Camera.main == null) return;

        transform.LookAt(Camera.main.transform);
        transform.rotation = Quaternion.Euler(0f, transform.rotation.eulerAngles.y, 0f);
        Vector3 lookDirection = Camera.main.transform.position - transform.position;
        lookDirection.y = 0;
        transform.rotation = Quaternion.LookRotation(-lookDirection);

    }
}