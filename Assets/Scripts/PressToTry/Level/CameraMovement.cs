using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PressToTry.Level
{
	public class CameraMovement : MonoBehaviour
	{
		public static CameraMovement cam;

		public ZoneHolder zone;

		public void ChangeZone( ZoneHolder nextZone )
		{
			Debug.Log ( "ChangeZone" );
			m_stop = true;
			zone = nextZone;
			m_cam.orthographicSize = zone.fieldOfView;
			transform.position = zone.startPos.position;
			m_camPos = transform.position;



			m_stop = false;
		}

		void Start()
		{
			cam = this;
			m_cam = GetComponent<Camera> ();
			m_camPos = transform.position;

			m_cam.orthographicSize = zone.fieldOfView;
			m_stop = false;
		}

		void Update()
		{
			if ( !m_stop )
			{
				MoveCamera ();
				transform.position = m_camPos;
			}
		}

		private void MoveCamera()
		{
			Vector3 playerPos = PlayerController.player.transform.position;

			if ( zone.minPos.position.x < playerPos.x && playerPos.x < zone.maxPos.position.x )
			{
				m_camPos.x = playerPos.x;
			}

			if ( zone.minPos.position.y < playerPos.y && playerPos.y < zone.maxPos.position.y )
			{
				m_camPos.y = playerPos.y;
			}
		}

		private Camera m_cam;
		private Vector3 m_camPos;
		private bool m_stop;
	}
}
