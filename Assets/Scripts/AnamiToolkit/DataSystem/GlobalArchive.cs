using AnamiToolkit.DataSystem.Parser;
using AnamiToolkit.ActorSystem;
using AnamiToolkit.DialogueSystem;
using AnamiToolkit.ItemSystem;
using AnamiToolkit.QuestSystem;
using System.Collections.Generic;

namespace AnamiToolkit.DataSystem 
{
    public static class GlobalArchive 
    {
		static public bool archiveLoaded = false;

		static public void Init () 
        {
			if ( !archiveLoaded )
			{
				m_actorArchive = new Dictionary<string , Actor> ();
				m_dialogueArchive = new Dictionary<string , Dialogue> ();
				m_itemArchive = new Dictionary<string , Item> ();
				m_questArchive = new Dictionary<string , Quest> ();
			}			
        }

		//AddToArchive of all Types in the AnamiToolkit
		static public void AddToArchive ( DataObject data, string type)
		{
			switch ( type )
			{
				case DataObject.TYPE_DIALOGUE:
					AddToArchive( new Dialogue ( data.data ) );
					break;
				case DataObject.TYPE_ITEM:
					AddToArchive ( new Item ( data.data ) );
					break;
				case DataObject.TYPE_ACTOR:
					AddToArchive ( new Actor ( data.data ) );
					break;
				case DataObject.TYPE_QUEST:
					AddToArchive ( new Quest ( data.data ) );
					break;
			}
		}

		static public void AddToArchive( Actor data )
		{
			m_actorArchive.Add ( data.Id , data );
		}

		static public void AddToArchive( Dialogue data )
		{
			m_dialogueArchive.Add ( data.FullId , data );
		}

		static public void AddToArchive( Item data )
		{
			m_itemArchive.Add ( data.Id , data );
		}

		static public void AddToArchive( Quest data )
		{
			m_questArchive.Add ( data.Id , data );
		}

		//General
		public static bool Exist ( string id, string type ) 
        {
			switch ( type )
			{
				case DataObject.TYPE_ACTOR:
					return m_actorArchive.ContainsKey ( id );
				case DataObject.TYPE_DIALOGUE:
					return m_dialogueArchive.ContainsKey ( id );
				case DataObject.TYPE_ITEM:
					return m_itemArchive.ContainsKey ( id );
				case DataObject.TYPE_QUEST:
					return m_questArchive.ContainsKey ( id );
				default:
					return false;
			}
        }

		//Actor Related
		public static Actor GetActor( string id )
		{
			return m_actorArchive[id];
		}

		//Dialogue Related
		public static Dialogue GetDialogue( string id ) 
        {
            return m_dialogueArchive[id];
        }

		//Item Related
		public static Item GetItem( string id )
		{
			return m_itemArchive[id];
		}

		//Item Related
		public static Quest GetQuest( string id )
		{
			return m_questArchive[id];
		}

		static Dictionary<string , Actor> m_actorArchive;
		static Dictionary<string , Dialogue> m_dialogueArchive;
		static Dictionary<string , Item> m_itemArchive;
		static Dictionary<string , Quest> m_questArchive;
	}
}