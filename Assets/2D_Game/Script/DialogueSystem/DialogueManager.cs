using DialogueSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DialogueSystem
{
    public class DialogueManager : MonoBehaviour
    {
        public List<DialogueData> dialogues = new List<DialogueData>();
        
        // Todo: 다음 대사 출력
        // Todo: 대화 시작 트리거
        // Todo: 대화 종료 트리거
        // Todo: 대화 선택지 출력

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
