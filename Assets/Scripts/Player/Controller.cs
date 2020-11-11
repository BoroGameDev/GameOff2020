using Moonshot.GameManagement;

using UnityEngine;

namespace Moonshot.Player {

	public class Controller : MonoBehaviour {
		private void Start() {
			GameManager.Instance.SetPlayer(gameObject);
		}
	}

}
