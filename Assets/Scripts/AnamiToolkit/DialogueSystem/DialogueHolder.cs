using AnamiToolkit.DataSystem;
using AnamiToolkit.Misc;
using System.Collections.Generic;
using UnityEngine;

namespace AnamiToolkit.DialogueSystem
{
	public class DialogueHolder : MonoBehaviour
	{
		public List<string> dialogues;
		public bool repeatLast = false;

		public void CallDialogue()
		{
			ChoseDialogue ();

			if ( m_loop && m_currentDialogue != null && !DialogueManager.InDialogue )
			{
				DialogueManager.instance.GetNewDialogue ( m_currentDialogue );
				Conditional.CompleteCondition ( m_currentDialogue );
			}
		}

		void Start()
		{
			m_index = -1;
		}

		private void ChoseDialogue()
		{
			if ( m_index < dialogues.Count - 1 )
			{
				NextDialogue ();
			}
			else
			{
				m_loop = repeatLast;
			}

			if ( m_nextDialogue != m_currentDialogue && m_condition.ConditionsMet () )
			{
				m_currentDialogue = m_nextDialogue;
			}
		}

		private void NextDialogue()
		{
			++m_index;
			m_nextDialogue = dialogues[m_index];
			Dialogue dialogue = GlobalArchive.GetDialogue ( m_nextDialogue );
			m_condition = new Conditional ( dialogue.Conditions );
		}

		private Conditional m_condition;
		private string m_currentDialogue;
		private string m_nextDialogue;
		private int m_index;
		private bool m_loop = true;
	}
}