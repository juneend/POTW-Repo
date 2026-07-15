using System;
using System.Collections.Generic;
using UnityEngine;

public class CutsceneManager : MonoBehaviour
{

    public List<CutsceneLine> currLines;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        CutsceneTrigger.ActorInteract += Interact;
        CutsceneTrigger.ActorDeselect += DestroySpeech;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void DeliverLines(List<CutsceneLine> lines)
    {
        currLines.Clear();

        foreach (CutsceneLine line in lines)
        {
            currLines.Add(line);
        }
    }

    public void Interact()
    {
        
    }

    public void DestroySpeech()
    {
        
    }
}
