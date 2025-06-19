using UnityEngine;

public class CuttersController : MonoBehaviour, IInteractable
{
    public string[] GetInteractionLabels()
    {
        return new string[] { "Podnieœ no¿yce do metalu", "", "", "" };
    }

    public void Interact(int index)
    {
        switch (index)
        {
            case 0:
                gameObject.GetComponent<CuttersItem>().Pickup();
                break;
        }
    }
}
