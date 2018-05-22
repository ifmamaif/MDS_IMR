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

	void Start () {
		
		cam = new CameraManager ();
		Vector2Int backScreen = (Vector2Int)cam.GetScreen ();
		terrain = new Land (backScreen);
		player = new Player ();
		input = new InputManager();
		/*		
		gameObject.AddComponent<AudioSource> ();
		audiomanager = gameObject.GetComponent<AudioSource> ();
		audiomanager.clip = Resources.Load<AudioClip> ("Audio/Dynasty Wars - Shinyang Castle (round 7)");
		audiomanager.Play ();
		audiomanager.loop = true;
		audiomanager.enabled = false; // !! keep it true for testing or release . !!

		*/
		Inventory inventory = new Inventory ();
		inventory.Space = 20;

		inventoryUI = (GameObject)Instantiate(Resources.Load("Prefabs/InventoryUI",typeof(GameObject)));
		inventoryUI.name = "InventoryUI";

		eventSystem = new GameObject ("EventSystem");
		eventSystem.AddComponent<EventSystem>();
		eventSystem.AddComponent<StandaloneInputModule> ();
	
	}
	 
	// Update is called once per frame
	void Update () {
		if (Input.anyKey == true) {
			//input.Control (ref speed);
			Vector2Int move = input.Control();
			//Debug.Log (move);
			terrain.Move (move, speed);
			player.Move (move, speed);
		}
		else
			player.Move (new Vector2Int(0,0), speed);
	}

}