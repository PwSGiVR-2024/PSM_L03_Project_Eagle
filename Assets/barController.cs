using UnityEngine;
using UnityEngine.Audio;

public class barController : MonoBehaviour, IInteractable
{
    public string[] GetInteractionLabels()
    {
        return new string[] { "Obejrzyj prêt", "", "", "" };
    }

    public void Interact(int index)
    {
        switch (index)
        {
            case 0:
                DialogueManager.Instance.OnStartDialogueRequested?.Invoke("005");
                DataManager.Instance.weaponFinded = true;
                break;
        }
    }
}
