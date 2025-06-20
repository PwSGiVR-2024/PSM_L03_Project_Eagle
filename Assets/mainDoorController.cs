using UnityEngine;

public class mainDoorController : MonoBehaviour, IInteractable
{
    public string[] GetInteractionLabels()
    {
        return new string[] { "Spr�buj otworzy� drzwi", "", "", "" };
    }

    public void Interact(int index)
    {
        switch (index)
        {
            case 0:
                DialogueManager.Instance.OnStartDialogueRequested?.Invoke("006");
                break;
        }
    }
}
