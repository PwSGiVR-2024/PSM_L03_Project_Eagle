using UnityEngine;
using UnityEngine.SceneManagement;

public class Body2Controller : MonoBehaviour, IInteractable
{
    public string[] GetInteractionLabels()
    {
        return new string[] { "Przyj¿yj siê g³owiê", "", "", "" };
    }

    public void Interact(int index)
    {
        switch (index)
        {
            case 0:
                DialogueManager.Instance.OnStartDialogueRequested?.Invoke("003");
                DataManager.Instance.bodyInspected = true;
                Destroy(this);
                break;
        }
    }
}
