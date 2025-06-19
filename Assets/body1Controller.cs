using UnityEngine;
using UnityEngine.SceneManagement;

public class body1Controller : MonoBehaviour, IInteractable
{
    public string[] GetInteractionLabels()
    {
        return new string[] { "Obejrzyj cia³o", "", "", "" };
    }

    public void Interact(int index)
    {
        switch (index)
        {
            case 0:
                DialogueManager.Instance.OnStartDialogueRequested?.Invoke("033");
                Destroy(this);
                break;
        }
    }
}
