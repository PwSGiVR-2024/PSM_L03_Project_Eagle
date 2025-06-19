using UnityEngine;

public class ButtonController : MonoBehaviour, IInteractable
{
    private DeskController deskController;
    private void Start()
    {
        deskController = gameObject.GetComponentInParent<DeskController>();
    }
    public string[] GetInteractionLabels()
    {
        return new string[] { "Naciœnij przycisk", "", "", "" };
    }

    public void Interact(int index)
    {
        switch (index)
        {
            case 0:
                deskController.buttonCounter++;
                Destroy(this);
                break;
        }
    }
}
