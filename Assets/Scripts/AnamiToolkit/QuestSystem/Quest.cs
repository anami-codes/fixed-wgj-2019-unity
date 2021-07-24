using AnamiToolkit.Misc;
using System.Collections;
using System.Collections.Generic;

namespace AnamiToolkit.QuestSystem
{
	public class Quest
	{
		public Quest( string sceneId , string id , string name , string description , List<string> conditional )
		{
			m_sceneId = sceneId;
			m_id = id;
			m_name = name;
			m_description = description;
			m_conditional = new Conditional ( conditional );
		}

		public Quest( Dictionary<string , string> data )
		{
			m_conditional = new Conditional ();
			foreach ( string key in data.Keys )
			{
				switch ( key )
				{
					case "sceneId":
						m_sceneId = data[key];
						break;
					case "id":
						m_id = data[key];
						break;
					case "name":
						m_name = data[key];
						break;
					case "description":
						m_description = data[key];
						break;
					default:
						if ( key.Contains ( "conditions" ) )
						{
							m_conditional.AddCondition ( data[key] );
						}
						break;
				}
			}
		}

		public string Id
		{
			get { return m_sceneId + "_" + m_id; }
		}

		public string Name
		{
			get { return m_name; }
		}

		public string Description
		{
			get { return m_description; }
		}

		public Conditional Conditional
		{
			get { return m_conditional; }
		}

		private string m_sceneId;
		private string m_id;
		private string m_name;
		private string m_description;
		private Conditional m_conditional;
	}
}