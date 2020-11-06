using System.Collections.Generic;
using System;
using UnityEngine;
using TMPro;

namespace Moonshot.Quests {

	public enum EventStatus { WAITING, CURRENT, DONE }

	public class QuestEvent {
		#region Variables
		private string id;
		private string name;
		private string description;
		private int order = -1;
		private EventStatus status;
		private List<Path> paths = new List<Path>();
		#endregion

		#region Properties
		public string Id { get { return id; } }
		public string Name { get { return name; } }
		public string Description { get { return description; } }
		public EventStatus Status { get { return status; } }
		public List<Path> Paths { get { return paths; } }
		public int Order { 
			get { return order; }
			set { order = value; }
		}
		#endregion

		public QuestEvent(string _name, string _description) {
			id = Guid.NewGuid().ToString();
			name = _name;
			description = _description;
			status = EventStatus.WAITING;
		}

		public void UpdateEvent(EventStatus _status) {
			status = _status;
		}
	}
}
