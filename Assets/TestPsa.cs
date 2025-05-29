using UnityEngine;

public class TestPsa : MonoBehaviour, IInteractable
{
    public string[] GetInteractionLabels()
    {
        return new string[] { "blagam","","",""};
    }

    public void Interact(int index)
    {
        switch (index)
        {
            case 0:
                Debug.Log("Pies opcja 1");
                break;
        }
    }
}