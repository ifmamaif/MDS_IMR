//using System.Collections;
using System.Collections.Generic;	//	List
using UnityEngine;	// core , Random
using UnityEngine.SceneManagement; //SceneManager
using UnityEngine.UI; // Text
using System.IO;	//	File, FileStream , GetFiles;
using System;
public class MenuSystem : MonoBehaviour {

	public GameObject mainMenu;
	public GameObject newGame;
	public GameObject savedGames;
	public GameObject exitMessage;
	public GameObject newCharacter;
	public GameObject alertMessage;
	public GameObject settings;

	public GameObject nameCharacterInputField;

	private string[] listSavedGames;
	//public GameObject contentSavedGames;
	//public GameObject verticalScrollBarSavedGames;
	private bool isVisibleSavedGames = false;
	private Vector2 scrollViewVector = Vector2.zero;
	private Vector2 guiDimension = new Vector2(410,340);
	private float longlist = 0;

	public GameObject soundButton;
	private bool isSoundMuted = false;

	// Use this for initialization
	void Start () {
		mainMenu.SetActive (true);
		exitMessage.SetActive (false);
		newGame.SetActive (false);
		newCharacter.SetActive (false);
		alertMessage.SetActive (false);
		isVisibleSavedGames = false;
	}

	/*
	// Update is called once per frame
	void Update () {
		
	}
	*/


	void OnGUI(){
		if (isVisibleSavedGames == true) {
			// Begin the ScrollView
			scrollViewVector = GUI.BeginScrollView (new Rect (Screen.width / 2 - guiDimension.x / 2, Screen.height / 2 - guiDimension.y / 2, guiDimension.x, guiDimension.y), scrollViewVector, new Rect (0, 0, 0, longlist));

			//Bottom right group of buttons
			GUI.BeginGroup (new Rect (0, 0, guiDimension.x, longlist));
			//Loop through each target
			for (int i = 0; i < listSavedGames.Length; i++) {
				//print("Button " + (i + 1).ToString() + " created: " + new Rect((i * 100), 0, 100, 50).ToString());
				//Create a button for each target, loading the attached script with source and target coordinates
				if (GUI.Button (	new Rect (	0, (i * 60) + (i * 10), guiDimension.x - 30, 60	) ,	listSavedGames[i]	)	) {
					Debug.Log ("You pressed "+ listSavedGames[i]);
				}
			}
			GUI.EndGroup ();

			// End the ScrollView
			GUI.EndScrollView ();
		}
	}

	public void BackToMainMenuFromExit(){
		mainMenu.transform.localPosition= new Vector3 (0, 0, 0);
		mainMenu.SetActive (true);
		exitMessage.SetActive (false);
	}

	public void ToExitGame(){
		exitMessage.transform.localPosition= new Vector3 (0, 0, 0);
		mainMenu.SetActive (false);
		exitMessage.SetActive (true);
	}

	public void GooGoodBye(){
		Application.Quit ();
	}

	public void BackToMainMenuFromNewGame(){
		mainMenu.transform.localPosition= new Vector3 (0, 0, 0);
		mainMenu.SetActive (true);
		newGame.SetActive (false);
	}

	public void ToNewGame(){
		newGame.transform.localPosition= new Vector3 (0, 0, 0);
		mainMenu.SetActive (false);
		newGame.SetActive (true);
	}

	public void BackToMainMenuFromNewCharacter(){
		mainMenu.transform.localPosition= new Vector3 (0, 0, 0);
		mainMenu.SetActive (true);
		newCharacter.SetActive (false);
	}

	public void ToNewCharacter(){
		newCharacter.transform.localPosition= new Vector3 (0, 0, 0);
		newGame.SetActive (false);
		newCharacter.SetActive (true);
	}

	public void BackToNewCharacter(){		
		alertMessage.SetActive (false);
		newCharacter.SetActive (true);
	}

	public void NewStart(){
		string name = nameCharacterInputField.GetComponent<InputField> ().text;
		string path = "Assets/Resources/Saved/" + name + ".data";
		if (File.Exists (path) == false) {
			FileStream f = new FileStream (path, FileMode.CreateNew);

			float offSetX = UnityEngine.Random.Range (-99999f, 99999f);
			//Debug.Log (offSetX);
			byte[] byteArray = BitConverter.GetBytes(offSetX);
			f.Write (byteArray,0,byteArray.Length);

			float offSetY = UnityEngine.Random.Range (-99999f, 99999f);
			//Debug.Log (offSetY);
			byteArray = BitConverter.GetBytes(offSetY);
			f.Write (byteArray,0,byteArray.Length);

			byteArray = System.Text.Encoding.UTF8.GetBytes(name);
			f.Write (byteArray,0,byteArray.Length);
			//SceneManager.LoadScene (1);
			f.Close ();
			Debug.Log (name + " Character Saved!");
		} else {
			alertMessage.transform.localPosition= new Vector3 (0, 0, 0);
			newCharacter.SetActive (false);
			alertMessage.SetActive (true);
		}
		//SceneManager.LoadScene ("Game");
	}

	public void ToSavedGames(){
		savedGames.transform.localPosition= new Vector3 (0, 0, 0);
		mainMenu.SetActive (false);
		savedGames.SetActive (true);
		listSavedGames = Directory.GetFiles(Application.dataPath+ "/Resources/Saved","*.data");
		for(int i=0;i<listSavedGames.Length;i++){
			listSavedGames[i] = Filter (listSavedGames[i]);
		}
		isVisibleSavedGames = true;
		int dimensiune = listSavedGames.Length * 70;
		if (dimensiune > guiDimension.y) {
			longlist = dimensiune + 10;
		} else {
			longlist = guiDimension.y;
		}

	}

	public void BackToMainMenuFromSavedGames(){		
		isVisibleSavedGames = false;
		savedGames.SetActive (false);
		mainMenu.SetActive (true);
	}

	string Filter(string stringSource){
		int positionLastSlash = 0;
		int positionDot =0;
		for (int i = 0; i < stringSource.Length; i++) {
			if (stringSource [i] == '/')
				positionLastSlash = i+1;
			else if (stringSource [i] == '.')
				positionDot = i;
		}
		string result = new string (stringSource [positionLastSlash], 1);
		for(int i=positionLastSlash+1;i<positionDot;i++){
			result += stringSource[i];
		}
		return result;
	}

	public void SoundVolume(){
		if (isSoundMuted == false) {
			isSoundMuted = true;
			//soundButton.GetComponent<Image> ().over
			//soundButton.GetComponent<Image>().overrideSprite = MyNewSprite;
			soundButton.GetComponent<Image>().overrideSprite = Resources.Load<Sprite> ("Sprites/UI/1");
			//soundButton.GetComponent<Image> ().sprite = Resources.Load<Sprite> ("UI/1");
		} else {
			isSoundMuted = false;
			//soundButton.GetComponent<Image> ().sprite = Resources.Load<Sprite> ("UI/0");
			soundButton.GetComponent<Image>().overrideSprite = Resources.Load<Sprite> ("Sprites/UI/0");
		}
	}


	public void BackToMainMenuFromSettings(){
		mainMenu.SetActive (true);
		settings.SetActive (false);
	}

	public void ToSettings(){		
		settings.transform.localPosition= new Vector3 (0, 0, 0);
		mainMenu.SetActive (false);
		settings.SetActive (true);
	}
}
	