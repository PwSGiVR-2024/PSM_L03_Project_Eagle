using UnityEngine;
using UnityEngine.SceneManagement;

public class jagnaController : MonoBehaviour, IInteractable
{
    private bool firstD = true;
    private bool secondD = true;
    public string[] GetInteractionLabels()
    {
        if (firstD) {
            return new string[] { "Sprawd� co z Jagn�", "", "", "" };
        }
        else if (!firstD && secondD && DataManager.Instance.weaponFinded && DataManager.Instance.weaponFinded)
        {
            return new string[] { "Zabierz skarb dla siebie", "Pom� Jagnie", "Co� mi tu nie gra", "" };
        }
        else if (!firstD && !secondD && DataManager.Instance.weaponFinded && DataManager.Instance.weaponFinded)
        {
            return new string[] { "Wydaj Jagn� milicji", "", "", "" };
        }
        else
        {
            return new string[] { "Zabierz skarb dla siebie", "Pom� Jagnie", "", "" };
        }
    }

    public void Interact(int index)
    {
        if (firstD)
        {
            switch (index)
            {
                case 0:
                    firstD = false;
                    DialogueManager.Instance.OnStartDialogueRequested?.Invoke("016");
                    break;
            }
        }
        else if (!firstD && !secondD && DataManager.Instance.weaponFinded && DataManager.Instance.weaponFinded)
        {
            switch (index)
            {
                case 0:
                    Debug.Log("true ending");
                    SceneManager.LoadScene(4);
                    break;
            }
        }
        else if (!firstD && DataManager.Instance.weaponFinded && DataManager.Instance.weaponFinded)
        {
            switch (index)
            {
                case 0:
                    SceneManager.LoadScene(3);
                    break;
                case 1:
                    SceneManager.LoadScene(1);
                    break;
                case 2:
                    secondD = false;
                    DialogueManager.Instance.OnStartDialogueRequested?.Invoke("018");
                    break;
            }
        }
        else
        {
            switch (index)
            {
                case 0:
                    SceneManager.LoadScene(3);
                    break;
                case 1:
                    SceneManager.LoadScene(1);
                    break;
            }
        }
    }
}
