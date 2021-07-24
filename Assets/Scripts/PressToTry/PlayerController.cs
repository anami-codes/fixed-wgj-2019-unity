using AnamiToolkit.ActorSystem;
using AnamiToolkit.DataSystem;
using AnamiToolkit.DialogueSystem;
using AnamiToolkit.ItemSystem;
using PressToTry.Elements;
using PressToTry.GUI;
using PressToTry.Level;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PressToTry
{
	public class PlayerController : MonoBehaviour
	{
		static public PlayerController player;
		public Inventory inventory;

		public string actorId;
		public float speed = 1f;

		void Start()
		{
			player = this;
			inventory = new Inventory ( 99 , 5 );
			m_actor = GlobalArchive.GetActor ( actorId );
			m_sprite = GetComponentInChildren<SpriteRenderer> ();
			m_anim = GetComponent<Animator> ();

			m_collider = GetComponent<Collider2D> ();
			m_target = transform.position;
			m_canControl = true;
			m_canMove = true;
		}

		void Update()
		{
			if ( m_canControl == DialogueManager.InDialogue )
				m_canControl = !DialogueManager.InDialogue;

			if ( LevelManager.CanTakeAction () && m_canControl && Input.GetMouseButtonDown ( 0 ) )
			{
				GetAction ();
			}

			if ( m_canMove && Vector3.Distance ( transform.position , m_target ) > 0.1f )
			{
				m_anim.SetBool ( "Walking" , true );
				transform.position = Vector3.MoveTowards ( transform.position , m_target , speed * Time.deltaTime );
			}
			else
			{
				m_anim.SetBool ( "Walking" , false );
			}
		}

		public Collider2D playerCollider
		{
			get
			{
				return m_collider;
			}
		}

		public void FixZ( float z )
		{
			Vector3 newPos = transform.position;
			newPos.z = -z;
			transform.position = newPos;
			m_target.z = -z;
		}

		public void StopMovement()
		{
			m_target = transform.position;
			m_canControl = true;
			m_hitCounter = 0;
		}

		public void ChangeControl ( bool canControl )
		{
			m_canControl = canControl;
			m_canMove = canControl;
			m_hitCounter = 0;
		}

		public bool CallInventory ( Item item, bool take )
		{
			bool success = false;
			if ( take )
			{
				success = inventory.TakeFromInventory ( item.Id );
			}
			else
			{
				success = inventory.AddToInventory ( item );
			}

			if ( success )
				InventoryHud.inventoryHud.UpdateInventory ( inventory );

			return success;
		}

		public void SetTarget( Vector2 target )
		{
			m_target = target;
			m_target.z = transform.position.z;
			if ( m_target.x < transform.position.x )
			{
				transform.eulerAngles = new Vector3 ( 0 , 180 , 0 );
			}
			else
			{
				transform.eulerAngles = new Vector3 ( 0 , 0 , 0 );
			}
			m_moving = true;
		}

		private void GetAction()
		{
			RaycastHit2D raycastHit = Physics2D.Raycast ( Camera.main.ScreenToWorldPoint ( Input.mousePosition ) , Vector3.forward , 100f );
			if ( raycastHit && raycastHit.transform.gameObject.layer == 8 )
			{
				Transform other = raycastHit.transform;

				switch ( other.tag )
				{
					case "Ground":
						SetTarget ( Camera.main.ScreenToWorldPoint ( Input.mousePosition ) );
						break;
					case "Interactable":
						InteractWith ( other.GetComponent<TemplateHolder> () );
						break;
				}
			}
		}

		private void InteractWith( TemplateHolder other )
		{
			bool canInteract = !other.Interact ();
			if ( canInteract )
			{
				SetTarget ( other.transform.position );
				m_canControl = false;
			}
		}

		void OnCollisionEnter2D( Collision2D collision )
		{
			StopMovement ();
		}

		void OnCollisionStay2D( Collision2D collision )
		{
			if ( m_moving )
			{
				++m_hitCounter;
				if ( m_hitCounter >= 60 )
				{
					StopMovement ();
				}
			}
		}

		private SpriteRenderer m_sprite;
		private Animator m_anim;
		private Actor m_actor;

		private Collider2D m_collider;
		private Vector3 m_target;
		private bool m_canControl = false;
		private bool m_canMove = false;
		private bool m_moving = false;
		private int m_hitCounter = 0;
	}
}
