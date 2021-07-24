using System.Collections.Generic;

namespace AnamiToolkit.DataSystem.Parser
{
	public static class DataParser
	{
		public static void ParseDocument ( string text, DataManager dataManager )
		{
			GetEntries ( text, dataManager );
		}

		static void GetEntries ( string text, DataManager dataManager )
		{
			string[] textData = text.Split ( '"' );

			DataObject data = null;

			for ( int i = 0 ; i < textData.Length ; i++ )
			{

				if ( textData[i].Contains ( "Entry" ) )
				{
					if ( data != null )
						dataManager.AddToQueue ( data );

					data = new DataObject ();
				}
				else if ( textData[i].Contains( ":" ) && !textData[i].Contains ( "{" ) )
				{
					data.AddData ( textData[i - 1] , textData[i + 1] );
					++i;
				}
			}

			if ( data != null )
				dataManager.AddToQueue ( data );
		}

		static Queue<DataObject> m_dataQueue;
		static bool m_inEntry;
	}

	public class DataObject
	{
		public const string TYPE_ACTOR = "Actor";
		public const string TYPE_DIALOGUE = "Dialogue";
		public const string TYPE_ITEM = "Item";
		public const string TYPE_QUEST = "Quest";

		public Dictionary<string , string> data;

		public DataObject()
		{
			data = new Dictionary<string, string>();
			//Type objType = Type.GetType ( m_className );
			//object instance = Activator.CreateInstance ( objType , m_param );
		}

		public void AddData( string key, string value )
		{
			data.Add ( key , value );
		}
	}
}
