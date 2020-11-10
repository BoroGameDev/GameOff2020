using Moonshot.Items;
using Moonshot.Locations;

using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Moonshot.Quests {

	public class Quest {
		public string title = "Quest";
		public List<BaseEvent> events = new List<BaseEvent>();
		public bool completed;

		public Quest(string _title) {
			this.title = _title;
		}

		public void CheckEvents() {
			completed = events.All(evt => evt.Status == EventStatus.DONE);
		}

		public BaseEvent AddEvent(string _name, string _description) {
			BaseEvent e = new BaseEvent(_name, _description);
			events.Add(e);
			return e;
		}

		public CollectionEvent AddCollectionEvent(string _name, string _description, int _requiredAmount, Item _item) {
			CollectionEvent e = new CollectionEvent(_name, _description, _requiredAmount, _item);
			events.Add(e);
			return e;
		}

		public LocationEvent AddLocationEvent(string _name, string _description, Location _location) {
			LocationEvent e = new LocationEvent(_name, _description, _location);
			events.Add(e);
			return e;
		}

		public void AddPath(string fromEventId, string toEventId) {
			BaseEvent from = FindEventById(fromEventId);
			BaseEvent to = FindEventById(toEventId);

			if (from == null || to == null) { return; }

			Path p = new Path(from, to);
			from.Paths.Add(p);
		}

		public BaseEvent FindEventById(string eventId) {
			foreach (BaseEvent e in events) {
				if (e.Id == eventId) {
					return e;
				}
			}

			return null;
		}

		public void BFS(string _id, int _orderNumber = 1) {
			BaseEvent thisEvent = FindEventById(_id);
			thisEvent.Order = _orderNumber;

			foreach (Path p in thisEvent.Paths) {
				Debug.Log(p.endEvent.Order);
				if (p.endEvent.Order == -1) {
					BFS(p.endEvent.Id, _orderNumber + 1);
				}
			}
		}

		public void PrintPath() {
			Debug.Log($"Quest: {title}");
			foreach (BaseEvent e in events) {
				Debug.Log($"{e.Name}:  {e.Order}");
			}
		}
	}
}
