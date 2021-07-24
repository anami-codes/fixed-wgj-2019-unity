using AnamiToolkit.DataSystem;
using AnamiToolkit.ItemSystem;
using AnamiToolkit.Misc;
using UnityEngine;

namespace PressToTry.Elements
{
	public class ItemHolder : TemplateHolder
	{

		override protected void Start()
		{
			base.Start ();

			m_sprite = GetComponentInChildren<SpriteRenderer> ();
			m_item = GlobalArchive.GetItem ( id );
			m_sprite.sprite = GetItemSprite ( m_item );

			ShowItem ( m_condition.ConditionsMet () );
		}

		override protected void Update()
		{
			if ( m_itemHidden && m_condition.ConditionsMet () )
			{
				ShowItem ( m_condition.ConditionsMet () );
			}

			base.Update ();
		}

		override protected void Interaction()
		{
			base.Interaction ();
			m_taken = PlayerController.player.CallInventory ( m_item , false );
			Conditional.CompleteCondition ( m_item.Id );
			Clean ();
			Debug.Log ( "Received Item: " + m_item.Name + " - Status: " + m_taken );
		}

		static public Sprite GetItemSprite( Item item )
		{
			if ( item.IsSpritesheet )
			{
				Sprite[] sprites = Resources.LoadAll<Sprite> ( item.ImgName );
				return sprites[item.ImgNum];
			}
			else
			{
				return Resources.Load<Sprite> ( item.ImgName );
			}
		}

		private void Clean ()
		{
			if ( m_taken )
			{
				Destroy ( gameObject );
			}
		}

		private void ShowItem( bool show )
		{
			m_collider.enabled = show;
			m_sprite.enabled = show;
			m_itemHidden = !show;
		}

		private Item m_item = null;
		private SpriteRenderer m_sprite = null;

		private bool m_taken;
		private bool m_itemHidden;
	}
}
