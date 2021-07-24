using AnamiToolkit.DataSystem;
using UnityEngine;
using UnityEngine.UI;

namespace AnamiToolkit.Misc.Log
{
	public class DebugScreenManager : MonoBehaviour
	{
		public static DebugScreenManager instance;

		public Text debugText;
		public RectTransform viewContent;
		public GameObject debugScreen;

		private void Awake()
		{
			DebugLog.AddLog ( "Debug Screen Manager" , DebugLog.Type.Start );
			instance = this;
			m_isDebugScreenActive = false;
			m_onHold = true;
			if (debugText != null ) CleanDebugScreen ();
			m_onHold = false;
		}

		public void ShowDebugScreen()
		{
			debugScreen.SetActive ( !m_isDebugScreenActive );
			m_isDebugScreenActive = debugScreen.activeSelf;
		}

		public void CleanDebugScreen()
		{
			debugText.text = "\n";
			viewContent.sizeDelta = new Vector2 ();
			m_onHold = false;
		}

		void Update()
		{
			if ( !m_onHold )
			{
				CheckLog ();
			}	
		}

		private void CheckLog()
		{
			while ( DebugLog.GetLogLenght() > 0 ) {
				if ( debugText != null && debugText.text.Length >= 5500 ) {
					WriteLog ( "<color=red>Log is too long. Clean Log to receive more messages.</color>" );
					m_onHold = true;
					return;
				} else {
					WriteLog ( DebugLog.GetLog () + "\n" );
				}
			}
			m_onHold = false;
		}

		private void WriteLog( string log )
		{
			if ( debugText != null )
			{
				debugText.text += log;
			}
			else
			{
				//Debug.Log ( log );
			}
		}

		bool m_isDebugScreenActive;
		bool m_onHold;
	}
}
