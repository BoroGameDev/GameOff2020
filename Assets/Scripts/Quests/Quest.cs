﻿
using System.Collections.Generic;
using System.Linq;

using UnityEngine;

namespace Moonshot.Quests {

	[CreateAssetMenu(fileName = "New Quest", menuName = "Quests/Quest")]
	public class Quest : ScriptableObject {
		public string title = "Quest";
		public List<BaseEvent> events = new List<BaseEvent>();
		public bool completed;
		public bool started;

		public void Init() {
			foreach (BaseEvent _e in events) { 
				_e.Parent = this;
				_e.Init();
			}
		}

		public void Start() {
			List<BaseEvent> _events = events.Where<BaseEvent>(_e => _e.Order == 0).ToList<BaseEvent>();
			foreach (BaseEvent _e in _events) {
				_e.Start();
			}
			started = true;
		}

		public void CheckEvents() {
			completed = events.All(evt => evt.Status == EventStatus.DONE);
		}

		public BaseEvent AddEvent(BaseEvent _event) {
			events.Add(_event);
			return _event;
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

		public void BFS(string _id, int _orderNumber = 0) {
			BaseEvent thisEvent = FindEventById(_id);
			thisEvent.Order = _orderNumber;

			foreach (Path p in thisEvent.Paths) {
				if (p.endEvent.Order == -1) {
					BFS(p.endEvent.Id, _orderNumber + 1);
				}
			}
		}

		public void PrintPath() {
			Debug.Log($"Quest: {title}");
			foreach (BaseEvent e in events) {
				Debug.Log($"{e.Name}:  {e.Order} {e.Status}");
			}
		}
	}
}
