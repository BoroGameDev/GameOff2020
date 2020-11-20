using Moonshot.GameManagement;

using UnityEngine;

namespace Moonshot.UI {
	public class PauseMenu : MonoBehaviour {

		public void ResumeGame() {
			GameManager.Instance.UnpauseGame();
		}

		public void ExitToTitle() {
			GameManager.Instance.ToTitle();
		}

		public void QuitGame() {
			GameManager.Instance.QuitGame();
		}
	}
}
