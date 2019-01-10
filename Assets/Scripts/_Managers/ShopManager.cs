using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Items
{
	public string itemName;
	public int itemCount;
	public int itemPrice;
	public Text itemText;

	public Items(string _itemName, int _itemCount, int _itemPrice, Text _itemText)
	{
		this.itemName = _itemName;
		this.itemCount = _itemCount;
		this.itemPrice = _itemPrice;
		this.itemText = _itemText;
	}

	public void ShowCant()
	{
		itemText.text = itemCount.ToString();
	}

	public bool Buy()
	{
		if(GameManager.manager.coins >= itemPrice)
		{
			itemCount++;
			GameManager.manager.coins -= itemPrice;
			GameManager.manager.ui.UpdateMainCoin();
			ShowCant();
			return true;
		}
		else return false;
	}
}

public class ShopManager : MonoBehaviour {

	public List<Items> itemList = new List<Items>();
	public AudioClip buySFX;
	public AudioClip denySFX;
	
	// Use this for initialization
	void Start () {
		for (int i = 0; i < itemList.Count; i++)
		{
				itemList[i].ShowCant();
		}
	}
	
	// Update is called once per frame
	public void BuyItem (string _item) {
		for (int i = 0; i < itemList.Count; i++)
		{
			if(itemList[i].itemName == _item) 
				ShopSFX(itemList[i].Buy() ? true : false);
		}
	}

	void ShopSFX(bool value)
	{
		if(value)
			GameManager.manager.soundMng.SetCurrentEffect(buySFX);
		else
			GameManager.manager.soundMng.SetCurrentEffect(denySFX);
	}
}
