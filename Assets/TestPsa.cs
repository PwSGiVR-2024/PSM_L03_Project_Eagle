using UnityEngine;

public class TestPsa : MonoBehaviour, IInteractable
{
    public string[] GetInteractionLabels()
    {
        return new string[] { "Pies opcja 1 ", "Pies Opcja 2", "", "" };
    }

    public void Interact(int index)
    {
        switch (index)
        {
            case 0:
                Debug.Log("Pies opcja 1");
                break;
            case 1:
                Debug.Log("Pies Opcja 2");
                break;
        }
    }
}