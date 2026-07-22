//  Author: June Endstrasser

using UnityEngine;
using System.Collections;
using System;

[RequireComponent(typeof(Move))]
public class Flap : Physics2DObject
{	
	[Header("Jump setup")]
	// the key used to activate the push
	public KeyCode key = KeyCode.Space;

    //event that triggers when the player presses pace to flap
    //int: how many flaps remain
    public static event System.Action<int> onPlayerFlap;
    //event that triggers when the player lands and flaps reset
    //int: max flaps
    public static event System.Action<int> onPlayerLand;

	// strength of the push
	public float flapStrength = 10f;

    public int MaxFlaps = 2;
    private int FlapsRemaining;

    private bool isGrounded = true;
    private bool descending = false;

    //how much higher the player gets every flap
    public float FlapIncrease = 0.1f;
    //highest the player can get
    public float MaxHeightMult = 2f;
    //how quickly the player goes upwards
    //public float ScaleSpeed = 8f; TODO: maybe add this again?

    //scale that the player starts the level at
    private Vector3 initScale;
    //highest scale the player has yet reached during an upwards flap
    private Vector3 highestScale;
    private float targetScaleMult = 1f;
    //percentage between 0 and 1, how far the player is through lerping a flap
    private float percent = 0;


    Move Moveref;
    Animator animator;

    void Start()
    {
        FlapsRemaining = MaxFlaps;
        Moveref = transform.GetComponent<Move>();
        animator = GetComponent<Animator>();
        initScale = transform.localScale;

    }

	// Read the input from the player
	void Update()
	{
        #if UNITY_STANDALONE || UNITY_EDITOR

        //if player is in the air
        if (isGrounded == false)
        {

            //lerp(linearly interpolate) between the current scale & desired scale
            //(in hindsight i probably should have done this with 
            // some sort of state machine/ animation clips lol - June)
            transform.localScale = Vector3.Lerp(
                transform.localScale,           //start at current scale
                initScale * targetScaleMult,    //lerp towards target scale
                percent                         //percentage of the way there
            );
            //print("lerp: " + percent);


            //if lerp is still going
            if(percent < 1)
            {
                percent += Time.deltaTime; //* ScaleSpeed;
            }
            else //percent is greater than one, the player has either reached top or bottom  of flap
            {

                if (!descending)
                {
                    //reached peak
                    descending = true;
                    //start the player to move down
                    highestScale = initScale * targetScaleMult;
                    targetScaleMult = 1;
                    percent = 0;

                    //print("reached peak");
                }
                else
                {
                    //landed
                    descending = false;
                    isGrounded = true;
                    FlapsRemaining = MaxFlaps;
                    percent = 0;
                    
                    onPlayerLand?.Invoke(MaxFlaps);
                    //print("landed");
                }

            }
        }

        if (FlapsRemaining > 0
            && Input.GetKeyDown(key))
        {
            //if the player is moving
            if(Moveref.movement.sqrMagnitude >= 0.05f)
			{
				// Apply a force in the direction the player is moving
                rigidbody2D.AddForce(Moveref.movement * flapStrength, ForceMode2D.Impulse);

                FlapsRemaining -= 1;
                isGrounded = false;

                //play double flap animation
                animator.SetTrigger("Flap");

                //print("flaps remaining: " + FlapsRemaining);

                // Increase size but clamp to max
            	targetScaleMult = Mathf.Min(
                	targetScaleMult + FlapIncrease,
                	MaxHeightMult
				);

                //reset the percent count
                percent = 0;

                onPlayerFlap?.Invoke(FlapsRemaining);


			}
        }
        #endif
    }
}