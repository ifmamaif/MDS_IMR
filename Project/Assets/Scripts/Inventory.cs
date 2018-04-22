using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour {
	#region 
	public static Inventory instance;

	void Awake(){
		if (instance != null) {
			Debug.LogWarning ("More instance of inventory!");
			return;
		}
		instance = this;
	}
	#endregion
	public delegate void OnItemChanged ();
	public OnItemChanged onItemChangedCallBack;
	public int Space = 20;
	public List<Item> items = new List<Item>();

	public bool Add(Item item){
		if(!item.isDefaultItem){
			if (items.Count >= Space) {
				Debug.Log ("Not enough space in inventory!");
				return false;
			}
			items.Add(item);
			if(onItemChangedCallBack != null)
				onItemChangedCallBack.Invoke ();
			//return true;
		}
		return true;
	}

	public void Remove(Item item){
		items.Remove(item);
		if(onItemChangedCallBack != null)
			onItemChangedCallBack.Invoke ();
	}
}
