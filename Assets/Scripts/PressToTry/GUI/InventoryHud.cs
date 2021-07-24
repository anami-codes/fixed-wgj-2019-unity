using AnamiToolkit.ItemSystem;
using PressToTry.Elements;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace PressToTry.GUI
{
	public class InventoryHud : MonoBehaviour
	{
		static public InventoryHud inventoryHud;

		public Image[] m_slots;
		public Sprite defaultSprite;

		void Awake()
		{
			inventoryHud = this;
		}

		public void UpdateInventory( Inventory inventory )
		{
			List<Item> items = inventory.GetAllItems();

			for ( int i = 0 ; i < m_slots.Length ; i++ )
			{
				if ( items.Count > i )
				{
					m_slots[i].sprite = ItemHolder.GetItemSprite ( items[i] );
					m_slots[i].color = Color.white;
				}
				else
				{
					m_slots[i].sprite = defaultSprite;
					m_slots[i].color = Color.clear;
				}
			}
		}
	}
}