using AnamiToolkit.Misc;
using System.Collections.Generic;
using UnityEngine;

namespace PressToTry.Elements
{
	public class TemplateHolder : MonoBehaviour
	{
		public string id;
		public List<string> successMessage;
		public List<string> failureMessage;
		public List<string> conditions;

		virtual protected void Start()
		{
			m_collider = GetComponent<Collider2D> ();
			m_condition = new Conditional ( conditions );
		}

		virtual protected void Update()
		{
			if ( m_waitInteraction )
			{
				Interact ();
			}
		}

		virtual public bool Interact()
		{
			float distance = m_collider.Distance ( PlayerController.player.playerCollider ).distance;

			if ( distance < m_distance )
			{
				Interaction ();
			}
			else if ( !m_waitInteraction )
			{
				m_waitInteraction = true;
			}
			return ( distance < m_distance );
		}

		virtual protected void Interaction()
		{
			Debug.Log ( "Interacting" );
			PlayerController.player.StopMovement ();
			m_waitInteraction = false;
			//Add DialogueHolder.Call
		}

		protected Collider2D m_collider = null;
		protected Conditional m_condition;
		protected float m_distance = 0.1f;
		protected bool m_waitInteraction = false;
	}
}
