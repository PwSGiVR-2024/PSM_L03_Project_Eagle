using System.Collections.Generic;

[System.Serializable]
public class DialogueData
{
    public List<DialogueEntry> dialogues;
}

[System.Serializable]
public class DialogueEntry
{
    public string id;
    public List<DialogueLine> lines;
}

[System.Serializable]
public class DialogueLine
{
    public string speaker;
    public string color;
    public string text;
}