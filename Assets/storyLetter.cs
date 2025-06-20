using UnityEngine;

public class storyLetter : MonoBehaviour, IInteractable
{
    [Header("Audio")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip paperRustleClip;

    public string[] GetInteractionLabels()
    {
        return new string[] { "Przeczytaj list", "", "", "" };
    }

    public void Interact(int index)
    {
        switch (index)
        {
            case 0:
                if (audioSource != null && paperRustleClip != null)
                {
                    audioSource.PlayOneShot(paperRustleClip);
                }

                InspectionManager.RequestInspection(gameObject);
                DataManager.Instance.letterReaded = true;
                break;
        }
    }
}
