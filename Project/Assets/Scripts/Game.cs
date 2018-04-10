using UnityEngine;
using System.Collections;
public class Game : MonoBehaviour {
	private CameraManager cam;
	private Land terrain;
	private Player player;
	private InputManager input;
	public int speed = 10;
	public delegate void ExampleDelegate (short move);


	void Start () {
		cam = new CameraManager ();
		Vector2 backScreen = cam.GetScreen ();
		terrain = new Land (backScreen);	
		//ReSizeLand (backScreen);
		player = gameObject.AddComponent<Player>();
		input = new InputManager (terrain,player);

	}
	
	// Update is called once per frame
	void Update () {
		if (Input.anyKey == true) {
			input.Control (speed);
		}		
	}

	void ReSizeLand(Vector2 backScreen){
		//DestroyObject (terrain.GetboardHolder ());
		//terrain.ReSizeLand (backScreen);
	}

	public IEnumerator PlayAnim(ExampleDelegate function,short move,float speed){
		yield return new WaitForSeconds(speed); // > mai mare =>>> slow , < mai mic =>>> fast
		function(move);
		StopCoroutine("PlayAnim");  //	Important , keep it to stop call this function.
	}

}