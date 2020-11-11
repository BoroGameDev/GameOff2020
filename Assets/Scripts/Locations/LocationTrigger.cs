using Moonshot.GameManagement;

using UnityEngine;

namespace Moonshot.Locations {

	[RequireComponent(typeof(Collider2D))]
	public class LocationTrigger : MonoBehaviour {

		public Location location;

		private void OnTriggerEnter2D(Collider2D other) {
			if (!other.CompareTag("Player")) { return; }

			GameEvents.Instance.EnteredLocation(location);
		}
	}
}
