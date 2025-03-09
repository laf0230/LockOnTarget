using DialogueSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueSelectionUI : MonoBehaviour
{
    private DialogueSelectionData m_selectionData;
    [SerializeField] private Text text;
    [SerializeField] private Button button;

    private void OnSelectionClicked()
    {
        GetComponentInParent<SpeechBubble>().SelectSelection(m_selectionData.NextID);
    }

    #region Getter / Setter

    public void SetData(SpeechBubble speechbubble, DialogueSelectionData selection)
    {
        m_selectionData = selection;
        text.text = selection.Content;
        if (selection.NextID == 0) // NextID == 0인 선택지를 선택 시 대화 종료
        {
            button.onClick.AddListener(() => GameManager.instance.UIManager.speechUI.ReturnToPool(speechbubble));
            PlayerController.instance.isTalking = false;
        }
        else
            button.onClick.AddListener(OnSelectionClicked);
    }

    public DialogueSelectionData GetData()
    {
        return m_selectionData;
    }

    #endregion
}
