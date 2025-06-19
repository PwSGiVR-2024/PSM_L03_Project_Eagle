using UnityEngine;

public class DoorController : MonoBehaviour, IInteractable
{
    public string[] GetInteractionLabels()
    {
        return new string[] { "Otwórz drzwi", "", "", "" };
    }

    public void Interact(int index)
    {
        switch (index)
        {
            case 0:
                gameObject.GetComponent<Animation>().Play();
                Destroy(this);
                break;
        }
    }
}
