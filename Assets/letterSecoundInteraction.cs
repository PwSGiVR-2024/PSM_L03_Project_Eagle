using UnityEngine;
using UnityEngine.SceneManagement;

public class letterSecoundInteraction : MonoBehaviour
{
    public string[] GetInteractionLabels()
    {
        return new string[] { "Idü do willi", "", "", "" };
    }

    public void Interact(int index)
    {
        switch (index)
        {
            case 0:
                SceneManager.LoadScene(7);
                break;
        }
    }
}
