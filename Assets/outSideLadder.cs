using UnityEngine;
using UnityEngine.SceneManagement;

public class outSideLadder : MonoBehaviour, IInteractable
{
    private bool afterMonolog = false;
    public string[] GetInteractionLabels()
    {
        if (afterMonolog)
        {
            return new string[] { "Wejdü po drabinie", "", "", "" };
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

                    SceneManager.LoadScene(10);
                    break;
            }
        }
        else
        {
            switch (index)
            {
                case 0:
                    DialogueManager.Instance.OnStartDialogueRequested?.Invoke("007");
                    afterMonolog = true;
                    break;
            }
        }
    }
}
