using UnityEngine;

public class CutsceneTrigger : MonoBehaviour
{
    //TODO: implement interact functionality
    //should this cutscene activate on collision, or on interact?
    public bool ActivateOnCollide = true;

    //a list (array) of this cutscene's dialog lines
    public CutsceneLine[] Lines;

    //when the player comes within range of the actor
    public static event System.Action ActorSelect;

    //when the player leaves the actor's range
    public static event System.Action ActorDeselect;

    //when the player presses the interact key while in range of the actor
    public static event System.Action ActorInteract;

    //is this cutscene currently playing?
    [HideInInspector]
    public bool isActive = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
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
    void OnTriggerEnter2D(Collider2D other)
    {
        //if the player enters the cutscene and it is NOT active
        if (other.gameObject.CompareTag("Player") && isActive == false)
        {
            print("collision!");
            //call the select event 
            ActorSelect?.Invoke();
        }
    }
}
