using UnityEngine;
using System.Collections;
using UnityEngine.UI;	//Allows us to use UI.
using UnityEngine.SceneManagement;

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
	}

	public void Move(short direction,int speed){
		move = direction;
		if (coroutineEnabled == false) {
			//Debug.Log (speed);
			StartCoroutine ("PlayLoop",((float)speed) / 750); 
			coroutineEnabled = true;
		} 	
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
}