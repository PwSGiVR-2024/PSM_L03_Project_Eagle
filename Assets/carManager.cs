using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class carManager : MonoBehaviour, IInteractable
{
    public string[] GetInteractionLabels()
    {
        return new string[] { "Pojedü na cmentarz", "", "", "" };
    }

    public void Interact(int index)
    {
        switch (index)
        {
            case 0:
                SceneManager.LoadScene(6);
                break;
        }
    }
}
