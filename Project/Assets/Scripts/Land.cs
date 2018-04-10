using UnityEngine; // GameObject
using System.Collections; // Transform.Translate
using Random = UnityEngine.Random; 		//Tells Random to use the Unity Engine random number generator. //

using System.IO; // file
//using System;
//using UnityEngine.UI;	//Allows us to use UI.
//using UnityEngine.SceneManagement;
//using System.Collections.Generic; 		//Allows us to use Lists.

public class Land{
	private GameObject boardHolder;	//	A variable to store a reference to the transform of our Board object.	// GameObject or Transform
	private GameObject[,] terrain;	//undeva / cumva matricea maxima ar fi 23170 pe 23170

	private int rows;	//	Number of rows in our game board.
	private int columns;	//	Number of columns in our game board.	

	private int textureSize = 124;	// texturi 124 x 124
	private float coordError = 0.5f; // ajustarea pozitiilor in spatiu a elementelor din matrice

	private int toMoveHorizontal=0;
	private int indiceHorizontal =0;
	//private int toMoveVertical=0;
	//private int indiceVertical = 0;
	//private IEnumerator coroutine; //coroutine = PlayLoop (x, y);StartCoroutine (coroutine);

	public Land(Vector2 backScreen)	{
		rows = (int)(backScreen.y / textureSize) +3;	// +1 pentru eventuale spatiu nefolosit , +2 pentru mutarea liniilor
		columns = (int)(backScreen.x / textureSize)+ 3;	// +1 pentru eventuale spatiu nefolosit , +2 pentru mutarea coloanelor

		terrain = new GameObject[rows, columns];
		boardHolder = new GameObject ("Terrain");		//Instantiate Board and set boardHolder to its transform.


		string path = "Assets/Resources/test.txt";
		FileStream f = new FileStream (path, FileMode.Open);
		int n=f.ReadByte ();



		for(int x = 0; x < rows; x++)		//Loop along x axis, starting from -1 (to fill corner) with floor or outerwall edge tiles.
		{			
			for(int y = 0; y < columns; y++)		//Loop along y axis, starting from -1 to place floor or outerwall tiles.
			{	
				terrain[x,y] = new GameObject("[ "+x+" , "+y+" ]");
				terrain [x,y].AddComponent<SpriteRenderer> ();
				SpriteRenderer sprite = terrain[x,y].GetComponent<SpriteRenderer>();

				//int i = Random.Range (0, 3);
				int i = f.ReadByte();



				sprite.sprite = Resources.Load <Sprite> (i.ToString());	// as Sprite;
				terrain[x,y].transform.position = new Vector3((y - (float)columns/2 +coordError) * textureSize/100 , (x - (float)rows/2 +coordError) * textureSize /100);
				terrain [x,y].transform.SetParent (boardHolder.transform);		//Set the parent of our newly instantiated object instance to boardHolder, this is just organizational to avoid cluttering hierarchy.
			}
		}


		f.Close ();
	}

	public void ReSizeLand(Vector2 backScreen)	{
		rows = (int)(backScreen.y / textureSize) +3;	// +1 pentru eventuale spatiu nefolosit , +2 pentru mutarea liniilor
		columns = (int)(backScreen.x / textureSize)+ 3;	// +1 pentru eventuale spatiu nefolosit , +2 pentru mutarea coloanelor

		terrain = new GameObject[rows, columns];
		boardHolder = new GameObject ("Terrain");		//Instantiate Board and set boardHolder to its transform.
		for(int x = 0; x < rows; x++)		//Loop along x axis, starting from -1 (to fill corner) with floor or outerwall edge tiles.
		{			
			for(int y = 0; y < columns; y++)		//Loop along y axis, starting from -1 to place floor or outerwall tiles.
			{	
				terrain[x,y] = new GameObject(x+","+y);
				terrain [x,y].AddComponent<SpriteRenderer> ();
				SpriteRenderer sprite = terrain[x,y].GetComponent<SpriteRenderer>();
				int i = Random.Range (0, 3);
				sprite.sprite = Resources.Load <Sprite> (i.ToString());	// as Sprite;
				terrain[x,y].transform.position = new Vector3((y - (float)columns/2 +coordError) * textureSize/100 , (x - (float)rows/2 +coordError) * textureSize /100);
				terrain [x,y].transform.SetParent (boardHolder.transform);		//Set the parent of our newly instantiated object instance to boardHolder, this is just organizational to avoid cluttering hierarchy.
			}
		}
	}

	public GameObject GetboardHolder(){
		return boardHolder;
	}



	public void Move(short move,int speed){		

		if (move == 2) {
			boardHolder.transform.Translate (0, ((float)textureSize / 100) / speed, 0);
		}
		if (move == 4) {
			boardHolder.transform.Translate (((float)textureSize / 100) / speed, 0, 0);
		}
		if (move == 6) {
			boardHolder.transform.Translate (-((float)textureSize / 100) / speed, 0, 0);
		}
		if (move == 8) {
			boardHolder.transform.Translate (0, -((float)textureSize / 100) / speed, 0);
		}
		
		//Debug.Log (((float)textureSize/100)/speed);

		if (move == 4) {
			//Debug.Log ("4 start");
			indiceHorizontal++;
			if (indiceHorizontal % (speed) == 0) {				
				int deMutat = columns - 1 - toMoveHorizontal;
				int inainteDe = (columns - toMoveHorizontal) % columns;
				//Debug.Log (deMutat + " " + inainteDe);
				for (int i = 0; i < rows; i++) {					
					terrain [i, deMutat].transform.position = new Vector3 (
						terrain [i, inainteDe].transform.position.x - ((float)textureSize)/100,
						terrain [i, inainteDe].transform.position.y, 
						0);
				}
				toMoveHorizontal++;
				if (toMoveHorizontal == columns) {
					toMoveHorizontal = 0;
				}
				indiceHorizontal = 0;
			}
			//Debug.Log ("4 end");
		}

		if (move == 6) {
			//Debug.Log ("6 start");
			indiceHorizontal--;
			if (indiceHorizontal % (speed) == 0) {				
				int deMutat = (columns - toMoveHorizontal) % columns;
				int inainteDe = columns - 1 - toMoveHorizontal;
				//Debug.Log (deMutat + " " + inainteDe);
				for (int i = 0; i < rows; i++) {					
					terrain [i, deMutat].transform.position = new Vector3 (
						terrain [i, inainteDe].transform.position.x + ((float)textureSize)/100,
						terrain [i, inainteDe].transform.position.y, 
						0);
				}
				toMoveHorizontal--;
				if (toMoveHorizontal == -1) {
					toMoveHorizontal = columns - 1;
				}
				indiceHorizontal = 0;
			}
			//Debug.Log ("6 end");
		}
		
	}
}