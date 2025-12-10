using System;
using System.Linq;
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
        UIManager.instance.DisplayDialogue(dialogues[currentText].text,dialogues[currentText].who.name);
    }


    
}

public class DialoguesManager : MonoBehaviour
{


    public static DialoguesManager instance {get ; private set;}

    public Dialogues[] dialogues;
    public static bool onDialogue;

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
        if(dialogues[0].dialogueName == "Start")
        {
            StartDialogue(0);
        }
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

    public void StartDialogue(string dialogueName)
    {
        GameManager.instance.Pause();
        onDialogue = true;

        int i = 0;

        foreach(var dialogue in dialogues)
        {
            
            if (dialogue.dialogueName == dialogueName)
            {
                currentDialogue = i;
                dialogue.Init();
            }
            i++;
        }


        // currentDialogue = dialogueId;
        // dialogues[dialogueId].Init();
    }

    public void FinishDialogue()
    {
        GameManager.instance.Unpause();
        onDialogue = false;
        UIManager.instance.UndisplayDialogue();
    }
}
