//  Author: June Endstrasser

using System.Collections.Generic;
using UnityEngine;

public class CutsceneTrigger : MonoBehaviour
{
    //TODO: implement interact functionality
    //should this cutscene activate on collision, or on interact?
    //public bool ActivateOnCollide = true;

    //a list (array) of this cutscene's dialog lines
    public List<CutsceneLine> Lines;

    [Header("Visit Settings")]
    [Tooltip("Unique name for this trigger to track visits.")]
    public string triggerID = "DefaultArea";

    [Tooltip("How many visits required before playing. Default is 1.")]
    public int requiredVisitCount = 1;
     

    //TODO: if dialogue changes so that there is a prompt to start cutscene, these events should probably
    //pass a bool stating such
    //when the player comes within range of the actor
    public static event System.Action ActorSelect;

    //when the player leaves the actor's range
    public static event System.Action ActorDeselect;

    //when the player presses the interact key while in range of the actor
    public static event System.Action ActorInteract;

    //is the player in range of this trigger?
    [HideInInspector]
    public bool isActive = false;

    private CutsceneManager cutsceneMgr;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GameObject UI = GameObject.Find("UserInterface");
        if (UI == null) Debug.LogError("Cutscene trigger could not find user interface in this scene, please add one from Assets>UserInterfaces!");
        cutsceneMgr = UI.GetComponent<CutsceneManager>();
    }

    // Update is called once per frame
    void Update()
    {
        //if the player is standing in range of the actor
        //and they interact
        if (Input.GetKeyUp(KeyCode.E) && isActive)
        {
            ActorInteract?.Invoke();
        }
    }

    /// <summary>
    /// Sent when another object enters a trigger collider attached to this
    /// object (2D physics only).
    /// </summary>
    /// <param name="other">The other Collider2D involved in this collision.</param>
    void OnTriggerEnter2D(Collider2D other)
    {
        //if the player enters the cutscene and it is NOT active
        if (other.gameObject.CompareTag("Player") && !isActive)
        {
            //increment and track visits in the game manager
            GameManager.Inst().IncrementVisitCount(triggerID);
            int currentVisits = GameManager.Inst().GetVisitCount(triggerID);

            //trigger only when visit count matched the target requirement
            if (currentVisits == requiredVisitCount)
            {
                print($"Trigger {triggerID} activated on visit {currentVisits}");
                cutsceneMgr.DeliverLines(Lines);
                ActorSelect?.Invoke();
                isActive = true;
            }

        }
    }

    /// <summary>
    /// Sent when another object leaves a trigger collider attached to
    /// this object (2D physics only).
    /// </summary>
    /// <param name="other">The other Collider2D involved in this collision.</param>
    void OnTriggerExit2D(Collider2D other)
    {
        //cutscenes should only activate once, for now
        //isActive = false;

        ActorDeselect?.Invoke();
        //destry only after it has successfully activated its dialogue
        if (isActive)
        {
            Destroy(this);
        }
    }

    
}
