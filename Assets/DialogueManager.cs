using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;
using static GameStateManager;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance;

    [Header("UI Elements")]
    public GameObject dialoguePanel;
    public TMP_Text dialogueText;
    public float textSpeed = 0.05f;

    // Event do uruchamiania dialogu
    public Action<string> OnStartDialogueRequested;

    private Queue<string> lines;
    private bool isTyping = false;
    private bool skipTyping = false;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        OnStartDialogueRequested += StartDialogue;
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
            lines = new Queue<string>(dialogue.lines);
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
        if (isTyping)
        {
            skipTyping = true;
            return;
        }

        if (lines.Count == 0)
        {
            EndDialogue();
            return;
        }

        string line = lines.Dequeue();
        StartCoroutine(TypeLine(line));
    }

    IEnumerator TypeLine(string line)
    {
        isTyping = true;
        dialogueText.text = "";

        foreach (char letter in line)
        {
            if (skipTyping)
            {
                dialogueText.text = line;
                break;
            }

            dialogueText.text += letter;
            yield return new WaitForSeconds(textSpeed);
        }

        isTyping = false;
        skipTyping = false;
    }

    public void EndDialogue()
    {
        GameStateManager.Instance.SetState(GameState.Normal);
        dialoguePanel.SetActive(false);
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
