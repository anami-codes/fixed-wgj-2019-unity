using UnityEngine;
using UnityEngine.SceneManagement;

namespace PressToTry.GUI
{
	public class MenuManager : MonoBehaviour
	{
		public GameObject quitPopUp;

		public void MainMenu()
		{
			SceneManager.LoadScene ( 1 , LoadSceneMode.Single );
		}

		public void StartGame()
		{
			SceneManager.LoadScene ( 2 , LoadSceneMode.Single );
		}

		public void CallQuitPopUp()
		{
			if ( !m_quitPopUp )
			{
				quitPopUp.SetActive ( true );
			}
		}

		public void Quit( bool quit )
		{
			if ( quit )
			{
				Application.Quit ();
			}
			else
			{
				quitPopUp.SetActive ( false );
			}
		}

		public void Save()
		{

		}

		public void Load()
		{

		}

		private bool m_quitPopUp = false;
	}
}
