using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public interface IInteractable
{
    public string[] GetInteractionLabels(); // np. ["Otwórz", "Zamknij", "", ""]
    public void Interact(int index); // 0-3
}
public class InteractionManager : MonoBehaviour
{
    [SerializeField] private Transform mainCamera;
    [SerializeField] private float interactionRange = 3f;
    [SerializeField] private UIInteractionDisplay uiDisplay;
    [SerializeField] private LayerMask interactionMask;

    private IInteractable currentInteractable;
    private string[] currentOptions = new string[4];

    private KeyCode[] keys = { KeyCode.E, KeyCode.F, KeyCode.G, KeyCode.H };
    private float[] holdTimers = new float[4];
    private float requiredHoldTime = 3f;

    private GameObject currentTargetObject;
    private float lostFocusTimer = 0f;
    private float lostFocusDuration = 0.3f;
    private bool interactionInProgress = false;
    void Update()
    {
        bool hitSomething = Physics.Raycast(mainCamera.position, mainCamera.forward, out RaycastHit hitInfo, interactionRange, interactionMask);

        if (hitSomething && hitInfo.collider.TryGetComponent(out IInteractable interactable))
        {
            // Jeœli nowy obiekt lub inny collider
            if (interactable != currentInteractable || hitInfo.collider.gameObject != currentTargetObject)
            {
                currentInteractable = interactable;
                currentTargetObject = hitInfo.collider.gameObject;

                currentOptions = interactable.GetInteractionLabels();
                uiDisplay.ShowOptions(currentOptions, hitInfo.transform);
            }

            lostFocusTimer = 0f; // resetuj timer utraty celu
            HandleInput(interactable);
        }
        else if (currentInteractable != null)
        {
            lostFocusTimer += Time.deltaTime;

            if (lostFocusTimer >= lostFocusDuration)
            {
                currentInteractable = null;
                currentTargetObject = null;
                lostFocusTimer = 0f;

                uiDisplay.HideOptions();
                ResetHoldTimers();
            }
        }
    }


    void HandleInput(IInteractable interactable)
    {
        for (int i = 0; i < keys.Length; i++)
        {
            if (string.IsNullOrEmpty(currentOptions[i])) continue;

            if (Input.GetKey(keys[i])&& !interactionInProgress)
            {
                interactionInProgress = true;
                holdTimers[i] += Time.deltaTime;
                uiDisplay.UpdateHoldProgress(i, holdTimers[i] / requiredHoldTime);

                if (holdTimers[i] >= requiredHoldTime)
                {
                    Debug.Log("Wykonujê interakcjê nr " + i);
                    interactable.Interact(i);
                    ResetHoldTimers();
                    return;
                }
            }
            else
            {
                interactionInProgress =  false;
                holdTimers[i] = 0f;
                uiDisplay.UpdateHoldProgress(i, 0f);

            }
        }
    }

    void ResetHoldTimers()
    {
        for (int i = 0; i < holdTimers.Length; i++)
        {
            holdTimers[i] = 0f;
            uiDisplay.UpdateHoldProgress(i, 0f);
        }
    }
}
