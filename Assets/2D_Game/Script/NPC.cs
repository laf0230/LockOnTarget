using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour, IInterectionable
{
    [SerializeField] private string characterName;
    private SpeechBubble speechBubble;
    private MonoBehaviour interectionTarget;

    public void Highlight(bool isHighlighted)
    {
        if (isHighlighted)
            GetComponent<SpriteRenderer>().material = GameManager.instance.M_highlighted;
        else
            GetComponent<SpriteRenderer>().material = GameManager.instance.M_normal;
    }

    public void Interection(MonoBehaviour target)
    {
        interectionTarget = target;
        speechBubble = GameManager.instance.UIManager.speechUI.GetFromPool();
        Talk();
    }

    public void Talk()
    {
        if(interectionTarget != null &&  interectionTarget.TryGetComponent<PlayerController>(out PlayerController player))
        {
            player.isTalking = true;
        }

        speechBubble.SetData(interectionTarget, GameManager.instance.DialogueManager.GetDialogueFromID(interectionTarget, 1));
        speechBubble.SetActive(true);
    }
}
