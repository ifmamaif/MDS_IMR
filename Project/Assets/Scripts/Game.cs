using UnityEngine;
using System.Collections;
public class Game : MonoBehaviour {
	public CameraManager cam;
	public Land terrain;
	public Player player;
	public InputManager input;
	public int speed = 10;
	public delegate void ExampleDelegate (short move);
	public AudioSource audiomanager;


	void Start () {
		cam = new CameraManager ();
		Vector2 backScreen = cam.GetScreen ();
		terrain = new Land (backScreen);	
		//ReSizeLand (backScreen);
		player = gameObject.AddComponent<Player>();
		input = new InputManager (terrain,player);
		gameObject.AddComponent<AudioSource> ();
		audiomanager = gameObject.GetComponent<AudioSource> ();
		audiomanager.clip = Resources.Load<AudioClip> ("Audio/Dynasty Wars - Shinyang Castle (round 7)");
		audiomanager.Play ();
		audiomanager.loop = true;
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