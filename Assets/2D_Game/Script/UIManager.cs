using DialogueSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    // �� ������Ʈ���� �����ϴ� ���� ������ �Լ��� �ٸ��� �����ϱ�
    // ��) ĳ���� - ��ǳ��, �÷��̾� - UI

    /* �ش� ���´� ��� �Ұ���
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
        // Ȯ�强�� ���� ���ʿ� SpeechBubble�� �θ� Ŭ������ �ΰ� ����ϱ�!
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
