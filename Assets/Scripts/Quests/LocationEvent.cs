using Moonshot.GameManagement;
using Moonshot.Locations;

using UnityEngine;

namespace Moonshot.Quests {
	public class LocationEvent : BaseEvent {

		public Location location { get; protected set; }

		public LocationEvent(string _n, string _d, Location _location) : base(_n, _d) {
			location = _location;

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
