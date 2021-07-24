using System.Collections;
using System.Collections.Generic;

namespace AnamiToolkit.DataSystem
{
	public static class DebugLog
	{
		public enum Type
		{
			None,
			Search,
			Parser,
			Important,
			Start,
			Error,
		}

		static public void AddLog( string text, Type messageType = Type.None, bool title = false )
		{
			string log = ( title ? GetTitle ( messageType , text.ToUpper() ) : GetMessage ( messageType, text ) );
			m_logs.Enqueue ( log );
		}

		static public string GetLog()
		{
			return m_logs.Dequeue();
		}

		static public int GetLogLenght()
		{
			return m_logs.Count;
		}

		static private string GetMessage ( Type type, string text )
		{
			switch ( type )
			{
				case Type.Parser:
					return "<color=#00A708FF> Reading: </color>" + text;
				case Type.Search:
					return "<color=#003FD6FF> Searching: </color>" + text;
				case Type.Start:
					return "<color=#5C5C5CFF> Starting: </color>" + text;
				case Type.Important:
					return "<color=#FFAE00FF> Important: </color>" + text;
				case Type.Error:
					return "<color=red> " + text + "</color>\n";
				default:
					return text;
			}
		}

		static private string GetTitle( Type type, string title )
		{
			switch ( type )
			{
				case Type.Parser:
					return "<color=#00A708FF>" + title +  "</color>";
				case Type.Search:
					return "<color=#003FD6FF>" + title + "</color>";
				case Type.Start:
					return "<color=#5C5C5CFF>" + title + "</color>";
				case Type.Important:
					return "<color=#FFAE00FF>" + title + "</color>";
				default:
					return title;
			}
		}

		private static Queue<string> m_logs = new Queue<string>();
	}
}
