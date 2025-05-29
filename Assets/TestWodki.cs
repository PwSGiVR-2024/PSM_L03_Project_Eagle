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
                Debug.Log("W�dka wypita");
                break;
            case 1:
                Debug.Log("W�dka wylana");
                break;
            case 2:
                Debug.Log("W�dka zjedzona");
                break;
        }
    }
}
