using UnityEngine;
using Moonshot.GameManagement;

namespace Moonshot.UI { 

	public class LoadGameButton : MonoBehaviour {

		public void LoadGame() {
			GameManager.Instance.LoadGameScene();
		}

	}

}
