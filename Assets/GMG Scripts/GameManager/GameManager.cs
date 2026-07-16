using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSettings
{
    public static bool IsDebugMode = false;
    public bool GenerationMode = false;

    public float MasterVolume = 1f;
    public float MusicVolume = 0.8f;
    public float SfxVolume = 1f;
}

public class GameManager : MonoBehaviour {

	private static GameManager gameMgr;
	public GameSettings Settings { get; private set; }

	public GameObject goMenu;
	public bool paused;

	public enum PlayerState
	{
		Pause,
		Play,
		Cutscene,
		NUM_STATES
	}

	//current game state
	[HideInInspector]
	public PlayerState currState = PlayerState.Play;
	//an event that is called whenever the game state changes
	public static event System.Action<PlayerState> PlayerStateChanged;


	public static GameManager Inst()
	{
		if (gameMgr != null) return gameMgr;

		GameManager[] gameMgrs = Object.FindObjectsOfType(typeof(GameManager)) as GameManager[];
		foreach (GameManager gameManager in gameMgrs)
		{
			gameMgr = gameManager;
		}

		if (gameMgr == null)
		{
			GameObject gObject = Instantiate(Resources.Load("GameManager")) as GameObject;
			gameMgr = gObject.GetComponent<GameManager>();
		}

		if (gameMgr == null)
		{
			GameObject gameObj = new GameObject();
			gameMgr = gameObj.AddComponent<GameManager>();
		}

		return gameMgr;
	}


	public bool isPaused = false;
	public delegate void PauseHandler(bool pause);
	public event PauseHandler onPause;
	public void PauseGame()
	{
<<<<<<< Updated upstream
		isPaused = true; 
		Time.timeScale = 0; 
		if (onPause != null) onPause(true);
	}
    public void UnpauseGame() 	
	{
		isPaused = false; 
		Time.timeScale = 1; 
		if (onPause != null) onPause(false);
	}
=======
		isPaused = true;
		paused = true;
        Time.timeScale = 0f;
        ChangePlayerState(PlayerState.Pause);
		onPause?.Invoke(true);
>>>>>>> Stashed changes

		
	}
	public void UnpauseGame()
	{
		isPaused = false;
		paused = false;
		Time.timeScale = 1;
		ChangePlayerState(PlayerState.Play);
		onPause?.Invoke(false);
	}

	//Getters
	InputManager inputMgr;
	public InputManager InputManager()
	{
		if (inputMgr != null) return inputMgr;
		inputMgr = GetComponent<InputManager>();

		if (inputMgr == null)
		{
			inputMgr = gameObject.AddComponent<InputManager>();
		}

		return inputMgr;
	}

	/*DialogueManager dialogueMgr;
	public DialogueManager DialogueManager()
	{
		if (dialogueMgr != null) return dialogueMgr;
		dialogueMgr = GetComponent<DialogueManager>();

		if (dialogueMgr == null)
		{
			dialogueMgr = gameObject.AddComponent<DialogueManager>();
		}

		return dialogueMgr;
	}*/

	// Use this for initialization
	void Start() {
		UnpauseGame();

	}

	// Update is called once per frame
	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			if (currState == PlayerState.Play)
<<<<<<< Updated upstream
			{
				PauseGame();
				ChangePlayerState(PlayerState.Pause);
			}
=======

				PauseGame();

>>>>>>> Stashed changes
			else if (currState == PlayerState.Pause)
			{
				UnpauseGame();
<<<<<<< Updated upstream
				ChangePlayerState(PlayerState.Play);
			}
			
		}	
=======
		}
		
>>>>>>> Stashed changes
	}

	public void ChangePlayerState(PlayerState newState)
	{
		currState = newState;
		print("game state changed to " + currState);
		PlayerStateChanged?.Invoke(newState);
	}

	public void Retry() {
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}

	public void Alerted() {
		PauseGame();
		goMenu.SetActive(true);

	}
}
