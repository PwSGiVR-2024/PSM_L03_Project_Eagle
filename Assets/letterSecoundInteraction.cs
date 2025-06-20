using UnityEngine;
using UnityEngine.SceneManagement;

public class letterSecoundInteraction : MonoBehaviour, IInteractable
{
    private bool afterMonolog = false;
    public string[] GetInteractionLabels()
    {
        if (afterMonolog)
        {
            return new string[] { "Idü do willi", "", "", "" };
        }
        else
        {
            return new string[] { "Pomyúl", "", "", "" };
        }
    }

    public void Interact(int index)
    {
        if (afterMonolog)
        {
            switch (index)
            {
                case 0:

                    SceneManager.LoadScene(9);
                    break;
            }
        }
        else {
            switch (index)
            {
                case 0:
                    DialogueManager.Instance.OnStartDialogueRequested?.Invoke("004");
                    afterMonolog = true;
                    break;
            }
        }
    }
}
