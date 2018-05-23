using UnityEngine;
using UnityEngine.EventSystems;	//	EventSystem;
//using System.Collections;
public class GameSystem : MonoBehaviour {
	public CameraManager cam;
	public Land terrain;
	public Player player;
	public InputManager input;
	public int speed = 10;
	public delegate void ExampleDelegate (short move);
	public AudioSource audiomanager;

	private Inventory inventory;
	private GameObject inventoryUI;
	private GameObject eventSystem;

	private GameObject menuSystem;

	public enum levelSceneOfGame{
		startUpMenu,
		game
	};
	private static int whatLevelSceneOfGameIs = (int)levelSceneOfGame.startUpMenu;
	private static bool changeLevelSceneOfGame = false;

	void Start () {
		
		cam = new CameraManager ();

		menuSystem = (GameObject)Instantiate(Resources.Load("Prefabs/MenuSystem",typeof(GameObject)));
		menuSystem.name = "MenuSystem";

		eventSystem = new GameObject ("EventSystem");
		eventSystem.AddComponent<EventSystem>();
		eventSystem.AddComponent<StandaloneInputModule> ();

		input = new InputManager();



		/*		
		gameObject.AddComponent<AudioSource> ();
		audiomanager = gameObject.GetComponent<AudioSource> ();
		audiomanager.clip = Resources.Load<AudioClip> ("Audio/Dynasty Wars - Shinyang Castle (round 7)");
		audiomanager.Play ();
		audiomanager.loop = true;
		audiomanager.enabled = false; // !! keep it true for testing or release . !!

		*/
		/*
		Vector2Int backScreen = (Vector2Int)cam.GetScreen ();
		terrain = new Land (backScreen);
		player = new Player ();

		Inventory inventory = new Inventory ();
		inventory.Space = 20;

		inventoryUI = (GameObject)Instantiate(Resources.Load("Prefabs/InventoryUI",typeof(GameObject)));
		inventoryUI.name = "InventoryUI";

		*/
	
	}
	 
	// Update is called once per frame
	void Update () {
		if (changeLevelSceneOfGame == true) {
			
			ChangeLevelStateOfGame ();
			changeLevelSceneOfGame = false;

		} else if (whatLevelSceneOfGameIs == (int)levelSceneOfGame.game) {
			if (Input.anyKey == true) {
			
				Vector2Int move = input.Control ();
				terrain.Move (move, speed);
				player.Move (move, speed);

			} else {
			
				player.Move (new Vector2Int (0, 0), speed);

			}
		}
	}

	void ChangeLevelStateOfGame(){
		if (whatLevelSceneOfGameIs == (int)levelSceneOfGame.game) {
			menuSystem.SetActive (false);

			Vector2Int backScreen = (Vector2Int)cam.GetScreen ();
			terrain = new Land (backScreen);
			player = new Player ();

			Inventory inventory = new Inventory ();
			inventory.Space = 20;

			inventoryUI = (GameObject)Instantiate (Resources.Load ("Prefabs/InventoryUI", typeof(GameObject)));
			inventoryUI.name = "InventoryUI";

		} else {
			
			menuSystem.SetActive (true);
			Destroy (terrain.DestroyGameObject());
			Destroy (player.DestroyGameObject());
			inventory.DestroyGameObject();
			Destroy (inventoryUI);

		}
	}

	public static void ChangeLevelOfGame(levelSceneOfGame level){
		whatLevelSceneOfGameIs = (int)level;
		changeLevelSceneOfGame = true;
	}
}