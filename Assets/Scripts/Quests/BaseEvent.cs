using Moonshot.GameManagement;

using System.Collections.Generic;

using UnityEngine;

namespace Moonshot.Quests {

	public enum EventStatus { WAITING, CURRENT, DONE }

	public class BaseEvent {

		[HideInInspector] public Quest Parent;
		public string Id { get; protected set; }
		public string Name { get; protected set; }
		public string Description { get; protected set; }
		public EventStatus Status = EventStatus.WAITING;
		public List<Path> Paths = new List<Path>();
		[HideInInspector] public int Order = -1;

		public BaseEvent(Quest _parent, string _name, string _description) {
			Id = System.Guid.NewGuid().ToString();
			Name = _name;
			Description = _description;
			Parent = _parent;
		}

		protected virtual bool IsRequirementSatisfied() {
			return Status == EventStatus.DONE;
		}

		public virtual void Start() { 
			UpdateEvent(EventStatus.CURRENT);
		}

		public void UpdateEvent(EventStatus _status) {
			Status = _status;

			if (_status == EventStatus.DONE) {
				GameEvents.Instance.QuestEventCompleted();
				foreach (Path p in Paths) {
					p.endEvent.Start();
				}
			}
			Parent.CheckEvents();
		}
	}
}
