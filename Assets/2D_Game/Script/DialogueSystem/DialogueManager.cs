using DialogueSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DialogueSystem
{
    public class DialogueManager : MonoBehaviour
    {
        public List<DialogueData> dialogues = new List<DialogueData>();
        
        // Todo: ���� ��� ���
        // Todo: ��ȭ ���� Ʈ����
        // Todo: ��ȭ ���� Ʈ����
        // Todo: ��ȭ ������ ���

        public DialogueData GetDialogueFromID(MonoBehaviour target, int ID)
        {
            if(ID == 0)
            {
                Debug.Log("- Dialogue End -");
                return null;
            }

            foreach (var dialogue in dialogues)
            {
                var id = dialogue.GetDialogueID();
                if (ID == id)
                    return dialogue;
            }

            Debug.LogError($"Not Exist Dialogue ID: {ID}");
            return null;
        }
    }
}
