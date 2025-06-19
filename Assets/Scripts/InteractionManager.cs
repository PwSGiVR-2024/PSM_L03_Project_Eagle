using UnityEngine;
using UnityEngine.UI;

public interface IInteractable
{
    string[] GetInteractionLabels(); // np. ["Otwórz", "Zamknij", "", ""]
    void Interact(int index); // 0-3
}

public class InteractionManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform mainCamera;
    [SerializeField] private UIInteractionDisplay uiDisplay;

    [Header("Settings")]
    [SerializeField] private float interactionRange = 3f;
    [SerializeField] private float requiredHoldTime = 1.5f;
    [SerializeField] private LayerMask interactionMask;

    private KeyCode[] keys = { KeyCode.E, KeyCode.F, KeyCode.G, KeyCode.H };
    private float[] holdTimers = new float[4];
    private string[] currentOptions = new string[4];

    private IInteractable currentInteractable;
    private GameObject currentTargetObject;

    private float lostFocusTimer = 0f;
    private const float lostFocusDuration = 0.3f;

    private int? activeInteractionIndex = null;

    void Update()
    {
        if (!GameStateManager.Instance.IsNormal) return;
        DetectInteractableInView();
        HandleFocusLoss();
        HandleUserInput();
    }

    private void DetectInteractableInView()
    {
        bool hit = Physics.Raycast(mainCamera.position, mainCamera.forward, out RaycastHit hitInfo, interactionRange, interactionMask);

        if (hit && hitInfo.collider.TryGetComponent(out IInteractable interactable))
        {
            if (interactable != currentInteractable || hitInfo.collider.gameObject != currentTargetObject)
            {
                currentInteractable = interactable;
                currentTargetObject = hitInfo.collider.gameObject;

                currentOptions = interactable.GetInteractionLabels();
                uiDisplay.ShowOptions(currentOptions, hitInfo.transform);
            }

            lostFocusTimer = 0f;
        }
    }

    private void HandleFocusLoss()
    {
        if (currentInteractable == null) return;

        bool hit = Physics.Raycast(mainCamera.position, mainCamera.forward, out RaycastHit hitInfo, interactionRange, interactionMask);
        if (!hit || hitInfo.collider.gameObject != currentTargetObject)
        {
            lostFocusTimer += Time.deltaTime;

            if (lostFocusTimer >= lostFocusDuration)
            {
                ClearCurrentInteractable();
            }
        }
        else
        {
            lostFocusTimer = 0f;
        }
    }

    private void ClearCurrentInteractable()
    {
        currentInteractable = null;
        currentTargetObject = null;
        lostFocusTimer = 0f;

        uiDisplay.HideOptions();
        CancelAllInteractions();
    }

    private void HandleUserInput()
    {
        if (currentInteractable == null) return;

        bool anyKeyHeld = false;

        for (int i = 0; i < keys.Length; i++)
        {
            if (string.IsNullOrEmpty(currentOptions[i])) continue;

            if (Input.GetKey(keys[i]))
            {
                anyKeyHeld = true;

                if (activeInteractionIndex == null)
                {
                    activeInteractionIndex = i;
                }

                if (activeInteractionIndex == i)
                {
                    holdTimers[i] += Time.deltaTime;
                    uiDisplay.UpdateHoldProgress(i, holdTimers[i] / requiredHoldTime);

                    if (holdTimers[i] >= requiredHoldTime)
                    {
                        Debug.Log($"Wykonujê interakcjê nr {i}");
                        currentInteractable.Interact(i);
                        ClearCurrentInteractable();
                        CancelAllInteractions();
                        return;
                    }
                }
            }
            else
            {
                if (activeInteractionIndex == i)
                {
                    // jeœli puszczono klawisz aktywnej interakcji
                    activeInteractionIndex = null;
                }

                holdTimers[i] = 0f;
                uiDisplay.UpdateHoldProgress(i, 0f);
            }
        }

        if (!anyKeyHeld)
        {
            activeInteractionIndex = null;
        }
    }

    private void CancelAllInteractions()
    {
        for (int i = 0; i < holdTimers.Length; i++)
        {
            holdTimers[i] = 0f;
            uiDisplay.UpdateHoldProgress(i, 0f);
        }

        activeInteractionIndex = null;
    }
}
