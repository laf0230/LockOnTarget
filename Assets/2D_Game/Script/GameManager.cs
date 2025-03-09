using DialogueSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance { get; private set; }

    [SerializeField] public UIManager UIManager { get; private set; }
    [SerializeField] public DialogueManager DialogueManager { get; private set; }

    [SerializeField] public Material M_highlighted;
    [SerializeField] public Material M_normal;

    private void Awake()
    {
        if(instance == null)
            instance = this;
        else
            Destroy(instance);
    }

    private void Start()
    {
        UIManager = FindAnyObjectByType<UIManager>();
        DialogueManager = FindAnyObjectByType<DialogueManager>();
    }
}
