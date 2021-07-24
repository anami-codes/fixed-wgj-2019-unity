using AnamiToolkit.DataSystem;
using AnamiToolkit.ItemSystem;
using AnamiToolkit.Misc;
using System.Collections.Generic;
using UnityEngine;

namespace PressToTry.Elements
{
	public class CraftingHolder : TemplateHolder
	{
		public List<string> recipe;
		public string result;

		protected override void Start()
		{
			base.Start ();

			m_recipe = new List<Item> ();

			Item item = null;

			foreach ( string itemId in recipe )
			{
				item = GlobalArchive.GetItem ( itemId );

				if ( item != null )
					m_recipe.Add ( item );

				item = null;
			}

			item = GlobalArchive.GetItem ( result );
			if ( item != null )
				m_result = item;
		}

		protected override void Update()
		{
			base.Update ();
		}

		public override bool Interact()
		{
			return base.Interact ();
		}

		protected override void Interaction()
		{
			base.Interaction ();

			foreach ( Item item in m_recipe )
			{
				if ( !PlayerController.player.inventory.HasEnoughItems ( item.Id ) )
				{
					return;
				}
			}

			foreach ( Item item in m_recipe )
			{
				PlayerController.player.CallInventory ( item , true );
				Debug.Log ( "Took Item: " + item.Name );
			}

			PlayerController.player.CallInventory ( m_result , false );
			Conditional.CompleteCondition ( m_result.Id );
		}

		private List<Item> m_recipe;
		private Item m_result;
	}
}