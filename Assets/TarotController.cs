using UnityEngine;

public class TarotController : MonoBehaviour, IInteractable
{

    [SerializeField] private string cardName = "G³upiec";
    public string[] GetInteractionLabels()
    {
        return new string[] { cardName, "", "", "" };
    }

    public void Interact(int index)
    {
        switch (index)
        {
            case 0:
                gameObject.GetComponent<TarrotItem>().Pickup();
                break;
        }
    }
}
