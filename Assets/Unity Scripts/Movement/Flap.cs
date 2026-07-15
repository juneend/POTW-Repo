using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Move))]
public class Flap : Physics2DObject
{	
	[Header("Jump setup")]
	// the key used to activate the push
	public KeyCode key = KeyCode.Space;

	// strength of the push
	public float jumpStrength = 10f;

    public int MaxFlaps = 2;
    private int FlapsRemaining;

    private Move Moveref;

    void Start()
    {
        FlapsRemaining = MaxFlaps;
        Moveref = transform.GetComponent<Move>();

    }

	// Read the input from the player
	void Update()
	{

        #if UNITY_STANDALONE || UNITY_EDITOR
        if (FlapsRemaining > 0
            && Input.GetKeyDown(key))
        {
            //if the player is moving
            if(Moveref.movement.sqrMagnitude >= 0.05f)
			{
				// Apply a force in the direction the player is moving
                rigidbody2D.AddForce(Moveref.movement * jumpStrength, ForceMode2D.Impulse);
                FlapsRemaining -= 1;
                print("flaps remaining: " + FlapsRemaining);
			}

        }
        #endif
    }

    private void OnCollisionEnter2D(Collision2D collisionData)
	{

        //TODO: when the player touches the "floor", flaps should be reset
		/* if(checkGround
			&& collisionData.gameObject.CompareTag(groundTag))
		{

			canFlap = true;
		} */
	}
}