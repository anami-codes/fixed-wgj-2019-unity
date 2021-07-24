using System.Collections.Generic;

namespace AnamiToolkit.DialogueSystem
{
	public static class DialogueCode
	{
		public enum CustomCodeType
		{
			None,
			ActorName,
			TextSpeed,
		}

		public static CustomCodeType GetCustomCodeType()
		{
			return CustomCodeType.None;
		}

		public static string GetCodeEnd( string code )
		{
			string[] s = code.Split ( '=' );
			string closing = s[0].TrimEnd ( '>', ']' );
			closing = closing.TrimStart ( '<' , '[' );
			return "/" + closing;
		}

		public static string GetHTMLCode( string s, int startPoint )
		{
			string code = "";

			for ( int i = startPoint ; i < s.Length ; i++ )
			{
				code += s[i];
				if ( s[i] == '>' ) break;
			}

			return code;
		}

	}
}
