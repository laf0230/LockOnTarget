using DialogueSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpeechBubble : MonoBehaviour
{
    [Header("# 디버깅")]
    [SerializeField] private List<DialogueSelectionUI> selectionUIList = new List<DialogueSelectionUI>();
    public bool isUseable = false;
    [SerializeField] private DialogueData data;
    private MonoBehaviour playerController { get; set; }

    [Header("# 데이터 셋")]
    public Transform targetTransform;
    public Transform selectionContainer;
    public Text nameText;
    public Text contentText;
    [SerializeField] private Button dialogueButton;

    private void Update()
    {
        if(targetTransform != null)
            transform.position = targetTransform.position;
    }

    #region Getter / Setter

    public void SetData(MonoBehaviour target, DialogueData data)
    {
        this.playerController = target;
        this.data = data;
        SetName(data.Name);
        SetContent(data.Content);
        if(data.dialogueSelections.Count <= 0)
        {
            dialogueButton.onClick.RemoveAllListeners();
            dialogueButton.onClick.AddListener(() => SelectSelection(data.NextID));
        }
        else
        {
            SetSelection(data.dialogueSelections);
        }
    }

    public DialogueData GetData()
    {
        return data;
    }

    public void SetName(string name)
    {
        nameText.text = name;
    }

    public void SetContent(string text)
    {
        this.contentText.text = text;
    }

    public void SetSelection(List<DialogueSelectionData> selections)
    {
        if (selections.Count <= 0)
            return;

        // Selection UI Create on Dialogue UI
        foreach (var selection in selections)
        {
            var selectionUIObject = Instantiate(GameManager.instance.UIManager.speechUI.selectionUIPrefab, selectionContainer);
            selectionUIObject.SetData(this, selection);
            selectionUIList.Add(selectionUIObject);
        }
    }

    #endregion

    #region UI Event

    public void SelectSelection(int ID)
    {
        selectionUIList.ForEach(s => s.gameObject.SetActive(false));
        selectionUIList.Clear();

        // Todo: Set Selection
        var nextData = GameManager.instance.DialogueManager.GetDialogueFromID(playerController, ID);
        if(nextData == null)
        {
            PlayerController.instance.isTalking = false;
            GameManager.instance.UIManager.speechUI.ReturnToPool(this);
            return;
        }
        SetData(playerController, nextData);
    }

    #endregion

    #region Menupulation

    public void SetTarget(Transform target)
    {
        targetTransform = target;
    }

    public void SetActive(bool active)
    {
        gameObject.SetActive(active);
    }
    
    #endregion
}
