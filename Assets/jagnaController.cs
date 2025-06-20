using UnityEngine;
using UnityEngine.SceneManagement;

public class jagnaController : MonoBehaviour, IInteractable
{
    private bool firstD = true;
    private bool secondD = true;
    public string[] GetInteractionLabels()
    {
        if (firstD) {
            return new string[] { "SprawdŸ co z Jagn¹", "", "", "" };
        }
        else if (!firstD && secondD && DataManager.Instance.weaponFinded && DataManager.Instance.weaponFinded)
        {
            return new string[] { "Zabierz skarb dla siebie", "Pomó¿ Jagnie", "Coœ mi tu nie gra", "" };
        }
        else if (!firstD && !secondD && DataManager.Instance.weaponFinded && DataManager.Instance.weaponFinded)
        {
            return new string[] { "Wydaj Jagnê milicji", "", "", "" };
        }
        else
        {
            return new string[] { "Zabierz skarb dla siebie", "Pomó¿ Jagnie", "", "" };
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
