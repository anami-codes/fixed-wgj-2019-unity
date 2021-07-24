using AnamiToolkit.DataSystem.Parser;
using AnamiToolkit.Misc;
using System.Collections.Generic;
using UnityEngine;

namespace AnamiToolkit.DataSystem 
{
    public class DataManager : MonoBehaviour 
    {
        void Awake() 
        {
			if ( instance == null )
				instance = this;

			Init ();
		}

		public void AddToQueue( DataObject data )
		{
			dataObjects.Enqueue ( data );
		}

		private void Init()
		{
			dataObjects = new Queue<DataObject> ();
			GlobalArchive.Init ();

			DebugLog.AddLog ( "Config.json" , DebugLog.Type.Parser );
			DataParser.ParseDocument ( Resources.Load<TextAsset> ( "Config/Config" ).text , this );
			config = dataObjects.Dequeue ().data;

			foreach ( string key in config.Keys )
			{
				Load ( config[key] , key );
			}
			GlobalArchive.archiveLoaded = true;
		}

		private void Load( string json, string dataType )
		{
			dataObjects = new Queue<DataObject> ();

			DebugLog.AddLog ( json + ".json" , DebugLog.Type.Parser );
			DataParser.ParseDocument ( Resources.Load<TextAsset> ( "Config/" + json ).text , this );
			DebugLog.AddLog ( "Done!" );

			while ( dataObjects.Count > 0 )
			{
				GlobalArchive.AddToArchive ( dataObjects.Dequeue () , dataType );
			}
		}

		private static DataManager instance;

		private Dictionary<string , string> config;
		private Queue<DataObject> dataObjects;
	}
}
