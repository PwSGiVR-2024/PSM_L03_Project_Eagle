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
    public List<string> lines;
}
