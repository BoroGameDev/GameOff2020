using System.Collections.Generic;
using UnityEngine;

namespace Moonshot.Quests {
	public class Quest {
		public List<QuestEvent> events = new List<QuestEvent>();
		List<QuestEvent> paths = new List<QuestEvent>();

		public Quest() { }

		public QuestEvent AddEvent(string _name, string _description) {
			QuestEvent e = new QuestEvent(_name, _description);
			events.Add(e);
			return e;
		}

		public void AddPath(string fromEventId, string toEventId) {
			QuestEvent from = FindEventById(fromEventId);
			QuestEvent to = FindEventById(toEventId);

			if (from == null || to == null) { return; }

			Path p = new Path(from, to);
			from.Paths.Add(p);
		}

		public QuestEvent FindEventById(string eventId) {
			foreach (QuestEvent e in events) {
				if (e.Id == eventId) {
					return e;
				}
			}

			return null;
		}

		public void BFS(string _id, int _orderNumber = 1) {
			QuestEvent thisEvent = FindEventById(_id);
			thisEvent.Order = _orderNumber;

			foreach (Path p in thisEvent.Paths) {
				if (p.endEvent.Order == -1) {
					BFS(p.endEvent.Id, _orderNumber + 1);
				}
			}
		}

		public void PrintPath() {
			foreach (QuestEvent e in events) {
				Debug.Log($"{e.Name}:  {e.Order}");
			}
		}
	}
}
