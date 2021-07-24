using PressToTry.GUI;
using PressToTry.Level;
using AnamiToolkit.DialogueSystem;
using AnamiToolkit.Misc;
using System.Collections.Generic;
using UnityEngine;

namespace PressToTry.Elements
{
	public class TransportHolder : TemplateHolder
	{
		public ZoneHolder nextPoint;

		protected override void Start()
		{
			base.Start ();
		}

		protected override void Update()
		{
			base.Update ();
			if ( m_waitFade && Transition.instance.CheckFade )
			{
				SetPlayerPos ();
				Transition.instance.CallFade ( false );
				m_waitFade = false;
			}
		}

		public override bool Interact()
		{
			if ( m_condition.ConditionsMet () )
			{
				return base.Interact ();
			}
			else
			{
				DialogueManager.instance.GetMessage ( failureMessage );
				return false;
			}
		}

		protected override void Interaction()
		{
			base.Interaction ();
			Transition.instance.CallFade ( true );
			m_waitFade = true;
			SortingLayerManager.instance.ChangeRoom ( nextPoint.roomName );
		}

		private void SetPlayerPos()
		{
			CameraMovement.cam.ChangeZone ( nextPoint );
			PlayerController.player.transform.position = nextPoint.transform.position;
			PlayerController.player.SetTarget ( nextPoint.transform.position );
		}
		
		private bool m_waitFade;
	}
}
