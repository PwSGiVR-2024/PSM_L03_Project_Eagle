using UnityEngine;

public class socketController : MonoBehaviour, IInteractable
{
    public int socketID = 0;
    public int socketNumber = 0;
    [SerializeField] private Sprite[] digitSprites;
    private SpriteRenderer sprite;
    public string[] GetInteractionLabels()
    {
        return new string[] { "Dodaj", "Odejmij", "", "" };
    }

    public void Interact(int index)
    {
        switch (index)
        {
            case 0:
                socketNumber = (socketNumber + 1) % 10;
                UpdateSprite();
                break;
            case 1:
                socketNumber = (socketNumber + 9) % 10;
                UpdateSprite();
                break;
        }
    }

    private void Start()
    {
        sprite = gameObject.GetComponent<SpriteRenderer>();
        UpdateSprite();
    }

    private void UpdateSprite()
    {
        if (digitSprites != null && digitSprites.Length == 10 && sprite != null)
        {
            sprite.sprite = digitSprites[socketNumber];
        }
    }
}
