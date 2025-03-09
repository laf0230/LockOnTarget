using DialogueSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    // 각 오브젝트마다 대응하는 접근 가능한 함수가 다르게 설정하기
    // 예) 캐릭터 - 말풍선, 플레이어 - UI

    /* 해당 형태는 사용 불가능
    class a: Mono
    {
        class b: Mono
        {

        }
        
        class c: Mono
        {

        }
    }
     */

    [SerializeField] public SpeechUI speechUI = new SpeechUI();
    [SerializeField] public PlayerUI playerUI = new PlayerUI();

    [System.Serializable]
    public class SpeechUI
    {
        // 확장성을 위해 애초에 SpeechBubble을 부모 클래스로 두고 사용하기!
        [SerializeField] public SpeechBubble normalSpeechBubblePrefab;
        [SerializeField] public DialogueSelectionUI selectionUIPrefab;
        public List<SpeechBubble> pool = new List<SpeechBubble>();

        public SpeechBubble GetFromPool()
        {
            if (pool == null && pool.Count <= 0)
            {
                foreach (var p in pool)
                {
                    if (p.isUseable)
                    {
                        p.isUseable = false;
                        return p;
                    }
                }
            }
            else
            {
                // Nothing to useable Speechbubble
                var newSBObject = Instantiate(normalSpeechBubblePrefab, GameManager.instance.UIManager.transform);
                AddPool(newSBObject);
                newSBObject.isUseable = false;
                newSBObject.SetActive(false);
                return newSBObject;
            }
            Debug.LogError("Exception: Unexpected.");
            return null;
        }

        public void ReturnToPool(SpeechBubble usedSBObject)
        {
            usedSBObject.SetActive(false);
            usedSBObject.isUseable = true;
        }

        #region Manupulate Pool

        private void AddPool(SpeechBubble bubble)
        {
            pool.Add(bubble);
        }

        private void RemovePool(SpeechBubble bubble)
        {
            pool.Remove(bubble);
        }

        #endregion
    }

    [System.Serializable]
    public class PlayerUI
    {

    }
}
