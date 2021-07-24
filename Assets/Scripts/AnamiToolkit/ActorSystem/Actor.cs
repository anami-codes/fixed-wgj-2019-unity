using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AnamiToolkit.ActorSystem
{
	public class Actor
	{
		public Actor( string sceneId, string id, string name )
		{
			m_sceneId = sceneId;
			m_id = id;
			m_name = name;
		}

		public Actor( Dictionary<string , string> data )
		{
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
				}
			}
		}

		public string Id
		{
			get { return  m_sceneId + "_" + m_id; }
		}

		public string Name
		{
			get { return m_name; }
		}

		private string m_sceneId;
		private string m_id;
		private string m_name;
	}
}
