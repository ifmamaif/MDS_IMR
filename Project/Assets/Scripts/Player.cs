using UnityEngine;
using System.Collections;

public class Player	: MonoBehaviour	{
	private GameObject player;	 
	private SpriteRenderer sprite;
	private int timeAnimation = 0;


	short move = 0;

	private bool coroutineEnabled = false;

	//Input.GetKeyDown este adevarat doar cand s-a apasat butonul , nu este adevarat daca butonul este in continuare apasat !!!
	//Input.GetKeyUp este adevarat cand butonul respectiv s-a ridicat / i s-a dat drumul , cu conditia ca un buton sa fie apasat in acel timp

	public void Start(){
		player = new GameObject ("Player");
		player.tag= "Player";
		player.transform.position = new Vector3 (0, 0, -0.1f);

		player.AddComponent<SpriteRenderer> ();
		sprite = player.GetComponent<SpriteRenderer>();
		sprite.sprite = Resources.Load < Sprite	> ("bd02");

		player.AddComponent<AudioListener> ();

		player.AddComponent<BoxCollider2D> ();
		player.GetComponent<BoxCollider2D> ().size = new Vector2 (1.02f, 1.24f);

		player.AddComponent<Rigidbody2D> ();
		player.GetComponent<Rigidbody2D> ().isKinematic = true;

		player.AddComponent<TestCollision> ();

	}

	public void Move(short direction,int speed){
		move = direction;
		if (coroutineEnabled == false) {
			//Debug.Log (speed);
			StartCoroutine ("PlayLoop",((float)speed) / 1000); 
			coroutineEnabled = true;
		} 

		//Ray ray = new Vector2(player.transform.position.x,player.transform.position.y);

	}

	private IEnumerator PlayLoop(float speed)
	{
		yield return new WaitForSeconds(speed); // > mai mare =>>> slow , < mai mic =>>> fast
		AnimMove ();	
		StopCoroutine("PlayLoop");  //	Important , keep it to stop call this function.
		coroutineEnabled = false;
	}

	void AnimMove(){
		if(move == 2){
			if (timeAnimation == 0) {
				sprite.sprite = Resources.Load<Sprite> ("bd01");
				timeAnimation++;
			} else if (timeAnimation == 1) {
				sprite.sprite = Resources.Load<Sprite> ("bd03");
				timeAnimation = 0;
			}
		}
		else if(move == 8){
			if (timeAnimation == 0) {
				sprite.sprite = Resources.Load<Sprite> ("bu01");
				timeAnimation++;
			} else if (timeAnimation == 1) {
				sprite.sprite = Resources.Load<Sprite> ("bu03");
				timeAnimation = 0;
			}
		}
		else if(move == 4){
			if (timeAnimation == 0) {
				sprite.sprite = Resources.Load<Sprite> ("bl01");
				timeAnimation++;
			} else if (timeAnimation == 1) {
				sprite.sprite = Resources.Load<Sprite> ("bl03");
				timeAnimation = 0;
			}
		}
		else if(move == 6){
			if (timeAnimation == 0) {
				sprite.sprite = Resources.Load<Sprite> ("br01");
				timeAnimation++;
			} else if (timeAnimation == 1) {
				sprite.sprite = Resources.Load<Sprite> ("br03");
				timeAnimation = 0;
			}
		}
	}

	void Interact(){
		//We create a ray
		//Ray ray = camera.ScreenPointToRay(Input.mousePosition);
		//Ray ray = player.transform.position;
		Vector3 fwd = player.transform.TransformDirection(Vector3.forward);
		Vector3 sourcefrom = player.transform.position;
		RaycastHit hit;
		// if the ray hits
		if (Physics.Raycast (sourcefrom,fwd, out hit, 100)) {
			Debug.Log("We hit somethign!");
			//Interactable interactable = hit.collider.GetComponent<Interactable>;

			//if (Interactable != null) {
				// do something
			//}
		}
	}
}