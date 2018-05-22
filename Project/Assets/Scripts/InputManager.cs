using UnityEngine; // GameObject and Input

public class InputManager {
	private short move = 0;

	private Land terrain;
	private Player player;

	public InputManager(Land terrain , Player player){
		this.terrain = terrain;
		this.player = player;
	}

	public void SetMove(){
		if (Input.GetKeyDown (KeyCode.RightArrow)) {
			move=6;
		}
		if (Input.GetKey (KeyCode.RightArrow)) { // true daca butonul este apasat , 
			if(move == 0)
				move = 6;
		}
		else {
			if (move == 6)
				move = 0;
		}

		if (Input.GetKeyDown (KeyCode.LeftArrow)) {	
			move=4;
		}
		if (Input.GetKey (KeyCode.LeftArrow)) { // true daca butonul este apasat , 
			if(move == 0)
				move = 4;			
		}
		else {
			if (move == 4)
				move = 0;
		}

		if (Input.GetKeyDown (KeyCode.UpArrow)) {
			move=8;
		}
		if (Input.GetKey (KeyCode.UpArrow)) { // true daca butonul este apasat , 
			if(move == 0)
				move = 8;			
		}
		else {
			if (move == 8)
				move = 0;
		}

		if (Input.GetKeyDown (KeyCode.DownArrow)) {	
			move=2;
		}
		if (Input.GetKey (KeyCode.DownArrow)) { // true daca butonul este apasat , 
			if(move == 0)
				move = 2;			
		}
		else {
			if (move == 2)
				move = 0;
		}

		//return move;
	}

	public void Control(int speed){
		SetMove ();

		player.Move (move,speed);
		terrain.Move (move,speed);

	}
}