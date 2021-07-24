using AnamiToolkit.DataSystem;
using AnamiToolkit.DialogueSystem;
using AnamiToolkit.QuestSystem;
using AnamiToolkit.Misc;
using PressToTry.GUI;
using System.Collections.Generic;
using UnityEngine;

namespace PressToTry.Level
{
	public class LevelManager : MonoBehaviour
	{
		public static LevelManager instance;

		public string sceneId;
		public List<string> questIds;

		void Awake()
		{
			if ( instance != null )
				instance = this;

			m_quests = new List<Quest> ();
			foreach ( string s in questIds )
			{
				m_quests.Add ( GlobalArchive.GetQuest ( sceneId + "_" + s ) );
			}
		}

		void Update()
		{
			if ( !m_started && Transition.instance.CheckFade )
			{
				m_started = true;
				StartLevel ();
			}

			if ( m_started )
			{
				CheckQuests ();
			}
		}

		public void StartLevel()
		{
			JournalManager.instance.AddNewQuest ( GlobalArchive.GetQuest ( sceneId + "_QUEST01") );
			Conditional.CompleteCondition ( sceneId + "_QUEST01" );
			JournalManager.instance.OpenJournal ();
		}

		public void CheckQuests()
		{
			foreach ( Quest quest in m_quests )
			{
				if ( quest.Conditional.ConditionsMet() )
				{
					JournalManager.instance.AddNewQuest ( GlobalArchive.GetQuest ( quest.Id ) );
					Conditional.CompleteCondition ( quest.Id );
					m_quests.Remove ( quest );
					break;
				}
			}
		}

		public static  bool CanTakeAction()
		{
			if ( DialogueManager.InDialogue )
				return false;
			else if ( JournalManager.InJournal )
				return false;
			else if ( !Transition.instance.CheckFade )
				return false;
			else
				return true;
		}

		private bool m_started;
		private int m_currentQuestId = 1;
		private List<Quest> m_quests;
	}
}