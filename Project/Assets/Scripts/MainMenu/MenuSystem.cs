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

	public GameObject nameCharacterInputField;

	public string[] listSavedGames;
	public GameObject contentSavedGames;
	public GameObject verticalScrollBarSavedGames;

	// Use this for initialization
	void Start () {
		mainMenu.SetActive (true);
		exitMessage.SetActive (false);
		newGame.SetActive (false);
		newCharacter.SetActive (false);
		alertMessage.SetActive (false);

	}

	/*
	// Update is called once per frame
	void Update () {
		
	}
	*/

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

		RectTransform rectr = contentSavedGames.GetComponent<RectTransform>();
		if (rectr.sizeDelta.y < (listSavedGames.Length * 16 + 4)) {
			rectr.sizeDelta = new Vector2 (rectr.sizeDelta.x, listSavedGames.Length * 16 + 4);
		}

		float pozY = rectr.transform.position.y - 10;	//	rectTransform.transform.position.y - button.recttransform.sizedelta.y/2 - 2;

		for (int i = 0; i < listSavedGames.Length; i++) {
			GameObject  child = (GameObject)Instantiate(Resources.Load("Prefabs/ButtonLoadCharacter",typeof(GameObject)));
			child.name = listSavedGames [i];
			child.transform.GetChild(0).gameObject.GetComponent<Text>().text = listSavedGames [0];
			child.transform.SetParent(contentSavedGames.transform);
			RectTransform childrectr = child.GetComponent<RectTransform> ();
			childrectr.sizeDelta = new Vector2 (81, 16);
			//childrectr.position = new Vector3 (-1,pozY+16*i, 0);
			child.transform.position = new Vector3(0,0,0);
			childrectr.localScale = new Vector3 (1, 1, 1);
			//child.transform.localPosition = new Vector3 (-1,pozY+16*i, 0);
			//child.transform.localScale = new Vector3 (1, 1, 1);
			Debug.Log("child "+i+" "+childrectr.position+ " "+ child.transform.position);
		}

		//	inventoryUI = (GameObject)Instantiate(Resources.Load("Prefabs/InventoryUI",typeof(GameObject)));
		//	inventoryUI.name = "InventoryUI";
		//	GameObject  ChildGameObject2 = ParentGameObject.transform.GetChild (1).gameObject;


		verticalScrollBarSavedGames.GetComponent<Scrollbar> ().value = 1;

		//listSavedGames = Directory.GetFileSystemEntries(Application.dataPath+ "/Resources/Saved","*.data");

		//Debug.Log( Directory.GetFiles (Application.dataPath+ "/Resources/Saved"));
		//Debug.Log (System.AppDomain.CurrentDomain.BaseDirectory);	//NULL
		//Debug.Log(Environment.CurrentDirectory);
		//Debug.Log (Application.dataPath);	//	/home/maify/IM/Mds/Assets
	}

	public void BackToMainMenuFromSavedGames(){		
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
}
	