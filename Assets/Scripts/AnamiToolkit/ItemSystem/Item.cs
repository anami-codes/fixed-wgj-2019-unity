using System.Collections;
using System.Collections.Generic;

namespace AnamiToolkit.ItemSystem
{
	public class Item
	{
		const string ITEM_PATH = "Sprites/Items/";

		public Item ( string id, string name, string imgName )
		{
			m_id = id;
			m_name = name;
			m_imgName = imgName;
		}

		public Item( Dictionary<string , string> data )
		{
			foreach ( string key in data.Keys )
			{
				switch ( key )
				{
					case "id":
						m_id = data[key];
						break;
					case "name":
						m_name = data[key];
						break;
					case "imgPath":
						m_imgName = ITEM_PATH + data[key];
						break;
					case "imgNum":
						m_imgNum = int.Parse ( data[key] );
						break;
					case "spritesheet":
						m_spritesheet = bool.Parse ( data[key] );
						break;
				}
			}
		}

		public string Id
		{
			get { return m_id; }
		}

		public string Name
		{
			get { return m_name; }
		}

		public string ImgName
		{
			get { return m_imgName; }
		}

		public int ImgNum
		{
			get { return m_imgNum; }
		}

		public bool IsSpritesheet
		{
			get { return m_spritesheet; }
		}

		public void Use ()
		{

		}

		private string m_id;
		private string m_name;
		private string m_imgName;
		private int m_imgNum;
		private bool m_spritesheet;
	}
}
