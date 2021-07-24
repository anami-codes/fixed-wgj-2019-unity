using UnityEngine;
using UnityEngine.UI;

namespace PressToTry.GUI
{
	public class Transition : MonoBehaviour
	{
		public static Transition instance;

		public Image fade;
		public float speed;

		void Start()
		{
			instance = this;
			m_color = fade.color;
			m_fadeOver = true;
		}

		public void CallFade( bool fadeIn )
		{
			PlayerController.player.ChangeControl ( false );
			m_fadeIn = fadeIn;
			m_fadeOver = false;
			m_t = 0;
		}

		public bool CheckFade
		{
			get
			{
				return m_t >= 1f;
			}
		}

		void Update()
		{
			if ( m_fadeIn && m_t < 1f )
			{
				Fade ( 0f , 1f );
			}
			else if ( !m_fadeIn && m_t < 1f )
			{
				Fade ( 1f , 0f );
			}

			if ( !m_fadeOver && !m_fadeIn && CheckFade )
			{
				PlayerController.player.ChangeControl ( true );
				m_fadeOver = true;
			}
		}

		private void Fade( float a, float b )
		{
			m_t += Time.deltaTime;
			m_color.a = Mathf.Lerp ( a , b , m_t * speed );
			fade.color = m_color;
		}

		private Color m_color;
		private bool m_fadeIn;
		private bool m_fadeOver;
		private float m_t;
	}
}
