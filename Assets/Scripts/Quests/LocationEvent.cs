using Moonshot.GameManagement;
using Moonshot.Locations;

using UnityEngine;

namespace Moonshot.Quests {
	[CreateAssetMenu(fileName = "New Location Event", menuName = "Quests/Location Event")]
	public class LocationEvent : BaseEvent {

		public Location location;

		public override void Init() {
			GameEvents.Instance.onEnteredLocation += CheckEnteredLocation;
		}

		protected void CheckEnteredLocation(Location _location) {
			if (Status != EventStatus.CURRENT) { return; }
			if (_location == location) {
				Debug.Log($"Completed Quest Event: {location.Name}");
				this.UpdateEvent(EventStatus.DONE);
			}
		}
	}
}
