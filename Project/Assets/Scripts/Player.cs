using UnityEngine;
//using System.Collections;
using UnityEditor;

public class Player	{
	private GameObject player;	 
	private SpriteRenderer sprite;
	//private int timeAnimation = 0;
	private Animator animator;

	//short move = 0;

	//private bool coroutineEnabled = false;



	public Player(){
		player = new GameObject ("Player");
		player.tag= "Player";
		player.transform.position = new Vector3 (0, 0, -0.1f);

		player.AddComponent<SpriteRenderer> ();
		sprite = player.GetComponent<SpriteRenderer>();
		//sprite.sprite = Resources.Load < Sprite	> ("Player/bd02");

		Material playerMaterial = new Material(Shader.Find ("Sprites/Default"));
		playerMaterial.name = "playerMaterial";
		sprite.material = playerMaterial;

		player.AddComponent<AudioListener> ();

		player.AddComponent<BoxCollider2D> ();
		player.GetComponent<BoxCollider2D> ().size = new Vector2 (1.02f, 1.24f);

		player.AddComponent<Rigidbody2D> ();
		player.GetComponent<Rigidbody2D> ().isKinematic = true;

		player.AddComponent<TestCollision> ();

		player.AddComponent<Animator> ();
		animator = player.GetComponent<Animator> ();
		animator.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController> ("Animations/Player");// as RuntimeAnimatorController;
		//animator.runtimeAnimatorController = (RuntimeAnimatorController)RuntimeAnimatorController.Instantiate(Resources.Load("Animations/Player.controller",typeof(RuntimeAnimatorController)));
		//animator.runtimeAnimatorController = (RuntimeAnimatorController)Resources.Load("Animations/Player.controller");
		//animator = UnityEditor.Animations.AnimatorController.Instantiate(Resources.Load("Animations/Player.controller",typeof(RuntimeAnimatorController)));
	}

	public void Move(Vector2Int move,int speed){
		//Debug.Log (move);
		int direction = 0;
		if(move.x == 1)
			direction = 6;
		else if(move.x == -1)
			direction = 4;
		else if(move.y == 1)
			direction = 8;
		else if(move.y == -1)
			direction = 2;
		else 
			direction =0;
		animator.SetInteger ("direction", direction);
	}

	/*
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
	*/
}