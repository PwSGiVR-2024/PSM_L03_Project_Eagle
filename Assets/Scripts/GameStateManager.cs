using UnityEngine;

public class GameStateManager : MonoBehaviour
{
    public enum GameState
    {
        Normal,
        Dialogue,
        Pause,
        Inspect
    }

    public static GameStateManager Instance { get; private set; }

    public GameState CurrentState { get; private set; } = GameState.Normal;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void SetState(GameState state)
    {
        CurrentState = state;
    }

    public bool IsDialogue => CurrentState == GameState.Dialogue;
    public bool IsNormal => CurrentState == GameState.Normal;
    public bool IsInspect => CurrentState == GameState.Inspect;
    public bool IsPasue => CurrentState == GameState.Pause;
}
