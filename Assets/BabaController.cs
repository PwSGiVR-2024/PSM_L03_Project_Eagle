using UnityEngine;

public class BabaController : MonoBehaviour, IInteractable
{
    public string[] GetInteractionLabels()
    {
        return new string[] { "Witaj Babciu", "", "", "" };
    }

    public void Interact(int index)
    {
        switch (index)
        {
            case 0:
                DialogueManager.Instance.OnDialogueEnded = () =>
                {
                    GetComponent<Animator>().SetTrigger("Dance");
                };

                DialogueManager.Instance.OnStartDialogueRequested?.Invoke("002");
                break;
        }
    }
}
