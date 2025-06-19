using UnityEngine;

public class specialController : MonoBehaviour, IInteractable
{
    [Header("Audio")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip paperRustleClip;
    [SerializeField] private GameObject interactin2;

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
                interactin2.SetActive(true);
                gameObject.GetComponent<Collider>().enabled = false;
                Destroy(this);
                break;
        }
    }
}
