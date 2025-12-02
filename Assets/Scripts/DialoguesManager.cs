using System;
using UnityEngine;


[Serializable]
public class Dialogue
{

    public enum Side
    {
        Left,
        Right,
    }

    public Side side;
    public CharacterSO who;

    public string text;
    
}


[Serializable]
public class Dialogues
{
    public string dialogueName;
    public Dialogue[] dialogues;
    public int currentText;
    public void Init()
    {
        DisplayTheDialogue();
    }

    public void NextText()
    {
        currentText++;
        if (currentText >= dialogues.Length)
        {
            DialoguesManager.instance.FinishDialogue();
        }
        else DisplayTheDialogue();
    }

    public void DisplayTheDialogue()
    {
        UIManager.instance.DisplayDialogue(dialogues[currentText].text);
    }


    
}

public class DialoguesManager : MonoBehaviour
{


    public static DialoguesManager instance {get ; private set;}

    public Dialogues[] dialogues;
    public bool onDialogue;

    private int currentDialogue;

    void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (onDialogue)
        {
            if (Input.GetButtonDown("Submit"))
            {
                dialogues[currentDialogue].NextText();
            }
        }
        
    }

    public void StartDialogue(int dialogueId)
    {
        GameManager.instance.Pause();
        onDialogue = true;
        currentDialogue = dialogueId;
        dialogues[dialogueId].Init();
    }

    public void FinishDialogue()
    {
        GameManager.instance.Unpause();
        onDialogue = false;
        UIManager.instance.UndisplayDialogue();
    }
}
