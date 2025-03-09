using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DialogueSystem
{
    public enum DialogueTriggerType
    {
        BeginOfGame,
        Twice,
        Third
    }

    [System.Serializable]
    public class Dialogue
    {
        // 간단 대화 시스템
        [SerializeField] private List<DialogueData> datas = new List<DialogueData>();
    }

    [System.Serializable]
    public class DialogueData
    {
        public string Name;
        // { get; private set; }
        public int ID;
        // { get; private set; }
        public string Content;
        public int NextID;
        public List<DialogueSelectionData> dialogueSelections = new List<DialogueSelectionData>();
        // { get; private set; }

        public int GetDialogueID()
        {
            return ID;
        }

        public DialogueData(int ID, string name, string content, List<DialogueSelectionData> selectionDatas = null)
        {
            this.ID = ID;
            this.Name = name;
            this.Content = content;
            this.dialogueSelections = selectionDatas;
        }
    }

    [System.Serializable]
    public class DialogueSelectionData
    {
        public string Content;
        public int NextID;

        public DialogueSelectionData(string content, int nextID)
        {
            Content = content;
            NextID = nextID;
        }
    }
}
