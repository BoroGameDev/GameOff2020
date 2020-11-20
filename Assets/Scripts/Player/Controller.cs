using Moonshot.GameManagement;

using UnityEngine;

namespace Moonshot.Player {

	public class Controller : MonoBehaviour {
		private void Awake() {
			GameManager.Instance.SetPlayer(gameObject);
		}
	}

}
