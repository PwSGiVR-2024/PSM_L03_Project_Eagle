using UnityEngine;

public class TestWodki : MonoBehaviour, IInteractable
{
    public string[] GetInteractionLabels()
    {
        return new string[] { "Wypij", "Wylej", "Zjedz", "" };
    }

    public void Interact(int index)
    {
        switch (index)
        {
            case 0:
                Debug.Log("Wódka wypita");
                break;
            case 1:
                Debug.Log("Wódka wylana");
                break;
            case 2:
                Debug.Log("Wódka zjedzona");
                break;
        }
    }
}
