using AnamiToolkit.ActorSystem;
using AnamiToolkit.DataSystem;
using AnamiToolkit.DialogueSystem;
using UnityEngine;

namespace PressToTry.Elements
{
	public class ActorHolder : TemplateHolder
	{
		public DialogueHolder dialogue;

		override protected void Start()
		{
			base.Start ();
			m_actor = GlobalArchive.GetActor ( id );
		}

		override protected void Update()
		{
			base.Update ();
		}

		override protected void Interaction()
		{
			base.Interaction ();
			dialogue.CallDialogue ();
			Debug.Log ( "Talk to: " + m_actor.Name );
		}

		private Actor m_actor;
	}
}
