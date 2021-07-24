using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using AnamiToolkit.DataSystem;
using AnamiToolkit.DataSystem.Parser;

namespace AnamiToolkit.DialogueSystem 
{
	public class DialogueManager : MonoBehaviour
	{
		public static DialogueManager instance;

		[Header ( "Visual Components" )]
		public Text dialogueTextShort;
		public Text dialogueTextLong;
		public Image dialogueBox;
		public GameObject dialogueActor;
		public Image dialogueActorImage;

		[Header ( "Text Speed Settings" )]
		public float slow = 1f;
		public float normal = 1f;
		public float fast = 1f;

		[Header ( "Debug Settings" )]
		public string debugId = "DEBUG_DEBUG_DTEST";
		public bool debugRetry;

		void Start()
		{
			instance = this;
			m_speed = normal;
		}

		void Update()
		{
			if ( m_inDialogue )
			{
				if ( m_writing && m_timer <= Time.time )
				{
					WriteText ();
				}

				if ( !m_writing && Input.GetMouseButtonDown ( 0 ) )
				{
					GetNextLine ();
					if ( !m_inDialogue )
						GetNewDialogue ( m_currentDialogue.NextId );
				}
			}
		}

		public static bool InDialogue
		{
			get { return m_inDialogue; }
		}

		public void GetNewDialogue( string dialogueId )
		{
			if ( dialogueId != "" )
			{
				m_currentDialogue = GlobalArchive.GetDialogue ( dialogueId );
				m_inDialogue = true;
				m_lineIndex = -1;
				GetNextLine ();
			}
			else
			{
				CloseDialogueBox ();
			}
		}

		public  void GetMessage ( List<string> message )
		{
			if ( message.Count > 0 )
			{
				m_currentDialogue = new Dialogue ( message );
				m_inDialogue = true;
				m_lineIndex = -1;
				GetNextLine ();
			}
		}

		private void GetNextLine()
		{
			dialogueTextLong.text = "";
			dialogueTextShort.text = "";
			m_lineIndex++;

			if ( m_lineIndex < m_currentDialogue.NumLines )
			{
				m_currentLine = m_currentDialogue.GetDialogue ( m_lineIndex );
				OpenDialogueBox ();
				m_writing = true;
			}
			else
			{
				m_inDialogue = false;
			}
		}

		private void WriteText()
		{
			m_charIndex++;

			if ( m_charIndex < m_currentLine.Length )
			{
				char c = m_currentLine[m_charIndex];

				if ( c == '<' )
				{
					AddHTMLCode ();
				}
				else if ( c == '[' )
				{

				}
				else if ( c == '\n' )
				{
					AddToString ( 1 );
				}
				else if ( c != '>' && c != ']' )
				{
					AddToString ( 1 );
					m_timer = Time.time + ( m_speed * Time.deltaTime );
				}

				m_currentText.text = m_textToWrite[0] + m_textToWrite[1] + m_textToWrite[2];
			}
			else
			{
				m_writing = false;
			}
		}

		private void OpenDialogueBox()
		{
			dialogueTextLong.text = "";
			dialogueTextShort.text = "";

			m_charIndex = -1;
			m_textToWrite[0] = "";
			m_textToWrite[1] = "";
			m_textToWrite[2] = "";

			if ( m_currentDialogue.UseFace )
			{
				m_currentText = dialogueTextShort;
				dialogueActorImage.sprite = Resources.Load<Sprite> ( "Sprites/UI/Faces/" + m_currentDialogue.Scene + "_" + m_currentDialogue.Actor );
				dialogueActor.SetActive ( true );
			}
			else
			{
				m_currentText = dialogueTextLong;
			}
			dialogueBox.enabled = true;
		}

		private void CloseDialogueBox()
		{
			dialogueTextLong.text = "";
			dialogueTextShort.text = "";
			dialogueActor.SetActive ( false );
			dialogueActorImage.sprite = null;
			dialogueBox.enabled = false;
		}

		private void AddHTMLCode ()
		{
			if ( m_currentLine[m_charIndex + 1] != '/' )
			{
				string code = DialogueCode.GetHTMLCode ( m_currentLine , m_charIndex );
				AddToString ( 0, code );
				m_charIndex = m_textToWrite[0].Length - 1 ;
				AddToString ( 2 , "<" + DialogueCode.GetCodeEnd ( code ) + ">" );
			}
			else 
			{
				CheckClosingCode ();
			}
		}

		private void AddToString( int i , string s = "" )
		{ 
			if ( s != "" )
			{
				if ( i == 0 )
				{
					m_textToWrite[0] += m_textToWrite[1];
					m_textToWrite[0] += s;
					m_textToWrite[1] = "";
				}
				else if ( i == 2 )
				{
					m_textToWrite[2] = s + m_textToWrite[2];
				}
			}
			else if ( i == 1 )
			{
				m_textToWrite[1] += m_currentLine[m_charIndex];
			}
		}

		private void CheckClosingCode ()
		{
			m_textToWrite[0] += m_textToWrite[1];
			m_textToWrite[1] = "";

			for ( int i = m_charIndex ; i < m_currentLine.Length ; i++ )
			{
				m_charIndex = i - 1;

				if ( m_textToWrite[2].Length > 0 && m_currentLine[i] == m_textToWrite[2][0] )
				{
					m_textToWrite[0] += m_textToWrite[2][0];
					m_textToWrite[2] = m_textToWrite[2].Substring ( 1 );
				}
				else
				{
					break;
				}
			}
		}

		private Dialogue m_currentDialogue = null;
		private Text m_currentText = null;
		private string m_currentLine = "";
		private string[] m_textToWrite = new string[3] { "", "", "" };
		private int m_lineIndex = -1;
		private int m_charIndex = -1;

		private float m_speed = 1f;
		private float m_timer = 0f;

		private bool m_writing = false;
		private static bool m_inDialogue = false;
	}
}