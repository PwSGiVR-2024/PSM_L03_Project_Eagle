using UnityEngine;

public class clockController : MonoBehaviour, IInteractable
{
    [SerializeField] GameObject clockHour;
    [SerializeField] GameObject clockMinute;
    [SerializeField] Animation door;
    public string[] GetInteractionLabels()
    {
        return new string[] { "godzina do przodu", "godzina do ty�u", "5 Min do przodu", "5 min do ty�u" };
    }

    public void Interact(int index)
    {
        switch (index)
        {
            case 0: // godzina do przodu
                SetZRotation(clockHour, GetZRotation(clockHour) - 30f);
                hourCheck();
                break;
            case 1: // godzina do ty�u
                SetZRotation(clockHour, GetZRotation(clockHour) + 30f);
                hourCheck();
                break;
            case 2: // 5 minut do przodu
                SetZRotation(clockMinute, GetZRotation(clockMinute) - 30f);
                hourCheck();
                break;
            case 3: // 5 minut do ty�u
                SetZRotation(clockMinute, GetZRotation(clockMinute) + 30f);
                hourCheck();
                break;
        }
    }

    private float GetZRotation(GameObject obj)
    {
        return obj.transform.eulerAngles.z;
    }

    private void SetZRotation(GameObject obj, float newZ)
    {
        newZ = NormalizeAngle(newZ);
        Vector3 currentEuler = obj.transform.eulerAngles;
        obj.transform.eulerAngles = new Vector3(currentEuler.x, currentEuler.y, newZ);
    }

    private float NormalizeAngle(float angle)
    {
        angle %= 360f;
        if (angle < 0f) angle += 360f;
        return angle;
    }
    private void hourCheck()
    {
        if (Mathf.Approximately(GetZRotation(clockHour), 120f) && Mathf.Approximately(GetZRotation(clockMinute), 270f))
        {
            Debug.Log("Zegar ustawiony na 8:15!");
            door.Play();
            Destroy(this);
        }
    }
}
