using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CutsceneManager : MonoBehaviour
{
    [HideInInspector]
    public List<CutsceneLine> currLines;

    int lineIndex;


    public GameObject speechPanel;
    public TextMeshProUGUI dialogueText;
    public TextMeshProUGUI speakerText;

    GameManager gameMgr;
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        CutsceneTrigger.ActorSelect += CreateSpeech;
        CutsceneTrigger.ActorInteract += Interact;
        CutsceneTrigger.ActorDeselect += DestroySpeech;

        gameMgr = GameManager.Inst();

        lineIndex = 0;

        if (speechPanel == null) print("Cannot find reference to the speech panel!");
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
        lineIndex += 1;
        dialogueText.text = currLines[lineIndex].dialogue;
        speakerText.text = currLines[lineIndex].speaker;

        //if the next line does not exist, the next interact should destroy the speechbox
        if (lineIndex == currLines.Count - 1)
        {
            CutsceneTrigger.ActorInteract -= Interact;
            CutsceneTrigger.ActorInteract += DestroySpeech;
        }
    }

    public void DestroySpeech()
    {
        gameMgr.UnpauseGame();
        gameMgr.ChangePlayerState(GameManager.PlayerState.Play);

        speechPanel.SetActive(false);

        CutsceneTrigger.ActorInteract -= DestroySpeech;
        CutsceneTrigger.ActorInteract += Interact;
    }

    public void CreateSpeech()
    {
        print("cutscene start!");
        gameMgr.PauseGame();
        gameMgr.ChangePlayerState(GameManager.PlayerState.Cutscene);

        speechPanel.SetActive(true);

        //TODO: add speaker image as well
        lineIndex = 0;
        dialogueText.text = currLines[lineIndex].dialogue;
        speakerText.text = currLines[lineIndex].speaker;
        
    }
}
