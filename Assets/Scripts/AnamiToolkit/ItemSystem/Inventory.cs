using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AnamiToolkit.ItemSystem
{
	public class Inventory
	{
		public Inventory( int maxAmount = 99, int maxSlots = -1 )
		{
			m_items = new List<Item> ();
			m_amounts = new List<int> ();

			m_maxAmount = maxAmount;
			m_maxSlots = maxSlots;
		}

		public List<Item> GetAllItems()
		{
			return m_items;
		}

		public bool AddToInventory ( Item item, int amount = 1 )
		{
			if ( item != null )
			{
				int index = FindSpaceInInventory ( item.Id , amount );

				if ( index >= 0 )
				{
					m_items[index] = item;
					m_amounts[index] += amount;
					return true;
				}
			}

			return false;
		}

		public bool TakeFromInventory( string itemId , int amount = 1 )
		{
			if ( itemId != "" )
			{
				int index = FindItemIndex ( itemId, amount );
				Debug.Log ( index );

				if ( index >= 0 )
				{
					m_items[index].Use ();
					m_amounts[index] -= amount;
					Debug.Log ( m_amounts[index] );

					if ( m_amounts[index] <= 0 )
					{
						Debug.Log ( "Remove" );
						m_items.RemoveAt ( index );
						m_amounts.RemoveAt ( index );
					}

					return true;
				}
			}

			return false;
		}

		public bool HasEnoughItems ( string itemId, int amount = 1 )
		{
			for (int i = 0 ; i < m_items.Count ; i++ )
			{
				if ( m_items[i].Id == itemId && m_amounts[i] >= amount )
					return true;
			}

			return false;
		}

		private int FindItemIndex ( string itemId, int amount = 1 )
		{
			int index = -1;

			for ( int i = 0 ; i < m_items.Count ; i++ )
			{
				Debug.Log ( m_items[i].Id + "@" + itemId + "@" + m_amounts[i] + "@" + amount );
				if ( m_items[i].Id == itemId && m_amounts[i] >= amount )
				{
					index = i;
					break;
				}
			}

			return index;
		}

		private int FindSpaceInInventory ( string itemId , int amount )
		{
			int index = -1;

			for ( int i = 0 ; i < m_items.Count ; i++ )
			{
				if ( m_items[i].Id == itemId && m_amounts[i] + amount < m_maxAmount )
				{
					index = i;
					break;
				}
			}

			if ( index == -1 && ( m_maxSlots == -1 || m_items.Count < m_maxSlots ) )
			{
				index = m_items.Count;
				m_items.Add ( null );
				m_amounts.Add ( 0 );
			}

			return index;
		}

		private List<Item> m_items;
		private List<int> m_amounts;

		private int m_maxSlots;
		private int m_maxAmount;
	}
}
