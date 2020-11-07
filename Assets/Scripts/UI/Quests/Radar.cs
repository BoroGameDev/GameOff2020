using UnityEngine;
using UnityEngine.UI;
using Moonshot.GameManagement;

namespace Moonshot.UI.Quests {
	public class Radar : MonoBehaviour {
		[SerializeField] private GameObject indicator = null;
		[SerializeField] private Transform destination = null;
		private GameObject player;
		private RectTransform indicatorRect;

		private void Start() {
			player = GameManager.Instance.Player;
			indicatorRect = indicator.GetComponent<RectTransform>();
		}

		private void LateUpdate() {
			if (destination == null) {
				indicator.SetActive(false);
				return;
			}

			Vector3 direction = destination.transform.position - player.transform.position;

			if (direction.magnitude <= 7.5f) { 
				indicator.SetActive(false);
				return;
			}

			indicator.SetActive(true);
			float angleToTarget = Vector3.SignedAngle(player.transform.up, direction, player.transform.forward);
			indicatorRect.rotation = Quaternion.Euler(0f, 0f, angleToTarget);

		}
	}
}
