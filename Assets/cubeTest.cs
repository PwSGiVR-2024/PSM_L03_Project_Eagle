using UnityEngine;

public class cubeTest : MonoBehaviour, IInteractable
{
    public string[] GetInteractionLabels()
    {
        return new string[] { "Podnie�", "", "", "" };
    }

    public void Interact(int index)
    {
        switch (index)
        {
            case 0:
                gameObject.GetComponent<TestItem>().Pickup();
                break;
        }
    }
}
