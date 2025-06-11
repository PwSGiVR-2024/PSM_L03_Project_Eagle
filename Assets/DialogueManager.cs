using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using TMPro;
using System;
using static GameStateManager;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance;

    [Header("UI Elements")]
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private TMP_Text dialogueText;
    [SerializeField] private TMP_Text speakerNameText;

    public Action<string> OnStartDialogueRequested;
    public Action OnDialogueEnded;
    private Queue<DialogueLine> lines;
    private bool isTyping = false;
    private float textSpeed = 0.065f;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    private void OnEnable()
    {
        OnStartDialogueRequested += StartDialogue;
    }

    private void OnDisable()
    {
        OnStartDialogueRequested -= StartDialogue;
    }

    private void Start()
    {
        dialoguePanel.SetActive(false);
    }

    public void StartDialogue(string dialogueId)
    {
        DialogueEntry dialogue = GetDialogueById(dialogueId);
        if (dialogue != null)
        {
            GameStateManager.Instance.SetState(GameState.Dialogue);
            lines = new Queue<DialogueLine>(dialogue.lines);
            dialoguePanel.SetActive(true);
            DisplayNextLine();
        }
        else
        {
            Debug.LogWarning("Nie znaleziono dialogu o ID: " + dialogueId);
        }
    }

    public void DisplayNextLine()
    {
        if (lines.Count == 0 && !isTyping)
        {
            EndDialogue();
            return;
        }

        if (!isTyping)
        {
            DialogueLine line = lines.Dequeue();
            StartCoroutine(TypeLine(line));
        }
    }

    IEnumerator TypeLine(DialogueLine line)
    {
        isTyping = true;
        dialogueText.text = "";

        // Ustaw imiê i kolor
        speakerNameText.text = line.speaker;
        if (ColorUtility.TryParseHtmlString(line.color, out Color speakerColor))
        {
            speakerNameText.color = speakerColor;
        }

        foreach (char letter in line.text)
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(textSpeed);
        }

        isTyping = false;
    }

    public void EndDialogue()
    {
        GameStateManager.Instance.SetState(GameState.Normal);
        dialoguePanel.SetActive(false);

        OnDialogueEnded?.Invoke();
        OnDialogueEnded = null;
    }

    private DialogueEntry GetDialogueById(string id)
    {
        string filePath = Path.Combine(Application.streamingAssetsPath, "Dialogues.json");
        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            DialogueData data = JsonUtility.FromJson<DialogueData>(json);
            return data.dialogues.Find(d => d.id == id);
        }
        else
        {
            Debug.LogError("Nie znaleziono pliku Dialogues.json w StreamingAssets.");
            return null;
        }
    }

    private void Update()
    {
        if (!GameStateManager.Instance.IsDialogue) return;
        if (dialoguePanel.activeSelf && Input.GetKeyDown(KeyCode.Q))
        {
            DisplayNextLine();
        }
    }
}
