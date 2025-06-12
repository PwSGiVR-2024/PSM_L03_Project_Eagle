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

    [Serializable]
    public class SpeakerAudio
    {
        public string speakerName;
        public AudioClip blebleClip;
        [Range(0.1f, 3f)] public float minPitch = 0.9f;
        [Range(0.1f, 3f)] public float maxPitch = 1.1f;

        public float GetRandomPitch()
        {
            return UnityEngine.Random.Range(minPitch, maxPitch);
        }
    }

    [Header("UI Elements")]
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private TMP_Text dialogueText;
    [SerializeField] private TMP_Text speakerNameText;

    [Header("Audio")]
    [SerializeField] private AudioSource speakerAudioSource;
    [SerializeField] private List<SpeakerAudio> speakerSounds;
    [SerializeField] private AudioClip blebleClip;
    [SerializeField] private float defaultMinPitch = 0.9f;
    [SerializeField] private float defaultMaxPitch = 1.1f;

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

        speakerNameText.text = line.speaker;

        if (ColorUtility.TryParseHtmlString(line.color, out Color color))
            speakerNameText.color = color;

        SpeakerAudio speakerAudio = GetSpeakerAudio(line.speaker);
        AudioClip currentClip = speakerAudio != null ? speakerAudio.blebleClip : blebleClip;

        int charCount = 0;

        foreach (char letter in line.text)
        {
            dialogueText.text += letter;

            if (char.IsLetterOrDigit(letter))
            {
                charCount++;
                if (charCount % 2 == 0 && currentClip && speakerAudioSource)
                {
                    float pitch = speakerAudio != null ? speakerAudio.GetRandomPitch() : UnityEngine.Random.Range(defaultMinPitch, defaultMaxPitch);
                    speakerAudioSource.pitch = pitch;
                    speakerAudioSource.PlayOneShot(currentClip);
                }
            }

            yield return new WaitForSeconds(textSpeed);
        }

        isTyping = false;
    }

    private SpeakerAudio GetSpeakerAudio(string speakerName)
    {
        foreach (var speaker in speakerSounds)
        {
            if (speaker.speakerName.Equals(speakerName, StringComparison.OrdinalIgnoreCase))
            {
                return speaker;
            }
        }

        return null;
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
