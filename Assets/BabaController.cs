using UnityEngine;

public class BabaController : MonoBehaviour, IInteractable
{
    [SerializeField] FlagsManager manager;
    public string[] GetInteractionLabels()
    {
        return new string[] { "Witaj Babciu", "", "", "" };
    }

    public void Interact(int index)
    {
        switch (index)
        {
            case 0:
                DialogueManager.Instance.OnStartDialogueRequested?.Invoke("001");
                manager.dialog1 = true;
                Destroy(this);
                break;
        }
    }
}
