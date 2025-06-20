using UnityEngine;
using UnityEngine.SceneManagement;

public class outSideLadder : MonoBehaviour
{
    private bool afterMonolog = false;
    public string[] GetInteractionLabels()
    {
        if (afterMonolog)
        {
            return new string[] { "Wejd� po drabinie", "", "", "" };
        }
        else
        {
            return new string[] { "Pomy�l", "", "", "" };
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
        else
        {
            switch (index)
            {
                case 0:
                    DialogueManager.Instance.OnStartDialogueRequested?.Invoke("006");
                    afterMonolog = true;
                    break;
            }
        }
    }
}
