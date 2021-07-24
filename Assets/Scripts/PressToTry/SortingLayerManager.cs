using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PressToTry
{
	public class SortingLayerManager : MonoBehaviour
	{
		public static SortingLayerManager instance;

		public List<Transform> rooms;
		public List<string> containerNames = new List<string> ()
		{
			"Items",
			"NPCs" ,
			"Interactables",
			"Enviroment"
		};

		void Awake()
		{
			instance = this;

			m_currentRoom = rooms[0].name;
			m_roomBorder = new Dictionary<string , float[]> ();

			foreach ( Transform room in rooms )
			{
				Debug.Log ( room.name );
				m_roomBorder.Add ( room.name , new float[2] );
				m_roomBorder[room.name][0] = room.Find ( "MinY" ).position.y;
				m_roomBorder[room.name][1] = room.Find ( "MaxY" ).position.y;
				GetAllSprites ( room );
			}
		}

		void Update()
		{
			PlayerController.player.FixZ ( GetPlayerZ () );
		}

		public void ChangeRoom( string roomName )
		{
			Debug.Log ( roomName );
			m_currentRoom = roomName;
		}

		private float GetPlayerZ()
		{
			float z = GetZ ( PlayerController.player.transform.position.y, m_currentRoom );
			return z;
		}

		private float GetZ( float otherY, string room )
		{
			float z = 0;
			otherY = otherY - m_roomBorder[room][0];
			float maxY = m_roomBorder[room][1] - m_roomBorder[room][0];

			float t = ( 1f * otherY ) / maxY;

			z = Mathf.Lerp ( 0 , -1 , t );

			return 1 + z;
		}

		private void GetAllSprites( Transform room )
		{
			m_sprites = new List<SpriteRenderer>();
			foreach ( string name_ in containerNames )
			{
				Transform container =  room.Find ( name_ );
				int childCount = container.childCount;
				AddSprite ( container.GetComponentsInChildren<SpriteRenderer>(true) , room.name );
			}

			RearrangeSprites ( room.name );
		}

		private void AddSprite( SpriteRenderer[] objects, string roomName )
		{
			foreach ( SpriteRenderer sprite in objects )
			{
				if ( sprite && sprite.sortingLayerName == "Middleground" )
				{
					m_sprites.Add ( sprite );
				}
			}
		}

		private void RearrangeSprites( string room )
		{
			foreach ( SpriteRenderer sprite in m_sprites )
			{
				Vector3 newPos = sprite.transform.position;
				newPos.z = - GetZ ( sprite.transform.position.y, room );
				sprite.transform.position = newPos;
			}
		}

		private Dictionary<string , float[]> m_roomBorder;
		private List<SpriteRenderer> m_sprites;
		private string m_currentRoom;
		private float m_playerPos;
	}
}