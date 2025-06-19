using UnityEngine;
using static GameStateManager;

public class LadderController : MonoBehaviour, IInteractable
{
    [SerializeField] private Animation playerAnimation;

    private bool isAnimating = false;

    public string[] GetInteractionLabels()
    {
        return new string[] { "Wejdü po drabinie", "", "", "" };
    }

    public void Interact(int index)
    {
        if (index == 0 && !isAnimating)
        {
            GameStateManager.Instance.SetState(GameState.Animation);
            playerAnimation.Play();
            isAnimating = true;
        }
    }

    private void Update()
    {
        if (isAnimating)
        {
            if (!playerAnimation.IsPlaying(""))
            {
                GameStateManager.Instance.SetState(GameState.Normal);
                isAnimating = false;
            }
        }
    }
}
