using UnityEngine;

public class basementController : MonoBehaviour, IInteractable
{
    [SerializeField] private Hotbar playerHotbar;
    [SerializeField] private string requiredItemID = "Cutters"; 
    public string[] GetInteractionLabels()
    {
        return new string[] { "Spróbuj otworzyæ", "", "", "" };
    }

    public void Interact(int index)
    {
        if (index == 0)
        {
            Item selectedItem = playerHotbar.GetSelectedItem();
            if (selectedItem != null && selectedItem.id == requiredItemID)
            {
                GetComponent<Animation>().Play();
                Destroy(this);
            }
            else
            {
                DialogueManager.Instance.OnStartDialogueRequested?.Invoke("020");
            }
        }
    }
}
