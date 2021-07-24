using AnamiToolkit.QuestSystem;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace PressToTry.GUI
{
	public class JournalManager : MonoBehaviour
	{
		public static JournalManager instance;

		public Animator journalButton;
		public GameObject journal;
		public Text title;
		public Text description;

		void Start()
		{
			if ( instance == null )
				instance = this;
			m_quests = new List<Quest> ();
		}

		public static bool InJournal
		{
			get { return m_inJournal; }
		}

		public void OpenJournal()
		{
			m_inJournal = true;
			m_currentPage = m_quests.Count - 1;
			WriteEntry ();
			journalButton.SetBool ( "alert" , false );
			journal.SetActive ( true );
		}

		public void CloseJournal()
		{
			journal.SetActive ( false );
			m_inJournal = false;
		}

		public void PreviousEntry()
		{
			WriteEntry ( -1 );
		}

		public void NextEntry()
		{
			WriteEntry ( 1 );
		}

		public void AddNewQuest( Quest newQuest )
		{
			if ( newQuest != null )
			{
				m_quests.Add ( newQuest );
				journalButton.SetBool ( "alert" , true );
			}
		}

		private void WriteEntry( int next = 0 )
		{
			int nextPage = m_currentPage + next;
			if ( nextPage >= 0 && nextPage < m_quests.Count )
			{
				m_currentPage = nextPage;
				title.text = m_quests[m_currentPage].Name;
				description.text = m_quests[m_currentPage].Description;
			}
		}

		private List<Quest> m_quests;
		private int m_currentPage = 0;
		private static bool m_inJournal;
	}
}