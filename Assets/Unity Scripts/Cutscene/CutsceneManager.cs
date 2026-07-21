//  Author: June Endstrasser

using NUnit.Framework.Constraints;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UI;

public class CutsceneManager : MonoBehaviour
{
    [HideInInspector]
    public List<CutsceneLine> currLines = new List<CutsceneLine>();

    int lineIndex;


    public GameObject speechPanel;
    public TextMeshProUGUI dialogueText;
    public TextMeshProUGUI speakerText;
    public Image portraitImg;

    GameManager gameMgr;
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {    //subscribe to static events once at the start
        CutsceneTrigger.ActorSelect += CreateSpeech;
        CutsceneTrigger.ActorInteract += Interact;
        CutsceneTrigger.ActorDeselect += DestroySpeech;

        gameMgr = GameManager.Inst();

        lineIndex = 0;

        if (speechPanel == null) print("Cannot find reference to the speech panel!");
    }

    //clean up event listeners when object is destroyed to avoid memory leaks/ghost events

    void OnDestroy() { 
        CutsceneTrigger.ActorSelect -= CreateSpeech;
        CutsceneTrigger.ActorInteract -= Interact;
        CutsceneTrigger.ActorDeselect -= DestroySpeech;
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    public void DeliverLines(List<CutsceneLine> lines)
    {
        currLines.Clear();

        foreach (CutsceneLine line in lines)
        {
            currLines.Add(line);
        }
    }

    public void Interact()
    {

        //only progress is the panel is currently active
        if (speechPanel != null && !speechPanel.activeSelf) return;
        lineIndex++;

        // If we reached past the last line, end the cutscene
        if (lineIndex >= currLines.Count)
        {
            DestroySpeech();
            return;
        }

        DisplayCurrentLine();
    }

    public void DestroySpeech()
    {
        gameMgr.UnpauseGame();
        gameMgr.ChangePlayerState(GameManager.PlayerState.Play);

        if (speechPanel != null)
        {
            speechPanel.SetActive(false);


        }// do not resubsribe to event listeners here
    }

    public void CreateSpeech()
    {
        print("cutscene start!");
        gameMgr.PauseGame();
        gameMgr.ChangePlayerState(GameManager.PlayerState.Cutscene);

        speechPanel.SetActive(true);

        //TODONE: add speaker image as well
        lineIndex = 0;
        dialogueText.text = currLines[lineIndex].dialogue;
        speakerText.text = currLines[lineIndex].speaker;
        portraitImg.sprite = currLines[lineIndex].portrait;

        if (currLines[lineIndex].portrait != null)
        {
            portraitImg.color = Color.white;
            portraitImg.sprite = currLines[lineIndex].portrait;
        }
        else
        {
            print("no portrait");
            portraitImg.color = Color.clear;
        }

        
        
    }
    private void DisplayCurrentLine() {
        dialogueText.text = currLines[lineIndex].dialogue;
        speakerText.text =  currLines[lineIndex].speaker;

        if (currLines[lineIndex].portrait != null)
        {
            portraitImg.color = Color.white;
            portraitImg.sprite = currLines[lineIndex].portrait;
        }
        else { 
            portraitImg.color= Color.clear;
        }
    }
}
