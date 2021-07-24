using System.Collections;
using System.Collections.Generic;

namespace AnamiToolkit.DialogueSystem 
{
	public class Dialogue
	{
		public Dialogue( List<string> dialogues )
		{
			m_sceneId = "GENERAL";
			m_actorId = "NONE";
			m_dialogueId = "MESSAGE";
			m_dialogues = dialogues;
			m_conditions = new List<string> ();
			m_nextId = "";
		}

		public Dialogue( string sceneId , string actorId , string dialogueId , List<string> dialogues , List<string> conditions , string nextId = "", bool useFace = false )
		{
			m_sceneId = ( sceneId != "" ) ? sceneId : "DEBUG";
			m_actorId = ( actorId != "" ) ? actorId : "DEBUG";
			m_dialogueId = dialogueId;
			m_dialogues = dialogues;
			m_conditions = conditions;
			m_nextId = nextId;
			m_useFace = useFace;
		}

		public Dialogue( Dictionary<string , string> data )
		{
			m_dialogues = new List<string> ();
			m_conditions = new List<string> ();

			foreach ( string key in data.Keys )
			{
				switch ( key )
				{
					case "sceneId":
						m_sceneId = data[key];
						break;
					case "actorId":
						m_actorId = data[key];
						break;
					case "dialogueId":
						m_dialogueId = data[key];
						break;
					case "nextId":
						m_nextId = data[key];
						break;
					case "useFace":
						m_useFace = bool.Parse ( data[key] );
						break;
					default:
						if ( key.Contains ( "dialogues" ) )
						{
							m_dialogues.Add ( data[key] );
						}
						if ( key.Contains ( "conditions" ) )
						{
							m_conditions.Add ( data[key] );
						}
						break;
				}
			}

			if ( m_nextId == null )
				m_nextId = "";
		}

		public string GetDialogue( int index ) {
			return m_dialogues[index];
		}

		public string FullId
		{
			get
			{
				return m_sceneId + "_" + m_actorId + "_" + m_dialogueId;
			}
		}

		public string Id
		{
			get { return m_dialogueId; ; }
		}

		public string Actor
		{
			get { return m_actorId; }
		}

		public string Scene
		{
			get { return m_sceneId; }
		}

		public int NumLines
		{
			get { return m_dialogues.Count; }
		}

		public List<string> Conditions
		{
			get { return m_conditions; }
		}

		public string NextId
		{
			get { return m_nextId; }
		}

		public bool UseFace
		{
			get { return m_useFace; }
		}

		private string m_dialogueId;
		private string m_actorId;
		private string m_sceneId;
		private List<string> m_dialogues;
		private List<string> m_conditions;
		private string m_nextId;
		private bool m_useFace;
    }
}