using UnityEngine;

public class letterController : MonoBehaviour, IInteractable
{
    public string[] GetInteractionLabels()
    {
        return new string[] { "Przeczytaj list", "", "", "" };
    }

    public void Interact(int index)
    {
        switch (index)
        {
            case 0:
                InspectionManager.RequestInspection(gameObject);
                break;
        }
    }
}
