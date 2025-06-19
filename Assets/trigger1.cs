using UnityEngine;

public class trigger1 : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            DialogueManager.Instance.StartDialogue("002");
            gameObject.SetActive(false);
        }
    }
}
